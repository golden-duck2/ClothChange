using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Jam;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenuText : DebugMenuBase
{
	public override void Build(string aText, Dictionary<string, object> aSettings)
	{
		base.Build(aText, aSettings);
		// DebugMenuButton<Image>.
		var bgImage = gameObject.AddComponent<Image>();
		bgImage.color = (Color) aSettings["BG_Color"];
		bgImage.enabled = false;
		// DebugMenuButton<RectTransform>.
		var rt = gameObject.GetRectTransform();
		rt.sizeDelta = (Vector2) aSettings["MinSize"];
		rt.anchorMin = Vector2.up;
		rt.anchorMax = Vector2.up;
		rt.pivot = Vector2.up;

		// DebugMenuText/UI.
		var uiGO = FindUI(aSettings);

		// DebugMenuText/UI/Text<Text>.
		var text = uiGO.InstantiateEmptyToChild<Text>("Text(" + aText + ")");
		text.text = aText;
		text.color = (Color) aSettings["Text_DisableColor"];
		text.font = aSettings["Text_Font"] as Font;
		text.fontSize = (int)aSettings["Font_Size"];
		text.alignment = TextAnchor.MiddleLeft;
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		text.verticalOverflow = VerticalWrapMode.Overflow;
		// DebugMenuText/UI/Text<RectTransform>.
		var textRT = text.transform.GetRectTransform();
		textRT.anchoredPosition = new Vector2(0f, 0f);
		textRT.sizeDelta = (Vector2) aSettings["MinSize"];
		textRT.anchorMin = new Vector2(0f, 0.5f);
		textRT.anchorMax = new Vector2(0f, 0.5f);
		textRT.pivot = new Vector2(0f, 0.5f);
		// DebugMenuText/UI/Text<ContentSizeFitter>.
		var uiContentSizeFitter = text.gameObject.AddComponent<ContentSizeFitter>();
		uiContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		uiContentSizeFitter.enabled = false;
		uiContentSizeFitter.enabled = true;
	}

	public string text
	{
		get{
			var uiTF = transform.Find("UI");
			var text = uiTF.GetComponentInChildren<Text>();
			return text.text;
		}
		set{
			var uiTF = transform.Find("UI");
			var text = uiTF.GetComponentInChildren<Text>();
			text.text = value;
		}
	}
}