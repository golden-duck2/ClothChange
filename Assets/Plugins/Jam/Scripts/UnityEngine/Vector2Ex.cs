using UnityEngine;

namespace Jam
{
	public static class Vector2Ex
	{
		#region value
		public static readonly Vector2 NaN = new Vector2(float.NaN, float.NaN);
		#endregion

		#region Set
		public static void Set(ref Vector2 v, float x, float y)
		{
			v.Set(x, y);
		}

		public static void SetX(ref Vector2 v, float x)
		{
			v.Set(x, v.y);
		}

		public static void SetY(ref Vector2 v, float y)
		{
			v.Set(v.x, y);
		}

		//	public static void SetNaN(ref Vector2 v)
		//	{
		//		v.Set(float.NaN, float.NaN);
		//	}
		#endregion

		#region Add
		public static void Add(ref Vector2 v, float x, float y)
		{
			v.Set(v.x + x, v.y + y);
		}

		public static void AddX(ref Vector2 v, float x)
		{
			v.Set(v.x + x, v.y);
		}

		public static void AddY(ref Vector2 v, float y)
		{
			v.Set(v.x, v.y + y);
		}
		#endregion

		/// <summary>
		/// Returns the squared distance between a and b.
		/// </summary>
		public static float SqrDistance(Vector2 a, Vector2 b)
		{
			var c = a - b;
			return c.sqrMagnitude;
		}

		/// <summary>
		/// Return the aim direction from a to b.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static float GetAimDeg(Vector2 a, Vector2 b)
		{
			float dx = b.x - a.x;
			float dy = b.y - a.y;
			float rad = Mathf.Atan2(dy, dx);
			return rad * Mathf.Rad2Deg;
		}

		/// <summary>
		/// Return the aim direction from a to b.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static float GetAimRad(Vector2 a, Vector2 b)
		{
			float dx = b.x - a.x;
			float dy = b.y - a.y;
			float rad = Mathf.Atan2(dy, dx);
			return rad;
		}

		public static Vector2 GetRot90Deg(this Vector2 a)
		{
			return new Vector2(-a.y, a.x);
		}


	}
}