//#define JAM_DEBUG

using UnityEngine;
using System.Collections.Generic;

namespace Jam
{
	/// <summary>
	/// Transform extension.
	/// </summary>
	public static class TransformEx
	{
		static Vector3 tmp;

		/// <summary>
		/// Get RectTransform.
		/// </summary>
		public static RectTransform GetRectTransform(this Transform self)
		{
			return self as RectTransform;
		}

		/// <summary>
		/// Get child Transform array.
		/// </summary>
		static public Transform[] GetChildArray(this Transform self)
		{
			if (self.childCount == 0)
			{
				return null;
			}
			var ret = new Transform[self.childCount];
			var idx = 0;
			foreach (Transform childTransform in self)
			{
				ret[idx] = childTransform;
				++idx;
			}
			return ret;
		}

		/// <summary>
		/// Get child Transform List.
		/// </summary>
		static public List<Transform> GetChildList(this Transform self)
		{
			var ret = new List<Transform>();
			foreach (Transform childTransform in self)
			{
				ret.Add(childTransform);
			}
			return ret;
		}

		/// <summary>
		/// Destroy all children.
		/// </summary>
		static public void DestroyAllChildren(this Transform self)
		{
			foreach (Transform childT in self)
			{
				GameObject.Destroy(childT.gameObject);
			}
		}

		/// <summary>
		/// Get the object path.
		/// </summary>
		/// <param name="self"></param>
		/// <returns>object path</returns>
		public static string GetPath(this Transform self)
		{
			var path = self.gameObject.name;
			var parent = self.parent;
			while (parent != null)
			{
				path = parent.name + "/" + path;
				parent = parent.parent;
			}
			path = "/" + path;
			return path;
		}

		#region SetPosition
		public static void SetPosition(this Transform transform, float x, float y, float z)
		{
			tmp.Set(x, y, z);
			transform.position = tmp;
		}
		public static void SetPositionX(this Transform transform, float x)
		{
			tmp.Set(x, transform.position.y, transform.position.z);
			transform.position = tmp;
		}
		public static void SetPositionY(this Transform transform, float y)
		{
			tmp.Set(transform.position.x, y, transform.position.z);
			transform.position = tmp;
		}
		public static void SetPositionZ(this Transform transform, float z)
		{
			tmp.Set(transform.position.x, transform.position.y, z);
			transform.position = tmp;
		}
		#endregion

		#region AddPosition
		public static void AddPosition(this Transform transform, float x, float y, float z)
		{
			tmp.Set(transform.position.x + x, transform.position.y + y, transform.position.z + z);
			transform.position = tmp;
		}
		public static void AddPositionX(this Transform transform, float x)
		{
			tmp.Set(transform.position.x + x, transform.position.y, transform.position.z);
			transform.position = tmp;
		}
		public static void AddPositionY(this Transform transform, float y)
		{
			tmp.Set(transform.position.x, transform.position.y + y, transform.position.z);
			transform.position = tmp;
		}
		public static void AddPositionZ(this Transform transform, float z)
		{
			tmp.Set(transform.position.x, transform.position.y, transform.position.z + z);
			transform.position = tmp;
		}
		#endregion

		#region SetLocalPosition
		public static void SetLocalPosition(this Transform transform, float x, float y, float z)
		{
			tmp.Set(x, y, z);
			transform.localPosition = tmp;
		}

		public static void SetLocalPositionX(this Transform transform, float x)
		{
			tmp.Set(x, transform.localPosition.y, transform.localPosition.z);
			transform.localPosition = tmp;
		}

		public static void SetLocalPositionY(this Transform transform, float y)
		{
			tmp.Set(transform.localPosition.x, y, transform.localPosition.z);
			transform.localPosition = tmp;
		}

		public static void SetLocalPositionZ(this Transform transform, float z)
		{
			tmp.Set(transform.localPosition.x, transform.localPosition.y, z);
			transform.localPosition = tmp;
		}

		public static void SetLocalPositionXY(this Transform transform, float x, float y)
		{
			tmp.Set(x, y, transform.localPosition.z);
			transform.localPosition = tmp;
		}
		#endregion

		#region AddLocalPosition
		public static void AddLocalPosition(this Transform transform, float x, float y, float z)
		{
			tmp.Set(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z + z);
			transform.localPosition = tmp;
		}

