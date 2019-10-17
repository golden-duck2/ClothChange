#if UNITY_EDITOR
//#define DEBUG_DETAIL
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 参考
//  https://cfm-art.sakura.ne.jp/sys/archives/319
//  http://wordpress.notargs.com/blog/blog/2015/08/30/unityugui%E3%81%A7%E6%9B%B2%E7%B7%9A%E3%82%92%E6%8F%8F%E7%94%BB%E3%81%99%E3%82%8B/

namespace Jam
{
	/// <summary>
	/// 折れ線グラフ描画.
	/// </summary>
	public class LineGraph : GraphBase
	{
		const float H_TEXT_COLOR_CHANGE_OFFSET = 0.5f;

		[SerializeField, Range(0.0f, 20.0f)]
		protected float lineThickness = 3;
		public void SetLineThickness(float aLineThickness)
		{
			lineThickness = aLineThickness;
			// 更新登録する.
			RegistUpdate();
		}

//		[SerializeField]
//		protected GameObject linePrefab;
		[SerializeField]
		protected GameObject graphLinePrefab;
		[SerializeField,ColorHtmlProperty]
		protected Color lineColor;

		[SerializeField, ColorHtmlProperty]
		protected Color graphBaseColor;

//		[SerializeField, Range(0.0f, 20.0f)]
//		protected float pointSize = 4;
//		[SerializeField]
//		protected GameObject pointPrefab;
//		[SerializeField, ColorHtmlProperty]
//		protected Color pointColor;

		[System.Serializable]
		public class AxisConfig
		{
			// line.
//			[SerializeField]
//			public GameObject linePrefab;
			[SerializeField, ColorHtmlProperty]
			public Color lineColor;
			[SerializeField, Range(0.0f, 20.0f)]
			public float lineThickness = 2;
//			[System.NonSerialized]
//			public PrefabPool linePrefabPool;

			// text.
			[SerializeField]
			public GameObject textPrefab;
			[SerializeField, ColorHtmlProperty]
			public Color textColor;
			[SerializeField, Range(0, 10)]
			public int fractionalDigits = 0;
			[SerializeField]
			public Vector2 textOffset;
			[System.NonSerialized]
			public PrefabPool textPrefabPool;

			public void Init(GameObject aGO, Transform aLinePreabParent, Transform aTextPreabParent)
			{
//				if (linePrefabPool != null)
//				{
//					Debug.LogError("linePrefabPool is not null.");
//				}
//				if (linePrefab != null && lineThickness > 0.0f)
//				{
//					var pp = aGO.AddComponent<PrefabPool>();
//					pp.SetValue(linePrefab, aLinePreabParent);
//					linePrefabPool = pp;
//				}
				if (textPrefab != null)
				{
					var pp = aGO.AddComponent<PrefabPool>();
					pp.SetValue(textPrefab, aTextPreabParent);
					textPrefabPool = pp;
				}
			}

			public void Clear()
			{
//				if (linePrefabPool != null)
//				{
//					linePrefabPool.ReturnAll();
//					Destroy(linePrefabPool);
//					linePrefabPool = null;
//				}
				if (textPrefabPool != null)
				{
					textPrefabPool.ReturnAll();
//					Destroy(textPrefabPool);
//					textPrefabPool = null;
				}
			}
		}

		[SerializeField]
		protected AxisConfig hMainAxisConfig;
		[SerializeField]
		protected AxisConfig hSubAxisConfig;
		[SerializeField]
		protected AxisConfig vMainAxisConfig;
		[SerializeField]
		protected AxisConfig vSubAxisConfig;


		protected List<KeyValuePair<DateTime, float>> valList;
		// horizonal parameters.
		protected DateTime valueHMin;
		protected DateTime valueHMax;
		protected float scaleH;
		protected int hMainDivisions = 5;
		protected int hSubDivisions = 1;
		// vertical parameters.
		protected float valueVMin;
		protected float valueVMax;
		protected float scaleV;
		protected int vMainDivisions = 5;
		protected int vSubDivisions = 1;

