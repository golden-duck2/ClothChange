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

// Debug menu button.
public class DebugMenuButton : DebugMenuBase
{
	public override void Build(string aText, Dictionary<string, object> aSettings)
	{
		base.Build(aText, aSettings);
		// DebugMenuButton-Image.
		var bgImage = gameObject.AddComponent<Image>();
		bgImage.transform.GetRectTransform().sizeDelta = (Vector2) aSettings["MinSize"];
		bgImage.rectTransform.anchorMin = Vector2.up;
		bgImage.rectTransform.anchorMax = Vector2.up;
		bgImage.rectTransform.pivot = Vector2.up;
		bgImage.color = (Color) aSettings["BG_Color"];
		// DebugMenuButton-Button.
		var button = gameObject.AddComponent<Button>();
		button.onClick.AddListener(() => ((Action<DebugMenuButton>) aSettings["OnButtonAction"]) (this));
		// DebugMenuButton-Text.
		var text = gameObject.InstantiateEmptyToChild<Text>();
		text.text = aText;
		var textRT = text.transform.GetRectTransform();
		textRT.sizeDelta = (Vector2) aSettings["MinSize"];
		textRT.anchoredPosition = new Vector2(10f, 0f);
		text.rectTransform.anchorMin = new Vector2(0f, 0.5f);
		text.rectTransform.anchorMax = new Vector2(0f, 0.5f);
		text.rectTransform.pivot = new Vector2(0f, 0.5f);
		text.color = (Color) aSettings["Text_Color"];
		text.font = aSettings["Text_Font"] as Font;
		text.fontSize = (int)aSettings["Font_Size"];
		text.alignment = TextAnchor.MiddleLeft;
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		text.verticalOverflow = VerticalWrapMode.Overflow;

		if (aSettings.ContainsKey("Hotkey"))
		{
			var keyCode = (KeyCode)aSettings["Hotkey"];
			var hotkeyData = new DebugMenu.HotkeyData(keyCode, this);
			DebugMenu.instance.hotkeyDataList.Add(hotkeyData);
		}
	}
}
