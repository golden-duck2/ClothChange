using System;
using System.Linq;

namespace Jam
{
	public static class ArrayEx
	{
		public static bool IsIndexOutOfRange(this System.Array self, int index)
		{
			return (index < 0) || (index >= self.Length);
		}
		/*
			IEnumerableEx を使用する.
			public static T GetRandom<T>(this T[] self)
			{
				return self[UnityEngine.Random.Range(0, self.Length)];
			}

			public static T[] Shuffle<T>(this T[] self)
			{
				return self.OrderBy(i => Guid.NewGuid()).ToArray();
			}
		*/
	}
}