		protected ScrollRect graphScrollRect;
		protected ScrollRect hTextScrollRect;
		protected ScrollRect vTextScrollRect;
		protected PrefabPool linePrefabPool;
//		protected PrefabPool pointPrefabPool;
		protected PrefabPool graphLinePrefabPool;
//		protected ComponentPoolBase<UIPolygonalMesh> uiPolygonalMeshPool;
#if DEBUG_DETAIL
		[SerializeField, Jam.Text]
		string paramMemo = "";
//		[SerializeField, Jam.Text]
//		string valMemo = "";
#endif

		protected override void Awake()
		{
			Spy.StopwatchesStart("LineGraph.Awake()");
			base.Awake();
			//			Debug.LogWarning("Graph.Awake()");
			//			Debug.LogWarning("viewRTF.rect:\n" + viewRTF.ToDebugString(" |"));

			{   // Create GraphScrollView.
				graphScrollRect = CreateScrollView("GraphScrollView", viewRTF, true);
				graphScrollRect.movementType = ScrollRect.MovementType.Clamped;
				graphScrollRect.viewport.GetComponent<Image>().color = graphBaseColor;
			}

			{   // Create hTextScrollView.
				hTextScrollRect = CreateScrollView("HTextScrollView", viewRTF, true);
				hTextScrollRect.GetComponent<Image>().enabled = false; // マウスへの反応を切る.
				hTextScrollRect.viewport.GetComponent<Image>().enabled = false; // マウスへの反応を切る.
				hTextScrollRect.viewport.GetComponent<Mask>().enabled = false;  // マスクを切る.
			}

			{   // Create vTextScrollView.
				vTextScrollRect = CreateScrollView("VTextScrollView", viewRTF, true);
				vTextScrollRect.GetComponent<Image>().enabled = false; // マウスへの反応を切る.
				vTextScrollRect.viewport.GetComponent<Image>().enabled = false; // マウスへの反応を切る.
				vTextScrollRect.viewport.GetComponent<Mask>().enabled = false;  // マスクを切る.
			}

			graphScrollRect.onValueChanged.AddListener(
				value =>
				{
					// graphScrollRectのスクロールとhTextScrollRectのスクロールを連動させる.
					hTextScrollRect.horizontalScrollbar.value = value.x;
					// graphScrollRectのスクロールとvTextScrollRectのスクロールを連動させる.
					vTextScrollRect.verticalScrollbar.value = value.y;
					// horizontal textの表示非表示切り替え.
					foreach (var hTextTF in hTextTFList)
					{
#if true
						var targetAlpha = 1.0f;
//						targetAlpha = Mathf.Clamp(hTextTF.position.x, viewMinXGlobal, viewMinXGlobal);
						if (hTextTF.position.x < viewMinXGlobal - H_TEXT_COLOR_CHANGE_OFFSET ||
							viewMaxXGlobal + H_TEXT_COLOR_CHANGE_OFFSET < hTextTF.position.x)
						{
							targetAlpha = 0.0f;
						}
						else if (hTextTF.position.x < viewMinXGlobal)
						{
							targetAlpha = (hTextTF.position.x - (viewMinXGlobal - H_TEXT_COLOR_CHANGE_OFFSET)) / H_TEXT_COLOR_CHANGE_OFFSET;
						}
						else if (viewMaxXGlobal < hTextTF.position.x)
						{
							targetAlpha = 1.0f-(hTextTF.position.x - (viewMaxXGlobal)) / H_TEXT_COLOR_CHANGE_OFFSET;
						}
						hTextTF.GetComponent<Text>().color = new Color(
							hTextTF.GetComponent<Text>().color.r,
							hTextTF.GetComponent<Text>().color.g,
							hTextTF.GetComponent<Text>().color.b,
							targetAlpha);
#else
						var targetAlpha = -1.0f;
						if (hTextTF.position.x < viewMinXGlobal - H_TEXT_COLOR_CHANGE_OFFSET)
						{
							targetAlpha = 0.0f;
						}
						else if (viewMaxXGlobal + H_TEXT_COLOR_CHANGE_OFFSET < hTextTF.position.x)
						{
							targetAlpha = 0.0f;
						}
						else if (viewMinXGlobal < hTextTF.position.x && hTextTF.position.x < viewMaxXGlobal)
						{
							targetAlpha = 1.0f;
						}
						if (targetAlpha != -1.0f)
						{
							DOTween.ToAlpha(
								() => hTextTF.GetComponent<Text>().color,
								color => hTextTF.GetComponent<Text>().color = color,
								targetAlpha,
								0.3f);
						}
#endif
					}
				});

			//			if (linePrefab != null && lineThickness > 0.0f)
			//			{
			//				linePrefabPool = gameObject.AddComponent<PrefabPool>();
			//				linePrefabPool.SetValue(linePrefab, graphScrollRect.content);
			//			}
			if (graphLinePrefab != null && lineThickness > 0.0f)
			{
				graphLinePrefabPool = gameObject.AddComponent<PrefabPool>();
				graphLinePrefabPool.SetValue(graphLinePrefab, graphScrollRect.content);
			}
/*
			if (pointPrefab != null && pointSize > 0.0f)
			{
				pointPrefabPool = gameObject.AddComponent<PrefabPool>();
				pointPrefabPool.SetValue(pointPrefab, graphScrollRect.content);
			}
*/
//			uiPolygonalMeshPool = gameObject.AddComponent<ComponentPoolBase<UIPolygonalMesh>>();

			hMainAxisConfig.Init(gameObject, graphScrollRect.content, hTextScrollRect.content);
			hSubAxisConfig.Init(gameObject, graphScrollRect.content, hTextScrollRect.content);
			vMainAxisConfig.Init(gameObject, graphScrollRect.content, vTextScrollRect.content);
			vSubAxisConfig.Init(gameObject, graphScrollRect.content, vTextScrollRect.content);
			Spy.StopwatchesStop("LineGraph.Awake()");
		}

