#if JAM_LOG_DETAIL
	#define LOG_DETAIL
#endif
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Jam
{
	/// <summary>
	/// enum utility.
	/// </summary>
	public static class EnumUtil
	{
		/// <summary>
		/// T型へvalueをパースする.
		/// </summary>
		/// <typeparam name="T">Enum.</typeparam>
		/// <param name="value">値.</param>
		/// <param name="defaultValue">パース失敗したときに返す値.</param>
		/// <returns>valueをT型にパースした値.</returns>
		public static T Parse<T>(string value, T defaultValue=default) where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				Debug.LogError("at EnumUtil.Parse<T:" + typeof(T).ToString() + ">(value:" + value + ", defaultValue:" + defaultValue.ToString() + ") T is not enum type.");
				return defaultValue;
			}
			else if (Enum.IsDefined(typeof(T), value))
			{
				return (T)Enum.Parse(typeof(T), value);
			}
#if LOG_DETAIL
			Debug.LogWarning("at EnumUtil.Parse<T:" + typeof(T).ToString() + ">(value:" + value + ", defaultValue:" + defaultValue.ToString() + ") parse failed.");
#endif // LOG_DETAIL
			return defaultValue;
		}

		/// <summary>
		/// Tにvalueが定義されているかを取得する.
		/// </summary>
		/// <typeparam name="T">Enum.</typeparam>
		/// <param name="value">値.</param>
		/// <returns>Tにvalueが定義されていればtrue.</returns>
		public static bool IsDefined<T>(string value) where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				Debug.LogError("at EnumUtil.IsDefined<T:" + typeof(T).ToString() + ">(value:" + value + ") T is not enum type.");
				return false;
			}
			return Enum.IsDefined(typeof(T), value);
		}

		/// <summary>
		/// Enumの値の配列を取得.
		/// </summary>
		/// <returns>指定型Enumの配列.</returns>
		public static Array GetValueArray<T>() where T : struct
		{
			return Enum.GetValues(typeof(T));
		}

		/// <summary>
		/// Enumの値のリストを取得.
		/// </summary>
		/// <returns>指定型Enumのリスト.</returns>
		public static List<T> GetValueList<T>() where T : struct
		{
			var list = new List<T>();
			foreach (T val in Enum.GetValues(typeof(T)))
			{
				list.Add(val);
			}
			return list;
		}

		/// <summary>
		/// 列挙型の値の定義されている数を取得.
		/// </summary>
		public static int GetCount<T>() where T : struct
		{
			return Enum.GetValues(typeof(T)).Length;
		}
	}




	public static class EnumUtil2<T>
	{
		/// <summary>
		/// Enumの値の配列を取得.
		/// </summary>
		/// <returns> 指定型Enumの配列. </returns>
		public static Array GetValueArray()
		{
			return Enum.GetValues(typeof(T));
		}

	}
}











/*	
public static T toObject(int val) {
	T	result;
	try {
		Enum<T>.SafeParse(val, result);
	} finally {
	}
	return result;
}

public static T parse(int val) {
	return parse(val.ToString());
}

public static T parse(string val) {
	T	result;
	if (Enum<T>.SafeParse(val, out result)) {
		return result;
	}
	return result;
}
 */

/*
	/// <summary> 列挙体を文字列の一覧として取得. </summary>
	/// <returns> 名前一覧. </returns>
	public static string[] GetNames( )
	{ return Enum.GetNames( typeof(T) ); }

	/// <summary> 文字列からEnumを取得(失敗すると例外を投げる). </summary>
	/// <returns> 解析結果. </returns>
	/// <param name='val'> 解析対象文字列. </param>
	public static T Parse( string val )
	{ return (T) Enum.Parse( typeof(T), val ); }

	/// <summary> 文字列から可能ならEnumを取得し、成否を返す. </summary>
	/// <returns> 解析の成否.成功したらtrue </returns>
	/// <param name='val'> 解析対象文字列. </param>
	/// <param name='result'> 解析結果の出力先. </param>
	public static bool TryParse( string val, out T result )
	{
		bool isMatch = false;
		result = default(T);

		if( Enum<T>.IsDefined( val ) )
		{
			result = (T) Enum<T>.Parse( val );
			isMatch = true;
		}

		return isMatch;
	}

	/// <summary> 文字列からEnumを取得(失敗時のデフォルト値を指定). </summary>
	/// <returns> 解析結果. </returns>
	/// <param name='val'> 解析対象文字列. </param>
	/// <param name='failsafe'> 解析失敗時のデフォルト値. </param>
	public static T SafeParse( string val, T failsafe )
	{
		T retval = failsafe;
		if( TryParse( val, out retval ) )
		{ return retval; }

		return failsafe;
	}


	/// <summary> 指定の値はEnumに含まれるか. </summary>
	/// <returns> valがT型に含まれるときtrue. </returns>
	/// <param name='val'> 検査対象. </param>
	public static bool IsDefined( object val )
	{ return Enum.IsDefined( typeof(T), val ); }

	/// <summary> 元となる型情報を取得. </summary>
	/// <returns> 型情報. </returns>
	public static Type GetUnderlyingType( )
	{ return Enum.GetUnderlyingType( typeof(T) ); }

*/
