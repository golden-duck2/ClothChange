using UnityEngine;
using System.Collections;

namespace Jam
{
	/// <summary>
	/// GC.
	/// </summary>
	public static class GC
	{
		public static void CollectLv1()
		{
			System.GC.Collect();
		}

		public static void CollectLv2()
		{
			System.GC.Collect();
			Resources.UnloadUnusedAssets();
		}

		public static void CollectLv3()
		{
			System.GC.Collect(System.GC.MaxGeneration);
			Resources.UnloadUnusedAssets();
		}
	}
}
