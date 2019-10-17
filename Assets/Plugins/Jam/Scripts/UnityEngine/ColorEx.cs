using UnityEngine;
using System.Collections;

namespace Jam
{
	public static class ColorEx
	{
		public static readonly Color NaN = new Color(float.NaN, float.NaN, float.NaN, float.NaN);
		public static readonly Color ClearWhite = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		public static Color Random {
			get {
				var r = UnityEngine.Random.Range(0.0f, 1.0f);
				var g = UnityEngine.Random.Range(0.0f, 1.0f);
				var b = UnityEngine.Random.Range(0.0f, 1.0f);
				return new Color(r, g, b);
			}
		}
	}
}