		public void SetValue(
			List<KeyValuePair<DateTime, float>> aValList,
			DateTime aValueHMin, DateTime aValueHMax, float aScaleH,// TimeSpan aViewHSize,
			float aValueVMin, float aValueVMax, float aScaleV,// float aViewVSize, /*Rect aGraphRect, Rect aGraphDispRect,*/
			int aVMainDivisions, int aVSubDivisions, int aHMainDivisions, int aHSubDivisions)
		{
#if DEBUG_DETAIL
			Debug.Log("at LineGraph.SetValue()");
			if (aHMainDivisions <= 0)
			{
				Debug.LogWarning("aHMainDivisions <= 0");
				aHMainDivisions = 1;
			}
			if (aHSubDivisions <= 0)
			{
				Debug.LogWarning("aHSubDivisions <= 0");
				aHSubDivisions = 1;
			}
			if (aVMainDivisions <= 0)
			{
				Debug.LogWarning("aVMainDivisions <= 0");
				aVMainDivisions = 1;
			}
			if (aVSubDivisions <= 0)
			{
				Debug.LogWarning("aVSubDivisions <= 0");
				aVSubDivisions = 1;
			}
#endif
			valList = aValList;
			valueHMin = aValueHMin;
			valueHMax = aValueHMax;
			scaleH = aScaleH;
			valueVMin = aValueVMin;
			valueVMax = aValueVMax;
			scaleV = aScaleV;
			vMainDivisions = aVMainDivisions;
			vSubDivisions = aVSubDivisions;
			hMainDivisions = aHMainDivisions;
			hSubDivisions = aHSubDivisions;

#if DEBUG_DETAIL
			{
				var sb = StringBuilderEx.sb;
				{
					sb.Clear();
					sb.AppendFormatLine("valueHMin:{0}", valueHMin.ToString("MM/dd HH:mm"));
					sb.AppendFormatLine("valueHMax:{0}", valueHMax.ToString("MM/dd HH:mm"));
					sb.AppendFormatLine("scaleH:{0}", scaleH);
//					sb.AppendFormatLine("viewHSize:{0}", viewHSize.Ticks);

					sb.AppendFormatLine("valueVMin:{0}", valueVMin);
					sb.AppendFormatLine("valueVMax:{0}", valueVMax);
					sb.AppendFormatLine("scaleV:{0}", scaleV);
//					sb.AppendFormatLine("viewVSize:{0}", viewVSize);

					sb.AppendFormatLine("hMainDivisions:{0}, hSubDivisions:{1}", hMainDivisions, hSubDivisions);
					sb.AppendFormatLine("vMainDivisions:{0}, vSubDivisions:{1}", vMainDivisions, vSubDivisions);
					paramMemo = sb.ToString();
//					Debug.Log("@SetValue():\n" + sb.ToString());
				}
				{
					sb.SetLine("valList:");
					foreach (var v in valList)
					{
						sb.AppendLine(" " + v.Key.ToString("MM/dd HH:mm") + ":" + v.Value);
					}
//					valMemo = sb.ToString();
					Debug.Log(sb.ToString());
				}
			}
#endif
			// 更新登録する.
			RegistUpdate();
		}

