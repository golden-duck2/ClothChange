using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Sprites;

namespace Jam
{
	/// <summary>
	/// UI lines.
	/// </summary>
	[ExecuteInEditMode()]
	[RequireComponent(typeof(CanvasRenderer))]
	public class UILines : MaskableGraphic, ILayoutElement
	{
		[System.Serializable]
		public struct UILineData
		{
			[SerializeField]
			public Vector2 pointA;
			[SerializeField]
			public Vector2 pointB;
			[SerializeField, Range(0.001f, 100)]
			public float thickness;
			[SerializeField]
			public Color32 color32;
			//			[SerializeField]
			//			public Color col;

			public UILineData(Vector2 aPointA, Vector2 aPointB, float aThickness, Color32 aColor)
			{
				pointA = aPointA;
				pointB = aPointB;
				thickness = aThickness;
				color32 = aColor;
			}
		}

		[SerializeField]
		public UILineData[] lineDataArray;

		public Rect pointsRect
		{
			get
			{
				var minX = float.MaxValue;
				var maxX = float.MinValue;
				var minY = float.MaxValue;
				var maxY = float.MinValue;
				foreach (var ld in lineDataArray)
				{
					minX = Mathf.Min(minX, ld.pointA.x);
					maxX = Mathf.Max(minX, ld.pointA.x);
					minY = Mathf.Min(minY, ld.pointA.y);
					maxY = Mathf.Max(minY, ld.pointA.y);
					minX = Mathf.Min(minX, ld.pointB.x);
					maxX = Mathf.Max(minX, ld.pointB.x);
					minY = Mathf.Min(minY, ld.pointB.y);
					maxY = Mathf.Max(minY, ld.pointB.y);
				}
				return new Rect(minX, minY, maxX - minX, maxY - minY);
			}
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			foreach (var ld in lineDataArray)
			{
				var lineVec = ld.pointB - ld.pointA;
				var lineVecRot90 = lineVec.GetRot90Deg();
				lineVecRot90.Normalize();
				lineVecRot90 = lineVecRot90 * (ld.thickness * 0.5f);
				var lt = ld.pointA + lineVecRot90;
				var lb = ld.pointA - lineVecRot90;
				var rt = ld.pointB + lineVecRot90;
				var rb = ld.pointB - lineVecRot90;

				int currentVertCount = vh.currentVertCount;
				vh.AddVert(lb, ld.color32, Vector2.zero, Vector2.zero, VertexHelperEx.s_DefaultNormal, VertexHelperEx.s_DefaultTangent);   // left bottom.
				vh.AddVert(rb, ld.color32, Vector2.zero, Vector2.zero, VertexHelperEx.s_DefaultNormal, VertexHelperEx.s_DefaultTangent);   // right bottom.
				vh.AddVert(rt, ld.color32, Vector2.zero, Vector2.zero, VertexHelperEx.s_DefaultNormal, VertexHelperEx.s_DefaultTangent);   // right top.
				vh.AddVert(lt, ld.color32, Vector2.zero, Vector2.zero, VertexHelperEx.s_DefaultNormal, VertexHelperEx.s_DefaultTangent);   // left top.
				vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
				vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
			}
			canvasRenderer.SetMaterial(material, Texture2D.whiteTexture);
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		public virtual void CalculateLayoutInputVertical()
		{
		}

		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		public virtual float preferredWidth
		{
			get
			{
				return pointsRect.width;
			}
		}

		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		public virtual float preferredHeight
		{
			get
			{
				return pointsRect.height;
			}
		}

		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();
		}

#if UNITY_EDITOR
		public override void OnRebuildRequested()
		{
			base.OnRebuildRequested();
		}
#endif

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			if (!this.IsActive())
			{
				base.OnValidate();
			}
			else
			{
				base.OnValidate();
			}
		}
#endif
	}
}
