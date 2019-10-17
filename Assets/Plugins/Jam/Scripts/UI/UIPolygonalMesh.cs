using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Sprites;

// @TODO
//  Pointsのwidth対応.

namespace Jam
{
	/// <summary>
	/// UI polygonal mesh.
	/// </summary>
	[ExecuteInEditMode()]
	[RequireComponent(typeof(CanvasRenderer))]
	public class UIPolygonalMesh : MaskableGraphic, ILayoutElement
	{
		//		static readonly System.Action<VertexHelper>[] onPopulateMeshActionArray = new System.Action<VertexHelper>[]
		//			{
		//				OnPopulateMeshPoints,
		//			};

		public enum DrawType
		{
			Points,
			LineStripTruncated, // 折れ線.(交点を面取りする.)
								//			LineStripSharp,		// 折れ線.(交点を尖らせる.)
			LineStripFast,      // 折れ線.
								//			Lines,				// ライン.
			User,
		}

		public DrawType drawType
		{
			get
			{
				return _drawType;
			}
			set
			{
				_drawType = value;
				switch (_drawType)
				{
					case DrawType.Points:
						onPopulateMeshAction = OnPopulateMeshPoints;
						break;
					case DrawType.LineStripTruncated:
						onPopulateMeshAction = OnPopulateMeshLineStripTruncated;
						break;
					case DrawType.LineStripFast:
						onPopulateMeshAction = OnPopulateMeshLineStripFast;
						break;
					//					case DrawType.Lines:
					//						onPopulateMeshAction = OnPopulateMeshLines;
					//						break;
					case DrawType.User:
						break;
					default:
						Debug.LogWarning("not supported yet.");
						break;
				}
			}
		}

		[SerializeField]
		DrawType _drawType = DrawType.Points;

		[SerializeField, Range(0.001f, 100)]
		public float width = 10;
		[SerializeField]
		public Vector2[] points;

		public Rect pointsRect
		{
			get
			{
				if (_pointsRect == Rect.zero)
				{
					CalcPointsRect();
				}
				return _pointsRect;
			}
		}
		protected Rect _pointsRect = Rect.zero;

		System.Action<VertexHelper> onPopulateMeshAction;

		protected override void Awake()
		{
			drawType = _drawType;
		}

		/// <summary>
		/// 描画方法をユーザー指定にする.
		/// </summary>
		/// <param name="aOnPopulateMeshAction"></param>
		public void SetOnPopulateMeshAction(System.Action<VertexHelper> aOnPopulateMeshAction)
		{
			onPopulateMeshAction = aOnPopulateMeshAction;
			_drawType = DrawType.User;
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if(onPopulateMeshAction != null)
			{
				onPopulateMeshAction(vh);
			}
		}

		void OnPopulateMeshPoints(VertexHelper vh)
		{
			vh.Clear();
			Color32 c32 = color;
			foreach (var p in points)
			{
				vh.AddQuad(new Vector3(p.x - 1, p.y + 1, 0), new Vector3(p.x + 1, p.y + 1, 0), new Vector3(p.x - 1, p.y - 1, 0), new Vector3(p.x + 1, p.y - 1, 0), c32);
			}
			//			material.color = color;
			canvasRenderer.SetMaterial(material, Texture2D.whiteTexture);
		}