		public void ClearValue()
		{
			valList = null;
			// 古いオブジェクトの返却.
			foreach (Transform tf in graphScrollRect.content)
			{
				if (tf.GetComponent<UILines>() != null)
				{
					Destroy(tf.gameObject);
				}
			}
			if (linePrefabPool != null)
			{
				linePrefabPool.ReturnAll();
			}
			if (graphLinePrefabPool != null)
			{
				graphLinePrefabPool.ReturnAll();
			}
/*
			if (pointPrefabPool != null)
			{
				pointPrefabPool.ReturnAll();
			}
*/
			hMainAxisConfig.Clear();
			hSubAxisConfig.Clear();
			vMainAxisConfig.Clear();
			vSubAxisConfig.Clear();
		}

		List<RectTransform> hTextTFList = new List<RectTransform>();
		float viewMinXGlobal;
		float viewMaxXGlobal;

//		[SerializeField]
//		Material graphMaterial;

		protected override IEnumerator UpdateCoroutine()
		{
			do
			{
				Spy.StopwatchesStart("LineGraph.UpdateCoroutine() A");
				needUpdate = false;
				var cornersArray = new Vector3[4];
				viewRTF.GetWorldCorners(cornersArray);
				viewMinXGlobal = cornersArray[0].x;
				viewMaxXGlobal = cornersArray[2].x;
#if DEBUG_DETAIL
				paramMemo += "viewMinXGlobal:" + viewMinXGlobal + "\n";
				paramMemo += "viewMaxXGlobal:" + viewMaxXGlobal + "\n";
//				Debug.LogWarning("@@@@@@@@viewMinXGlobal:" + viewMinXGlobal);
//				Debug.LogWarning("@@@@@@@@viewMaxXGlobal:" + viewMaxXGlobal);
#endif
				// scrollRectを求める.
				//  viewRTFよりも枠線分小さくする.
				var scrollRect = new Rect(
					hMainAxisConfig.lineThickness * 0.5f,
					vMainAxisConfig.lineThickness * 0.5f,
					(viewRTF.rect.width- hMainAxisConfig.lineThickness) * scaleH + hMainAxisConfig.lineThickness,
					(viewRTF.rect.height - vMainAxisConfig.lineThickness) * scaleV + vMainAxisConfig.lineThickness);
			var graphWidth = scrollRect.width - hMainAxisConfig.lineThickness;
			var graphHeight = scrollRect.height- vMainAxisConfig.lineThickness;

				var valueHDiff = (valueHMax - valueHMin);
//				Debug.LogWarning("@@@@@@@@valueHMin:" + valueHMin.ToString("MM / dd HH: mm"));
//				Debug.LogWarning("@@@@@@@@valueHMax:" + valueHMax.ToString("MM / dd HH: mm"));
//				Debug.LogWarning("@@@@@@@@TotalDays:" + valueHDiff.TotalDays);
//				Debug.LogWarning("@@@@@@@@Days:" + valueHDiff.Days);
//				Debug.LogWarning("@@@@@@@@m:" + valueHDiff.TotalMinutes);
				var valueVDiff = (valueVMax - valueVMin);
				graphScrollRect.content.pivot = Vector2.zero;
				graphScrollRect.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollRect.width);
				graphScrollRect.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scrollRect.height);
				// hTextScrollRectの位置調整.
				hTextScrollRect.content.pivot = Vector2.zero;
				hTextScrollRect.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollRect.width);
				var hTextScrollRectRTF = hTextScrollRect.transform as RectTransform;
				hTextScrollRectRTF.offsetMin = new Vector2(viewRTF.offsetMin.x, 0);
				hTextScrollRectRTF.offsetMax = new Vector2(viewRTF.offsetMax.x, viewRTF.offsetMax.y - viewRTF.rect.height);
				// vTextScrollRectの位置調整.
				vTextScrollRect.content.pivot = Vector2.zero;
				vTextScrollRect.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scrollRect.height);
				var vTextScrollRectRTF = vTextScrollRect.transform as RectTransform;
				vTextScrollRectRTF.offsetMin = new Vector2(0, viewRTF.offsetMin.y);
				vTextScrollRectRTF.offsetMax = new Vector2(viewRTF.offsetMax.x - viewRTF.rect.width, viewRTF.offsetMax.y);
				var valueHMaxTick = valueHMax.Ticks;

				Spy.StopwatchesStop("LineGraph.UpdateCoroutine() A");
				Spy.StopwatchesStart("LineGraph.UpdateCoroutine() CreateHorizontal");
