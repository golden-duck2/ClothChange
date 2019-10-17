#if JAM_LOG_DETAIL
//	#define LOG_DETAIL
#endif
using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;

namespace Jam
{
	/// <summary>
	/// StringBuilder extension.
	/// </summary>
	public static class StringBuilderEx
	{
		const int CAPACITY = 4096;
		const int MAX_POOL_COUNT = 2;

		/// <summary>
		/// 用意済みのStringBuilder.
		///  newしないで使えるが、他でも使っている可能性があるので注意.
		/// </summary>
		public static StringBuilder sb = new StringBuilder(CAPACITY);

		/// <summary>
		/// 用意済みのStringBuilder.
		/// </summary>
		static Queue<StringBuilder> pool = new Queue<StringBuilder>();

		static int poolInstanceNum = 0;
#if LOG_DETAIL
		static Dictionary<StringBuilder, string> inUseDic = new Dictionary<StringBuilder, string>();
#endif // LOG_DETAIL

		/// <summary>
		/// Rent instance from pool.
		///  ※返される値は初期化していない.
		/// </summary>
#if LOG_DETAIL
		public static StringBuilder Rent(
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			StringBuilder instance = null;
			if (pool.Count > 0)
			{
				instance = pool.Dequeue();
				var caller = sourceFilePath + ":" + sourceLineNumber.ToString();
				inUseDic[instance] = caller;
			}
			else if (poolInstanceNum < MAX_POOL_COUNT)
			{
				instance = new StringBuilder(CAPACITY);
				var caller = sourceFilePath + ":" + sourceLineNumber.ToString();
				Debug.Log("at StringBuilder.Rent() Create a new instance. caller:" + caller);
				inUseDic[instance] = caller;
				++poolInstanceNum;
			}
			else
			{
				Debug.LogError("at StringBuilder.Rent() Reached Max PoolSize.");
				LogPool();
			}
			return instance;
		}
#else  // LOG_DETAIL
		public static StringBuilder Rent()
		{
			StringBuilder instance = null;
			if (pool.Count > 0)
			{
				instance = pool.Dequeue();
			}
			else if (poolInstanceNum < MAX_POOL_COUNT)
			{
				instance = new StringBuilder(CAPACITY);
				++poolInstanceNum;
			}
			else
			{
				Debug.LogError("at StringBuilder.Rent() Reached Max PoolSize.");
			}
			return instance;
		}
#endif // LOG_DETAIL

		/// <summary>
		/// Return instance.ToString() result and Return(instance).
		/// </summary>
#if LOG_DETAIL
		public static string Return(
			StringBuilder instance,
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			if (instance == null)
			{   // nullが渡された.
				Debug.LogError("at StringBuilder.ToStringReturn() instance is null.");
				return "";
			}
			if (!inUseDic.ContainsKey(instance))
			{   // 管理対象外のinstanceが渡された.
				Debug.LogError("at StringBuilder.ToStringReturn() instance is unmanaged.");
				LogPool();
				return "";
			}
			var caller = sourceFilePath + ":" + sourceLineNumber.ToString();
			//Debug.Log("at StringBuilder.ToStringReturn() Return the instance.\n Rent().caller:" + inUseDic[instance] + "\n Return().caller:" + caller);
			var ret = instance.ToString();
			inUseDic.Remove(instance);
			pool.Enqueue(instance);
			return ret;
		}
#else  // LOG_DETAIL
		public static string Return(StringBuilder instance)
		{
			var ret = instance.ToString();
			pool.Enqueue(instance);
			return ret;
		}
#endif // LOG_DETAIL

		/// <summary>
		/// プールの情報をログ出力する.
		/// </summary>
#if LOG_DETAIL
		public static void LogPoolInfo()
		{
//			var sb = StringBuilderEx.sb.Clear();
			var sb = new StringBuilder(1024);
			sb.AppendLine("StringBuilderEx.LogPool()");
			sb.AppendLine(" instanceNum:" + poolInstanceNum);
			sb.AppendLine(" inUseDic.Count:" + inUseDic.Count);
			sb.AppendLine(" inUseDic:");
			foreach (var kv in inUseDic)
			{
				sb.AppendLine("  " + kv.Value);
			}
			Debug.Log(sb.ToString());
		}
#else  // LOG_DETAIL
		public static void LogPoolInfo()
		{
		}
#endif // LOG_DETAIL

			/*	オフィシャルに実装された.
			public static StringBuilder Clear(this StringBuilder aSelf)
			{
				aSelf.Length = 0;
				return aSelf;
			}*/

			public static StringBuilder Set(this StringBuilder aSelf, string aStr)
		{
			aSelf.Length = 0;
			aSelf.Append(aStr);
			return aSelf;
		}

		public static StringBuilder SetLine(this StringBuilder aSelf, string aStr)
		{
			aSelf.Length = 0;
			aSelf.AppendLine(aStr);
			return aSelf;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder aSelf, string aFormat, params object[] aArgs)
		{
			aSelf.AppendFormat(aFormat, aArgs);
			aSelf.AppendLine();
			return aSelf;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder aSelf, System.IFormatProvider aProvider, string aFormat, params object[] aArgs)
		{
			aSelf.AppendFormat(aProvider, aFormat, aArgs);
			aSelf.AppendLine();
			return aSelf;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder aSelf, string aFormat, object aArg0)
		{
			aSelf.AppendFormat(aFormat, aArg0);
			aSelf.AppendLine();
			return aSelf;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder aSelf, string aFormat, object aArg0, object aArg1)
		{
			aSelf.AppendFormat(aFormat, aArg0, aArg1);
			aSelf.AppendLine();
			return aSelf;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder aSelf, string aFormat, object aArg0, object aArg1, object aArg2)
		{
			aSelf.AppendFormat(aFormat, aArg0, aArg1, aArg2);
			aSelf.AppendLine();
			return aSelf;
		}
	}
}
