#if JAM_LOG_DETAIL
	#define LOG_DETAIL
#endif
using System;
using System.Reflection;
using UnityEngine;

namespace Jam
{
	/// <summary>
	/// string extension.
	/// </summary>
	public static class StringEx
	{
		public static bool ParseBool(this string self, bool defaultValue)
		{
			bool val;
			if (!bool.TryParse(self, out val))
			{
#if LOG_DETAIL
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(defaultValue:" + defaultValue + ")\n TryParse(\"" + self + "\") returns false.");
#endif
				val = defaultValue;
			}
			return val;
		}

		public static int ParseInt(this string self, int defaultValue)
		{
			int val;
			if (!int.TryParse(self, out val))
			{
#if LOG_DETAIL
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(defaultValue:" + defaultValue + ")\n TryParse(\"" + self + "\") returns false.");
#endif
				val = defaultValue;
			}
			return val;
		}

		public static uint ParseUInt(this string self, uint defaultValue)
		{
			uint val;
			if (!uint.TryParse(self, out val))
			{
#if LOG_DETAIL
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(defaultValue:" + defaultValue + ")\n TryParse(\"" + self + "\") returns false.");
#endif
				val = defaultValue;
			}
			return val;
		}

		public static long ParseLong(this string self, long defaultValue)
		{
			long val;
			if (!long.TryParse(self, out val))
			{
#if LOG_DETAIL
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(defaultValue:" + defaultValue + ")\n TryParse(\"" + self + "\") returns false.");
#endif
				val = defaultValue;
			}
			return val;
		}

		public static float ParseFloat(this string self, float defaultValue)
		{
			float val;
			if (!float.TryParse(self, out val))
			{
#if LOG_DETAIL
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(defaultValue:" + defaultValue + ")\n TryParse(\"" + self + "\") returns false.");
#endif
				val = defaultValue;
			}
			return val;
		}

		public static double ParseDouble(this string self, double defaultValue)
		{
			double val;
			if (!double.TryParse(self, out val))
			{
#if LOG_DETAIL
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(defaultValue:" + defaultValue + ")\n TryParse(\"" + self + "\") returns false.");
#endif
				val = defaultValue;
			}
			return val;
		}

		public static Color ParseColor(this string self, Color defaultValue)
		{
			Color val;
			if (!ColorUtility.TryParseHtmlString(self, out val))
			{
#if LOG_DETAIL
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(defaultValue:" + defaultValue + ")\n ColorUtility.TryParseHtmlString(\"" + self + "\") returns false.");
#endif
				val = defaultValue;
			}
			return val;
		}

		public static PrimitiveTypes32 ParsePrimitiveTypes32(this string self, PrimitiveTypes32 defaultValue)
		{
			PrimitiveTypes32 val;
			if (PrimitiveTypes32.TryParse(self, out val))
			{
#if LOG_DETAIL
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(defaultValue:" + defaultValue + ")\n PrimitiveTypes32.TryParse(\"" + self + "\") returns false.");
#endif
				val = defaultValue;
			}
			return val;
		}

		public static string[] Split(this string self, string separator)
		{
#if LOG_DETAIL
			if (separator.Length == 0)
			{
				Debug.LogWarning("at \"" + self + "\"" + "." + MethodBase.GetCurrentMethod().Name + "(separator:" + separator + ")\n Recommended to use Split(char).");
			}
#endif
			return self.Split(new[] { separator }, System.StringSplitOptions.None);
		}

		public static string[] SplitLines(this string self)
		{
#if true
			var sb = StringBuilderEx.sb;
			sb.Set(self);
			sb.Replace("\r\n", "\r");
			sb.Replace('\n', '\r');
			return sb.ToString().Split('\r');
#else
			// ※上記に比べ環境によっては2000倍遅い. 2017/10/27 Unity2017.1.0f3.
			return self.Split(new [] { "\r\n", "\n", "\r" }, System.StringSplitOptions.None);
#endif
		}

		/// <summary>
		/// 文字列の指定した位置から指定した文字数を取得する.
		/// </summary>
		/// <param name="self">文字列</param>
		/// <param name="start">開始位置(1文字目は1.)</param>
		/// <param name="length">文字数</param>
		/// <returns>取得した文字列</returns>
		public static string Mid(this string self, int start, int length)
		{
			if (start <= 0)
			{
				throw new ArgumentException("引数'aStart'は1以上でなければなりません。");
			}
			if (length < 0)
			{
				throw new ArgumentException("引数'aLength'は0以上でなければなりません。");
			}
			if (self == null || self.Length < start)
			{
				return "";
			}
			if (self.Length < (start + length))
			{
				return self.Substring(start - 1);
			}
			return self.Substring(start - 1, length);
		}

		/// <summary>
		/// 文字列の指定した位置から末尾までを取得する.
		/// </summary>
		/// <param name="self">文字列</param>
		/// <param name="start">開始位置</param>
		/// <returns>取得した文字列</returns>
		public static string Mid(this string self, int start)
		{
			return Mid(self, start, self.Length);
		}

		/// <summary>
		/// 文字列の先頭から指定した文字数の文字列を取得する.
		/// </summary>
		/// <param name="self">文字列</param>
		/// <param name="length">文字数</param>
		/// <returns>取得した文字列</returns>
		public static string Left(this string self, int length)
		{
			if (length < 0)
			{
				throw new ArgumentException("引数'aLength'は0以上でなければなりません。");
			}
			if (self == null)
			{
				return "";
			}
			if (self.Length <= length)
			{
				return self;
			}
			return self.Substring(0, length);
		}

		/// <summary>
		/// 文字列の末尾から指定した文字数の文字列を取得する.
		/// </summary>
		/// <param name="self">文字列</param>
		/// <param name="length">文字数</param>
		/// <returns>取得した文字列</returns>
		public static string Right(this string self, int length)
		{
			if (length < 0)
			{
				throw new ArgumentException("引数'length'は0以上でなければなりません。");
			}
			if (self == null)
			{
				return "";
			}
			if (self.Length <= length)
			{
				return self;
			}
			return self.Substring(self.Length - length, length);
		}
	}
}