//				List<Vector2> lineListMain = new List<Vector2>();
//				List<Vector2> lineListSub = new List<Vector2>();
				List<UILines.UILineData> lineData = new List<UILines.UILineData>();
				{   // Create horizontal objects.
					var divisions = hMainDivisions * hSubDivisions * scaleH;
					for (var idx = 0; idx < divisions + 1; ++idx)
					{
						var posH = scrollRect.x + (graphWidth / divisions) * idx;
						AxisConfig ac = (idx % hSubDivisions == 0) ? hMainAxisConfig : hSubAxisConfig;
//						if (ac.linePrefabPool != null)
						{   // Line.
#if true
							// Add horizontal line data.(縦線データの追加.)
							var ld = new UILines.UILineData(
								new Vector2(posH, 0),
								new Vector2(posH, scrollRect.height),
								ac.lineThickness,
								ac.lineColor);
							lineData.Add(ld);
#else
							// Create horizontal line.(縦線.)
							var lineRTF = ac.linePrefabPool.Rent() as RectTransform;
							lineRTF.anchorMin = Vector2.zero;
							lineRTF.anchorMax = Vector2.zero;
							lineRTF.pivot = new Vector2(0.5f, 0.0f);
							lineRTF.anchoredPosition = new Vector2(posH, 0);
							lineRTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ac.lineThickness);
							lineRTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scrollRect.height);
							lineRTF.eulerAngles = Vector3.zero;
							lineRTF.GetComponent<Image>().color = ac.lineColor;
#endif
						}
						if (ac.textPrefabPool != null)
						{   // Text.
							// Create horizontal text.
							var val = valueHMin.Ticks + (long)(valueHDiff.Ticks / divisions) * idx;
							var textRTF = ac.textPrefabPool.Rent() as RectTransform;
							textRTF.anchorMin = Vector2.up;
							textRTF.anchorMax = Vector2.up;
							textRTF.pivot = new Vector2(0.5f, 1.0f);
							textRTF.anchoredPosition = new Vector2(posH + ac.textOffset.x, ac.textOffset.y);
							var text = textRTF.GetComponent<Text>();
							text.text = new DateTime(val).Round(new TimeSpan(0, 1, 0)).ToString("MM/dd HH:mm");	// 分で丸める.
							if (textRTF.position.x < viewMinXGlobal - H_TEXT_COLOR_CHANGE_OFFSET ||
								viewMaxXGlobal + H_TEXT_COLOR_CHANGE_OFFSET < textRTF.position.x)
							{
								text.color = new Color(ac.textColor.r, ac.textColor.g, ac.textColor.b, 0.0f);
							}
							else
							{
								text.color = ac.textColor;
							}
							hTextTFList.Add(textRTF);
						}
					}
