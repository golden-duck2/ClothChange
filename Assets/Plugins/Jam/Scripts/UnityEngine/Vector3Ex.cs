using UnityEngine;

namespace Jam
{
	public static class Vector3Ex
	{
		#region value
		public static readonly Vector3 NaN = new Vector3(float.NaN, float.NaN, float.NaN);
		#endregion

		#region Set
		public static void Set(ref Vector3 v, float x, float y, float z)
		{
			v.Set(x, y, z);
		}

		public static void SetXY(ref Vector3 v, float x, float y)
		{
			v.Set(x, y, v.z);
		}

		public static void SetX(ref Vector3 v, float x)
		{
			v.Set(x, v.y, v.z);
		}

		public static void SetY(ref Vector3 v, float y)
		{
			v.Set(v.x, y, v.z);
		}

		public static void SetZ(ref Vector3 v, float z)
		{
			v.Set(v.x, v.y, z);
		}

		//	public static void SetNaN(ref Vector3 v)
		//	{
		//		v.Set(float.NaN, float.NaN, float.NaN);
		//	}
		#endregion

		#region Add
		public static void Add(ref Vector3 v, float x, float y, float z)
		{
			v.Set(v.x + x, v.y + y, v.z + z);
		}

		public static void AddXY(ref Vector3 v, float x, float y)
		{
			v.Set(v.x + x, v.y + y, v.z);
		}

		public static void AddX(ref Vector3 v, float x)
		{
			v.Set(v.x + x, v.y, v.z);
		}

		public static void AddY(ref Vector3 v, float y)
		{
			v.Set(v.x, v.y + y, v.z);
		}

		public static void AddZ(ref Vector3 v, float z)
		{
			v.Set(v.x, v.y, v.z + z);
		}
		#endregion
	}
}
