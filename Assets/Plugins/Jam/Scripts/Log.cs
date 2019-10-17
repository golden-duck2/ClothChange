#if JAM_LOG_DETAIL
	#define LOG_DETAIL
#endif
using System;
using System.Collections;
using UnityEngine;

namespace Jam
{
	public static class Log
	{
#if LOG_DETAIL
		/// <summary>
		/// 呼び出し元のメソッドの情報を返す.
		///  MethodName(ParamType ParamName)の文字列を返す.
		/// </summary>
		/// <returns>呼び出し元のメソッドの情報</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		public static string MethodInfo(
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			var sb = StringBuilderEx.Rent().Clear()	;
			var objStackFrame = new System.Diagnostics.StackFrame(1);
			var methodBase = objStackFrame.GetMethod();
			sb.Append(methodBase.ReflectedType.ToString());
			sb.Append(".");
			sb.Append(methodBase.Name);
			sb.Append("(");

			var parameters = methodBase.GetParameters();
			foreach (var p in parameters)
			{
#if false  // detail.
				sb.Append(p.ParameterType);
#else      // simple.
				sb.Append(p.ParameterType.Name);
#endif
				sb.Append(" ");
				sb.Append(p.Name);
				sb.Append(", ");
			}
			if (parameters.Length != 0)
			{
				sb.Length -= 2;     // 末尾の" ,"を削除する.
			}
			sb.Append(")");
			return StringBuilderEx.Return(sb);
		}
#else  // LOG_DETAIL
		public static string MethodInfo()
		{
			return "";
		}
#endif // LOG_DETAIL
				/*
				Android環境でログから負いやすくするために使用していたが、あまり使わないので、とりあえず封印.
				/// <summary>
				/// log debug.
				/// </summary>
				public static void D(string msg, Object context = null)
				{
		#if UNITY_EDITOR
					if (context == null)
					{
						Debug.Log(msg);
					}
					else
					{
						Debug.Log(msg, context);
					}
		#elif UNITY_STANDALONE_WIN
				System.Diagnostics.Trace.WriteLine(msg);
		#elif UNITY_ANDROID
				var log = new AndroidJavaClass("android.util.Log");
				log.CallStatic<int>("d", "Unity", msg);
		#else
		#endif
				}

				public static void W(string msg, Object context = null)
				{
		#if UNITY_EDITOR
					if (context == null)
					{
						Debug.LogWarning(msg);
					}
					else
					{
						Debug.LogWarning(msg, context);
					}
		#elif UNITY_STANDALONE_WIN
				System.Diagnostics.Trace.WriteLine(msg);
		#elif UNITY_ANDROID
				var log = new AndroidJavaClass("android.util.Log");
				log.CallStatic<int>("w", "Unity", msg);
		#else
		#endif
				}

				public static void E(string msg, Object context = null)
				{
		#if UNITY_EDITOR
					if (context == null)
					{
						Debug.LogError(msg);
					}
					else
					{
						Debug.LogError(msg, context);
					}
		#elif UNITY_STANDALONE_WIN
				System.Diagnostics.Trace.WriteLine(msg);
		#elif UNITY_ANDROID
				var log = new AndroidJavaClass("android.util.Log");
				log.CallStatic<int>("e", "Unity", msg);
		#else
		#endif
				}
				*/

			}
}