#if true
					/*					var graphLineRTF = graphLinePrefabPool.Rent() as RectTransform;
					graphLineRTF.SetParent(graphScrollRect.content, false);
					graphLineRTF.anchorMin = Vector2.zero;
					graphLineRTF.anchorMax = Vector2.zero;
					graphLineRTF.pivot = Vector2.zero;
					graphLineRTF.anchoredPosition = Vector2.zero;
					var polygonalLine = graphLineRTF.GetComponent<UIPolygonalMesh>();
					polygonalLine.width = lineThickness;
					polygonalLine.color = lineColor;
					polygonalLine.points = lineList.ToArray();
*/
#endif
				}
				Spy.StopwatchesStop("LineGraph.UpdateCoroutine() CreateHorizontal");
				Spy.StopwatchesStart("LineGraph.UpdateCoroutine() CreateVertical");
				{   // Create vertical objects.
					/*					if(true)
										{
											var graphRenderer = GameObjectEx.InstantiateEmptyToChild<UIPolygonalLineFast>(gameObject);
											graphRenderer.material = graphMaterial;
											graphRenderer.width = 10;
											graphRenderer.points = new List<Vector2>();
											var corners = new Vector3[4];
											viewRTF.GetLocalCorners(corners);
											graphRenderer.points.Add(corners[0]);
											graphRenderer.points.Add(corners[1]);
											graphRenderer.points.Add(corners[2]);
											graphRenderer.points.Add(corners[3]);
										}*/

					var divisions = vMainDivisions * vSubDivisions;
					for (var idx = 0; idx < divisions + 1; ++idx)
					{
						var posV = scrollRect.y + (graphHeight / divisions) * idx;
						AxisConfig ac = (idx % vSubDivisions == 0) ? vMainAxisConfig : vSubAxisConfig;
//						if (ac.linePrefabPool != null)
						{   // Line.
							// Add vertical line data.(横線データの追加.)
							var ld = new UILines.UILineData(
								new Vector2(0, posV),
								new Vector2(scrollRect.width, posV),
								ac.lineThickness,
								ac.lineColor);
							lineData.Add(ld);
						}
						if (ac.textPrefabPool != null)
						{   // Text.
							// Create vertical text.
							var val = valueVMin + (valueVDiff / divisions) * idx;
							var textRTF = ac.textPrefabPool.Rent() as RectTransform;
							textRTF.anchorMin = Vector2.right;
							textRTF.anchorMax = Vector2.right;
							textRTF.pivot = Vector2.right;
							textRTF.anchoredPosition = new Vector2(ac.textOffset.x, posV + ac.textOffset.y);
							var text = textRTF.GetComponent<Text>();
							text.text = val.ToString("0." + new string('0', ac.fractionalDigits));
							text.color = ac.textColor;
						}
					}
				}
				Spy.StopwatchesStop("LineGraph.UpdateCoroutine() CreateVertical");
				Spy.StopwatchesStart("LineGraph.UpdateCoroutine() CreatePosArray");
				var posArray = new Vector2[valList.Count];
				{
					for (var idx = 0; idx < valList.Count; ++idx)
					{
						var val = valList[idx];
						var valH = val.Key;
						var valV = val.Value;
						posArray[idx] = new Vector2(
							scrollRect.y + (graphWidth / valueHDiff.Ticks) * (valH.Ticks - valueHMin.Ticks),
							scrollRect.y + (graphHeight / valueVDiff) * (valV - valueVMin));
					}
				}
				Spy.StopwatchesStop("LineGraph.UpdateCoroutine() CreatePosArray");



				//				Debug.Log("posArray.Length :"+ posArray.Length);
				//					posArray = posArray.ToList().GetRange(169, 14).ToArray();



				Spy.StopwatchesStart("LineGraph.UpdateCoroutine() CreateBackLine");
				if (lineData.Count != 0)
				{
					var uiLines = GameObjectEx.InstantiateEmptyToChild<UILines>(graphScrollRect.content.gameObject);
					uiLines.lineDataArray = lineData.ToArray();
					uiLines.raycastTarget = false;
					var polygonalRTF = uiLines.rectTransform;
					polygonalRTF.anchorMin = Vector2.zero;
					polygonalRTF.anchorMax = Vector2.zero;
					polygonalRTF.pivot = Vector2.zero;
					polygonalRTF.anchoredPosition = Vector2.zero;
				}
				Spy.StopwatchesStop("LineGraph.UpdateCoroutine() CreateBackLine");

				Spy.StopwatchesStart("LineGraph.UpdateCoroutine() CreateLine");