		public static void AddLocalPositionX(this Transform transform, float x)
		{
			tmp.Set(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z);
			transform.localPosition = tmp;
		}

		public static void AddLocalPositionY(this Transform transform, float y)
		{
			tmp.Set(transform.localPosition.x, transform.localPosition.y + y, transform.localPosition.z);
			transform.localPosition = tmp;
		}

		public static void AddLocalPositionZ(this Transform transform, float z)
		{
			tmp.Set(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + z);
			transform.localPosition = tmp;
		}

		public static void AddLocalPositionXY(this Transform transform, float x, float y)
		{
			tmp.Set(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z);
			transform.localPosition = tmp;
		}
		#endregion

		#region SetLocalScale
		public static void SetLocalScale(this Transform transform, float x, float y, float z)
		{
			tmp.Set(x, y, z);
			transform.localScale = tmp;
		}
		public static void SetLocalScaleXY(this Transform transform, float x, float y)
		{
			tmp.Set(x, y, transform.localScale.z);
			transform.localScale = tmp;
		}
		public static void SetLocalScaleX(this Transform transform, float x)
		{
			tmp.Set(x, transform.localScale.y, transform.localScale.z);
			transform.localScale = tmp;
		}
		public static void SetLocalScaleY(this Transform transform, float y)
		{
			tmp.Set(transform.localScale.x, y, transform.localScale.z);
			transform.localScale = tmp;
		}
		public static void SetLocalScaleZ(this Transform transform, float z)
		{
			tmp.Set(transform.localScale.x, transform.localScale.y, z);
			transform.localScale = tmp;
		}
		#endregion

		#region AddLocalScale
		public static void AddLocalScale(this Transform transform, float x, float y, float z)
		{
			tmp.Set(transform.localScale.x + x, transform.localScale.y + y, transform.localScale.z + z);
			transform.localScale = tmp;
		}
		public static void AddLocalScaleX(this Transform transform, float x)
		{
			tmp.Set(transform.localScale.x + x, transform.localScale.y, transform.localScale.z);
			transform.localScale = tmp;
		}
		public static void AddLocalScaleY(this Transform transform, float y)
		{
			tmp.Set(transform.localScale.x, transform.localScale.y + y, transform.localScale.z);
			transform.localScale = tmp;
		}
		public static void AddLocalScaleZ(this Transform transform, float z)
		{
			tmp.Set(transform.localScale.x, transform.localScale.y, transform.localScale.z + z);
			transform.localScale = tmp;
		}
		#endregion