		/// <summary>
		/// 折れ線を描く.(交点を面取りする.)
		/// </summary>
		void OnPopulateMeshLineStripTruncated(VertexHelper vh)
		{
			vh.Clear();
			var pointsLength = points.Length;
			if (pointsLength == 0)
			{
				return;
			}
			else if (pointsLength == 1)
			{   // draw point.
				OnPopulateMeshPoints(vh);
				return;
			}
			Color32 c32 = color;

//			for (var idx = 0; idx < pointsLength; ++idx)
//			{
//				var pos = points[idx];       // current position.
//				Debug.Log("pos[" + idx + "]:" + pos);
//			}

			var prevPos = points[0];            // prev position.
			var currPos = points[1];            // current position.
			var pcLine = currPos-prevPos;
			var pcLineVecRot90 = pcLine.GetRot90Deg();
			pcLineVecRot90.Normalize();
			pcLineVecRot90 = pcLineVecRot90 * width * 0.5f;
			var prevT = prevPos + pcLineVecRot90;
			var prevB = prevPos - pcLineVecRot90;
			var currT = currPos + pcLineVecRot90;
			var currB = currPos - pcLineVecRot90;

			for (var idx = 2; idx < pointsLength; ++idx)
			{
				var nextPos = points[idx];       // current position.
				var cnLine = nextPos - currPos;
				var cnLineVecRot90 = cnLine.GetRot90Deg();
				cnLineVecRot90.Normalize();
				cnLineVecRot90 = cnLineVecRot90 * width * 0.5f;

				var currT2 = currPos + cnLineVecRot90;
				var currB2 = currPos - cnLineVecRot90;
				var nextT = nextPos + cnLineVecRot90;
				var nextB = nextPos - cnLineVecRot90;

				vh.AddQuad( prevT, currT, prevB, currB, c32);

				var pcAim = Vector2Ex.GetAimRad(prevPos, currPos);
				var cnAim = Vector2Ex.GetAimRad(currPos, nextPos);
				if (pcAim < cnAim)
				{
					vh.AddTriangle(currPos, currB2, currB, c32);
				}
				else
				{
					vh.AddTriangle(currT, currT2, currPos, c32);
				}

				prevPos = currPos;            // prev position.
				currPos = nextPos;// points[idx]            // current position.
				pcLine = cnLine;
				pcLineVecRot90 = cnLineVecRot90;
				prevT = currT2;
				prevB = currB2;
				currT = nextT;
				currB = nextB;
			}
			vh.AddQuad(prevT, currT, prevB, currB, c32);

			canvasRenderer.SetMaterial(material, Texture2D.whiteTexture);
		}

		/// <summary>
		/// 折れ線を描く.
		///  各線分の間を補完しない為、早いが線が太いと隙間が多い.
		/// </summary>
		/// <param name="vh"></param>
		void OnPopulateMeshLineStripFast(VertexHelper vh)
		{
			vh.Clear();
			Color32 c32 = color;
			var prePos = points[0];             // prev position.

			for (var idx = 1; idx < points.Length; ++idx)
			{
				var curPos = points[idx];       // current position.
				var lineVec = curPos - prePos;
				var lineVecRot90 = lineVec.GetRot90Deg();
				lineVecRot90.Normalize();
				lineVecRot90 = lineVecRot90 * width;
				var lt = prePos + lineVecRot90;
				var lb = prePos - lineVecRot90;
				var rt = curPos + lineVecRot90;
				var rb = curPos - lineVecRot90;
				prePos = curPos;
				int currentVertCount = vh.currentVertCount;
				vh.AddVert(lb, c32, Vector2.zero, Vector2.zero, VertexHelperEx.s_DefaultNormal, VertexHelperEx.s_DefaultTangent);   // left bottom.
				vh.AddVert(rb, c32, Vector2.zero, Vector2.zero, VertexHelperEx.s_DefaultNormal, VertexHelperEx.s_DefaultTangent);   // right bottom.
				vh.AddVert(rt, c32, Vector2.zero, Vector2.zero, VertexHelperEx.s_DefaultNormal, VertexHelperEx.s_DefaultTangent);   // right top.
				vh.AddVert(lt, c32, Vector2.zero, Vector2.zero, VertexHelperEx.s_DefaultNormal, VertexHelperEx.s_DefaultTangent);   // left top.
				vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
				vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
			}
			canvasRenderer.SetMaterial(material, Texture2D.whiteTexture);
		}

		Vector2 LineIntersect(Vector2 origin1, Vector2 direction1, Vector2 origin2, Vector2 direction2)
		{
			// at LineIntersect(origin1:(685.0, 89.4), direction1:(683.1, 85.7), origin2:(683.1, 85.7), direction2:(684.0, 87.3)) intersect:(705.7, 129.5)
			Vector2 intersect = Vector2.zero;
			Vector2 slopeV1 = origin1 - direction1;
			float slopeF1 = slopeV1.y / slopeV1.x;
			Vector2 slopeV2 = origin2 - direction2;
			float slopeF2 = slopeV2.y / slopeV2.x;
			intersect.x = (slopeF1 * origin1.x - slopeF2 * origin2.x + origin2.y - origin1.y) / (slopeF1 - slopeF2);
			intersect.y = slopeF1 * (intersect.x - origin1.x) + origin1.y;
			return intersect;
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

		protected void CalcPointsRect()
		{
			var minX = float.MaxValue;
			var maxX = float.MinValue;
			var minY = float.MaxValue;
			var maxY = float.MinValue;
			foreach (var p in points)
			{
				minX = Mathf.Min(minX, p.x);
				maxX = Mathf.Max(minX, p.x);
				minY = Mathf.Min(minY, p.y);
				maxY = Mathf.Max(minY, p.y);
			}
			_pointsRect = new Rect(minX, minY, maxX - minX, maxY - minY);
		}
	}
}