//graphLinePrefabPool = null;
				if (posArray.Length > 1 && graphLinePrefabPool != null)
				{   // Create line.
					const int MAX_VERTS = 64000 / 10;	// 本来65000/9ぐらいだが、バグが怖いので余裕を持っておく.

					/*
					 // テスト用に頂点を増やす.
											var a2 = new Vector2[posArray.Length];
											Array.Copy(posArray, a2, posArray.Length);
											var posList = posArray.ToList();
											while(posList.Count < MAX_VERTS / 4)
											{
												posList.AddRange(a2);

											}
											posArray = posList.ToArray();
					*/

					// 65000頂点までしか対応していない為、分割する.
					if (posArray.Length < MAX_VERTS)
					{
						Spy.StopwatchesStart("LineGraph.UpdateCoroutine() CreateLine A");
						var graphLineRTF = graphLinePrefabPool.Rent() as RectTransform;
						graphLineRTF.SetParent(graphScrollRect.content, false);
						graphLineRTF.anchorMin = Vector2.zero;
						graphLineRTF.anchorMax = Vector2.zero;
						graphLineRTF.pivot = Vector2.zero;
						graphLineRTF.anchoredPosition = Vector2.zero;
						var polygonalLine = graphLineRTF.GetComponent<UIPolygonalMesh>();
						polygonalLine.width = lineThickness;
						polygonalLine.color = lineColor;
						polygonalLine.points = posArray;
						Spy.StopwatchesStop("LineGraph.UpdateCoroutine() CreateLine A");
					}
					else
					{
						Spy.StopwatchesStart("LineGraph.UpdateCoroutine() CreateLine B");
						const int ChunkSize = MAX_VERTS;
						//						Debug.Log("posArray.Length:" + posArray.Length);
						var loopNum = (posArray.Length - 1) / ChunkSize + 1;
						for (var idx = 0; idx < loopNum; ++idx)
						{
							var offset = ChunkSize * idx;
							var count = (idx + 1 == loopNum) ? (posArray.Length - (idx * ChunkSize)) : ChunkSize;
							// つなぎ目の描画がきれいになるように2つ分多めに計算する.
							count = (offset + count + 2 <= posArray.Length) ? (count + 2) : (posArray.Length - offset);
#if false
							{   // .Net4.5以前なのでArraySegmentを活用できない.
								var arraySegment = new ArraySegment<Vector2>(posArray, offset, count);
								Debug.Log("idx:" + idx + ", arraySegment.Length:" + arraySegment.Count);
								for (int index = arraySegment.Offset; index <= arraySegment.Offset + arraySegment.Count - 1; index++)
								{
									Debug.Log(" vvv:" + arraySegment.Array[index]);
								}
							}
#endif
							var arraySeg = new Vector2[count];
							Array.Copy(posArray, offset, arraySeg, 0, count);

							var graphLineRTF = graphLinePrefabPool.Rent() as RectTransform;
							graphLineRTF.SetParent(graphScrollRect.content, false);
							graphLineRTF.anchorMin = Vector2.zero;
							graphLineRTF.anchorMax = Vector2.zero;
							graphLineRTF.pivot = Vector2.zero;
							graphLineRTF.anchoredPosition = Vector2.zero;
							var polygonalLine = graphLineRTF.GetComponent<UIPolygonalMesh>();
							polygonalLine.width = lineThickness;
							polygonalLine.color = lineColor;

							polygonalLine.points = arraySeg;
						}
						Spy.StopwatchesStop("LineGraph.UpdateCoroutine() CreateLine B");
					}
				}
				Spy.StopwatchesStop("LineGraph.UpdateCoroutine() CreateLine");

/*
				Spy.StopwatchesStart("LineGraph.UpdateCoroutine() CreatePoints");
				// Create points.
				if (pointPrefabPool != null)
				{
					foreach (var pos in posArray)
					{
						var pointRTF = pointPrefabPool.Rent() as RectTransform;
						pointRTF.anchorMin = Vector2.zero;
						pointRTF.anchorMax = Vector2.zero;
						pointRTF.pivot = new Vector2(0.5f, 0.5f);
						pointRTF.anchoredPosition = pos;
						pointRTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pointSize);
						pointRTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pointSize);
						pointRTF.GetComponent<Image>().color = pointColor;
#if UNITY_EDITOR
						pointRTF.GetComponent<Image>().color = ColorEx.Random;
#endif
					}
				}
				Spy.StopwatchesStop("LineGraph.UpdateCoroutine() CreatePoints");
*/

				// 開始時のスクロールバーの位置を設定する.
				graphScrollRect.normalizedPosition = Vector2.right;
				hTextScrollRect.normalizedPosition = Vector2.right;