		#region SetEulerAngles
		public static void SetEulerAngles(this Transform transform, float x, float y, float z)
		{
			tmp.Set(x, y, z);
			transform.eulerAngles = tmp;
		}
		public static void SetEulerAnglesX(this Transform transform, float x)
		{
			tmp.Set(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
			transform.eulerAngles = tmp;
		}
		public static void SetEulerAnglesY(this Transform transform, float y)
		{
			tmp.Set(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
			transform.eulerAngles = tmp;
		}
		public static void SetEulerAnglesZ(this Transform transform, float z)
		{
			tmp.Set(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
			transform.eulerAngles = tmp;
		}
		#endregion

		#region AddEulerAngles
		public static void AddEulerAngles(this Transform transform, float x, float y, float z)
		{
			tmp.Set(transform.eulerAngles.x + x, transform.eulerAngles.y + y, transform.eulerAngles.z + z);
			transform.eulerAngles = tmp;
		}
		public static void AddEulerAnglesX(this Transform transform, float x)
		{
			tmp.Set(transform.eulerAngles.x + x, transform.eulerAngles.y, transform.eulerAngles.z);
			transform.eulerAngles = tmp;
		}
		public static void AddEulerAnglesY(this Transform transform, float y)
		{
			tmp.Set(transform.eulerAngles.x, transform.eulerAngles.y + y, transform.eulerAngles.z);
			transform.eulerAngles = tmp;
		}
		public static void AddEulerAnglesZ(this Transform transform, float z)
		{
			tmp.Set(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + z);
			transform.eulerAngles = tmp;
		}
		#endregion

		#region SetLocalEulerAngles
		public static void SetLocalEulerAngles(this Transform transform, float x, float y, float z)
		{
			tmp.Set(x, y, z);
			transform.localEulerAngles = tmp;
		}
		public static void SetLocalEulerAnglesX(this Transform transform, float x)
		{
			tmp.Set(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
			transform.localEulerAngles = tmp;
		}
		public static void SetLocalEulerAnglesY(this Transform transform, float y)
		{
			tmp.Set(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
			transform.localEulerAngles = tmp;
		}
		public static void SetLocalEulerAnglesZ(this Transform transform, float z)
		{
			tmp.Set(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
			transform.localEulerAngles = tmp;
		}
		#endregion

		#region AddLocalEulerAngles
		public static void AddLocalEulerAngles(this Transform transform, float x, float y, float z)
		{
			tmp.Set(transform.localEulerAngles.x + x, transform.localEulerAngles.y + y, transform.localEulerAngles.z + z);
			transform.localEulerAngles = tmp;
		}
		public static void AddLocalEulerAnglesX(this Transform transform, float x)
		{
			tmp.Set(transform.localEulerAngles.x + x, transform.localEulerAngles.y, transform.localEulerAngles.z);
			transform.localEulerAngles = tmp;
		}
		public static void AddLocalEulerAnglesY(this Transform transform, float y)
		{
			tmp.Set(transform.localEulerAngles.x, transform.localEulerAngles.y + y, transform.localEulerAngles.z);
			transform.localEulerAngles = tmp;
		}
		public static void AddLocalEulerAnglesZ(this Transform transform, float z)
		{
			tmp.Set(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + z);
			transform.localEulerAngles = tmp;
		}
		#endregion

#if JAM_DEBUG || UNITY_EDITOR
		/// <summary>
		/// To string a hierarchy tree.
		/// </summary>
		/// <param name="originTF">起点となるTransform.nullの時は全てのTransform.</param>
		/// <returns></returns>
		public static string ToStringHierarchyTree(string prefix = "", Transform originTF = null)
		{
			var sb = new System.Text.StringBuilder(16384);
			if (originTF == null)
			{   // 全てのHierarchyを文字列化.
				sb.AppendLine("Hierarchy");
				var rootGameObjectArray = GameObjectEx.GetRootGameObjectsArray();
				for (var idx = 0; idx < rootGameObjectArray.Length; ++idx)
				{
					var originIsRootTerm = (idx == rootGameObjectArray.Length - 1);
					ToStringHierarchyTreeImpl(sb, prefix, originTF, originIsRootTerm, rootGameObjectArray[idx].transform);
				}
			}
			else
			{   // originTF以下のHierarchyを文字列化.
				ToStringHierarchyTreeImpl(sb, prefix, originTF, false, originTF);
			}
			return sb.ToString();
		}

		static void ToStringHierarchyTreeImpl(System.Text.StringBuilder sb, string prefix, Transform originTF, bool originIsRootTerm, Transform targetTF)
		{
			string treeStr = "";
			{   // treeStrの計算.
				var checkTF = targetTF;
				var checkParentTF = checkTF.parent;
				while (checkParentTF != null && checkTF != originTF)
				{
					string ts;
					if (checkParentTF.GetChild(checkParentTF.childCount - 1) != checkTF)
					{   // checkParentTF is default.
						ts = (checkTF == targetTF) ? "├─" : "│　";
					}
					else
					{   // checkParentTF is terminal.
						ts = (checkTF == targetTF) ? "└─" : "　　";
					}
					treeStr = ts + treeStr;
					checkTF = checkParentTF;
					checkParentTF = checkTF.parent;
				}
				if (originTF == null)
				{
					string ts;
					if (originIsRootTerm == false)
					{
						ts = (targetTF.parent == null) ? "├─" : "│　";
					}
					else
					{
						ts = (targetTF.parent == null) ? "└─" : "　　";
					}
					treeStr = ts + treeStr;
				}
			}
			sb.AppendLine(prefix + treeStr + targetTF.gameObject.name);
			for (var idx = 0; idx < targetTF.childCount; ++idx)
			{
				var childTF = targetTF.GetChild(idx);
				ToStringHierarchyTreeImpl(sb, prefix, originTF, originIsRootTerm, childTF);
			}
		}
#endif
	}
}
