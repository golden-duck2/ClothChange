using System;
using System.Linq;
using System.Collections.Generic;

namespace Jam
{
	public static class IEnumerableEx
	{
		/// <summary>
		/// 全ての要素にactionを実行する.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="action"></param>
		public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
		{
			foreach (T item in self)
			{
				action(item);
			}
		}

		/// <summary>
		/// ランダムに1個の要素を取得.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static T GetRandom<T>(this IEnumerable<T> self)
		{
			return self.ElementAt(UnityEngine.Random.Range(0, self.Count()));
		}

		/// <summary>
		/// ランダムにnum個の要素を取得.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> self, int num)
		{
			return self.Shuffle().Take(num);
		}

		/// <summary>
		/// シャッフルしたIEnumerableを取得.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> self)
		{
			return self.OrderBy(i => Guid.NewGuid());
		}
		/*	public static T[] Shuffle<T>(this IEnumerable<T> src)
			{
				var result = src.ToArray();
				for (int i = result.Length - 1; i >= 1; i--)
				{
					var n = UnityEngine.Random.Range(0, i + 1);
					var item = result[i];
					result[i] = result[n];
					result[n] = item;
				}
				return result;
			}
			*/
	}
}