//				Debug.Log("scrollRect.width:" + scrollRect.width);
//				Debug.Log("scrollRect.x:" + scrollRect.x);
//				Debug.Log("graphWidth:" + graphWidth);
//				Debug.Log("posArray[posArray.Length-1].x:" + posArray[posArray.Length-1].x);
//				Debug.Log("viewRTF:" + viewRTF.rect.xMin + ", " + viewRTF.rect.xMax);
//				Debug.Log("viewRTF.rect.width:" + viewRTF.rect.width);

				var r = (posArray[posArray.Length - 1].x - viewRTF.rect.width) / (scrollRect.width - viewRTF.rect.width);
				var normalizedPosition = new Vector2(r, 0);
				//				Debug.Log("r:" + r);
				graphScrollRect.normalizedPosition = normalizedPosition;
				hTextScrollRect.normalizedPosition = normalizedPosition;

			} while (needUpdate);
			updateCoroutine = null;
			yield break;

		}

		public ScrollRect CreateScrollView(string aName, RectTransform aRTF, bool aISCreateScrollbar)
		{
			// Create ScrollView.
			var scrollRectRTF = GameObject.Instantiate(aRTF, transform);
			var scrollRectGO = scrollRectRTF.gameObject;
			scrollRectGO.name = aName;
			var scrollRect = scrollRectGO.AddComponent<ScrollRect>();
			{	// Add Image component.
				scrollRectGO.AddComponent<Image>();
			}
			{	// Create viewport.
				var viewportGO = scrollRectGO.InstantiateEmptyToChild<Mask>("Viewport").gameObject;
				viewportGO.AddComponent<CanvasRenderer>();
				viewportGO.AddComponent<Image>();
				var viewportRTF = viewportGO.transform as RectTransform;
				viewportRTF.anchorMin = Vector2.zero;
				viewportRTF.anchorMax = Vector2.one;
				viewportRTF.pivot = new Vector2(0.5f, 0.5f);
				viewportRTF.offsetMin = Vector2.zero;
				viewportRTF.offsetMax = Vector2.zero;

				scrollRect.viewport = viewportRTF;
			}
			{	// Create Content.
				var contentRTF = scrollRect.viewport.gameObject.InstantiateEmptyToChild<RectTransform>("Content");
				contentRTF.anchorMin = Vector2.zero;
				contentRTF.anchorMax = Vector2.one;
				contentRTF.pivot = new Vector2(0.5f, 0.5f);
				contentRTF.offsetMin = Vector2.zero;
				contentRTF.offsetMax = Vector2.zero;
				scrollRect.content = contentRTF;
			}
			if (aISCreateScrollbar)
			{
				// Create ScrollbarVertical.
				var scrollbarV = scrollRectGO.InstantiateEmptyToChild<Scrollbar>("ScrollbarVertical");
				var scrollbarVRTF = scrollbarV.transform as RectTransform;
				scrollbarVRTF.anchorMin = new Vector2(1,0);
				scrollbarVRTF.anchorMax = Vector2.one;
				scrollbarVRTF.pivot = Vector2.one;
				scrollbarVRTF.offsetMin = Vector2.zero;
				scrollbarVRTF.offsetMax = Vector2.zero;
				scrollbarVRTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 18);
				scrollbarVRTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, aRTF.rect.height);
				scrollbarV.transition = Selectable.Transition.None;
				scrollbarV.handleRect = scrollbarV.transform as RectTransform;
				scrollbarV.direction = Scrollbar.Direction.BottomToTop;
				scrollRect.verticalScrollbar = scrollbarV;
				// Create ScrollbarHorizontal.
				var scrollbarH = scrollRectGO.InstantiateEmptyToChild<Scrollbar>("ScrollbarHorizontal");
				var scrollbarHRTF = scrollbarH.transform as RectTransform;
				scrollbarHRTF.anchorMin = new Vector2(1, 0);
				scrollbarHRTF.anchorMax = Vector2.one;
				scrollbarHRTF.pivot = Vector2.one;
				scrollbarHRTF.offsetMin = Vector2.zero;
				scrollbarHRTF.offsetMax = Vector2.zero;
				scrollbarHRTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, aRTF.rect.width);
				scrollbarHRTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 18);
				scrollbarH.transition = Selectable.Transition.None;
				scrollbarH.handleRect = scrollbarH.transform as RectTransform;
				scrollbarH.direction = Scrollbar.Direction.LeftToRight;
				scrollRect.horizontalScrollbar = scrollbarH;
			}
			return scrollRect;
		}
	}
}
