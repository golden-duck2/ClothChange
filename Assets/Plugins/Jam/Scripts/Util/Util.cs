using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Jam
{
	/// <summary>
	/// Utility.
	/// </summary>
	public static class Util
	{
		//    public delegate void OnRedraw(System.Object aRelativeObject);
		static readonly int[] digits = { 0, 9, 99, 999, 9999, 99999, 999999, 9999999, 99999999, 999999999, int.MaxValue };


		// int型の桁数を取得する.
		public static int GetDigits(int aNum)
		{
			aNum = System.Math.Abs(aNum);
			var digit = 1;
			for (; ; ++digit)
			{
				if (aNum <= digits[digit])
					break;
			}
			return digit;
		}

		public static int BoolToInt(bool aVal)
		{
			return aVal ? 1 : 0;
		}

		public static bool IntToBool(int aVal)
		{
			return aVal != 0;
		}

		//	public static bool? IntQToBoolQ(int? aVal) {
		//		return aVal.HasValue ? (bool?)(aVal != 0) : null;
		//	}

		// convert millisecond to mm:ss string.
		public static string millisecond2mmss(long aMillisecond)
		{
			long min = aMillisecond / (60L * 1000L);
			long sec = (aMillisecond - (min * (60L * 1000L))) / 1000L;
			return string.Format("{0:00}:{1:00}", min, sec);
		}

		/// <summary>
		/// Get the name of the type.
		/// </summary>
		static public string GetNameOfType<T>()
		{
			string s = typeof(T).ToString();
			return s;
		}

		/// <summary>
		/// 値を入れ替える.
		/// </summary>
		/// <param name="lhs">Lhs.</param>
		/// <param name="rhs">Rhs.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T temp = lhs;
			lhs = rhs;
			rhs = temp;
		}

		/*		たぶん現在は不要.
			#region
			/// <summary>  
			/// 特定の名前を持つ型を取得します。  
			/// </summary>  
			/// <param name="name">型名</param>  
			/// <returns>取得した型</returns>  
			public static Type GetTypeFromName(String name)
			{
				return GetTypeFromName(name, GetCurrentDomainAssembliesTypeOld());
			}

			/// <summary>  
			/// 特定の名前を持つ型を取得します。  
			/// </summary>  
			/// <param name="name">型名</param>  
			/// <param name="types">型のリスト</param>  
			/// <returns>取得した型</returns>  
			public static Type GetTypeFromName(String name, IEnumerable<Type> types)
			{
				Debug.Log("GetTypeFromName(name:"+name+")");
				foreach (Type type in types)
				{
					Debug.Log("*@" + type.FullName + ", " + type.AssemblyQualifiedName + ".");
					if (String.Equals(type.FullName, name) || String.Equals(type.AssemblyQualifiedName, name))
						return type;
				}
				return null;
			}

			/// <summary>  
			/// CurrentDomainのアセンブリの型一覧を取得します。  
			/// </summary>  
			/// <returns>型一覧</returns>  
			public static IList<Type> GetCurrentDomainAssembliesTypeOld()
			{
				System.Reflection.Assembly[] assemblies;
				assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
				List<Type> assemblyTypes = new List<Type>();
				foreach (System.Reflection.Assembly assembly in assemblies)
				{
					assemblyTypes.AddRange(assembly.GetTypes());
				}
				return assemblyTypes;
			}

			/// <summary> 
			/// CurrentDomainのアセンブリの型一覧を取得します。 
			/// </summary> 
			/// <returns>型一覧</returns> 
			public static IEnumerable<Type> GetCurrentDomainAssembliesType()
			{
				System.Reflection.Assembly[] assemblies;
				assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
				foreach (System.Reflection.Assembly assembly in assemblies)
				{
					foreach (System.Type type in assembly.GetTypes())
					{
						yield return type;
					}
				}
			}
			#endregion
		*/
	}
}