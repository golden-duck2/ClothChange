using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jam
{
	public static class VertexHelperEx
	{
		public static readonly Vector3 s_DefaultNormal = Vector3.back;
		public static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		/// <summary>
		/// vhへ三角形を追加する.
		///  頂点は反時計回りに指定する.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="p0"></param>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <param name="c32"></param>
		public static void AddTriangle(this VertexHelper self, Vector3 p0, Vector3 p1, Vector3 p2, Color32 c32)
		{
			//			Debug.Log("at AddTriangle(lt:" + p0 + ", rt:" + p1 + ", lb:" + p2 + ")");
			int currentVertCount = self.currentVertCount;
			self.AddVert(p0, c32, Vector2.zero, Vector2.zero, s_DefaultNormal, s_DefaultTangent);   // 
			self.AddVert(p1, c32, Vector2.zero, Vector2.zero, s_DefaultNormal, s_DefaultTangent);   // 
			self.AddVert(p2, c32, Vector2.zero, Vector2.zero, s_DefaultNormal, s_DefaultTangent);   // 
			self.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
		}

		/// <summary>
		/// vhへ四角形を追加する.
		/// </summary>
		public static void AddQuad(this VertexHelper self, Vector3 lt, Vector3 rt, Vector3 lb, Vector3 rb, Color32 c32)
		{
			//			Debug.Log("at AddQuad(lt:" + lt + ", rt:" + rt + ", lb:" + lb + ", rb:" + rb + ")");
			int currentVertCount = self.currentVertCount;
			self.AddVert(lb, c32, Vector2.zero, Vector2.zero, s_DefaultNormal, s_DefaultTangent);   // left bottom.
			self.AddVert(rb, c32, Vector2.zero, Vector2.zero, s_DefaultNormal, s_DefaultTangent);   // right bottom.
			self.AddVert(rt, c32, Vector2.zero, Vector2.zero, s_DefaultNormal, s_DefaultTangent);   // right top.
			self.AddVert(lt, c32, Vector2.zero, Vector2.zero, s_DefaultNormal, s_DefaultTangent);   // left top.
			self.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			self.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

	}
}
