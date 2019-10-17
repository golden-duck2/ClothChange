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


public class DebugMenuBase : MonoBehaviour
{
	protected const float MENU_OPEN_DELAY = 0.3f;
	static Dictionary<string, object> _defaultSettings = null;

	public static Dictionary<string, object> defaultSettings
	{
		get
		{
			if (_defaultSettings == null)
			{
				// Initialize default settings.
				_defaultSettings = new Dictionary<string, object>()
				{
					//             { "Font", Resources.FindObjectsOfTypeAll<Font>() [0] },
					{ "MinSize", new Vector2(120, 60) },
					{ "BG_Color", new Color(0.5f, 0.5f, 0.5f, 0.8f) },
					{ "Font_Size", 32 },
					{ "Text_Color", new Color(1f, 1f, 1f, 1f) },
					{ "Text_DisableColor", new Color(0.8f, 0.8f, 0.8f, 1f) },
					{ "key1", "value1" }
				};
//				var sw = Screen.width;

				_defaultSettings["Text_Font"] = Resources.FindObjectsOfTypeAll<Font>() [0];
			}
			return _defaultSettings;
		}
	}

	/* 
		public DebugMenuFolder folder
		{
			get
			{
				var folder = GetComponent<DebugMenuFolder>();
				if (folder == null)
				{
					folder = CreateFolder(defaultSettings);
				}
				return folder;
			}
		}*/

	public virtual void Build(string aText, Dictionary<string, object> aSettings)
	{
		if (aSettings.ContainsKey("OnEnableAction"))
		{
			onEnableAction = (Action<DebugMenuBase>)aSettings["OnEnableAction"];
		}
	}

	public DebugMenuText CreateText(string aText, Dictionary<string, object> aSettings = null)
	{
		// Mearge settings.
		var settings = MeargeSettings(aSettings);
		// Find folder GameObject.
		var folderGO = FindFolder(settings);
		// Build DebugMenu-DebugMenuText.
		var debugMenuText = folderGO.InstantiateEmptyToChild<DebugMenuText>("DebugMenuText(" + aText + ")");
		debugMenuText.Build(aText, settings);
		// position.
		var debugMenuTextRT = debugMenuText.GetRectTransform();
		var rt = gameObject.GetRectTransform();
		debugMenuTextRT.anchoredPosition = new Vector2(rt.sizeDelta.x, 0f);
		return debugMenuText;
	}

	public DebugMenuButton CreateButton(string aText, Dictionary<string, object> aSettings = null)
	{
		// Mearge settings.
		var settings = MeargeSettings(aSettings);
		// Find folder GameObject.
		var folderGO = FindFolder(settings);
		// Build DebugMenu-DebugMenuText.
		var debugMenuButton = folderGO.InstantiateEmptyToChild<DebugMenuButton>("DebugMenuButton(" + aText + ")");
		debugMenuButton.Build(aText, settings);
		// position.
		var debugMenuButtonRT = debugMenuButton.GetRectTransform();
		var rt = gameObject.GetRectTransform();
		debugMenuButtonRT.anchoredPosition = new Vector2(rt.sizeDelta.x, 0f);
		return debugMenuButton;
	}

	GameObject FindFolder(Dictionary<string, object> aSettings)
	{
		var folderTF = transform.Find("Folder");
		if (folderTF != null)
		{ // 既にFolderが存在するので返す.
			return folderTF.gameObject;
		}
		// Folderを作成する.
		var minSize = (Vector2) aSettings["MinSize"];
		// Folder.
		var folderGO = gameObject.InstantiateEmptyToChild("Folder");
		// Folder<VerticalLayoutGroup>.
		var verticalLayoutGroup = folderGO.AddComponent<VerticalLayoutGroup>();
		verticalLayoutGroup.childForceExpandHeight = false;
		verticalLayoutGroup.childForceExpandWidth = false;
		verticalLayoutGroup.childControlHeight = false;
		verticalLayoutGroup.childControlWidth = false;
		// Folder<CanvasGroup>.
		var folderCanvasGroup = folderGO.AddComponent<CanvasGroup>();
		// Folder<RectTransform>.
		var folderRT = folderGO.transform as RectTransform;
		folderRT.sizeDelta = (Vector2) aSettings["MinSize"];
		folderRT.anchorMin = Vector2.up;
		folderRT.anchorMax = Vector2.up;
		folderRT.pivot = Vector2.up;
		folderRT.anchoredPosition = new Vector2(transform.GetRectTransform().sizeDelta.x, 0f);

		folderGO.SetActive(false);

		// Folder/parent/UI/FolderArrowButton<Image>.
		var uiRT = transform.Find("UI").GetRectTransform();
		var FolderArrowButtonImage = uiRT.gameObject.InstantiateEmptyToChild<Image>("FolderArrowButton");
		FolderArrowButtonImage.color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
		var folderArrowButtonRT = FolderArrowButtonImage.rectTransform;
		folderArrowButtonRT.sizeDelta = new Vector2(minSize.y, minSize.y);
		folderArrowButtonRT.anchorMin = new Vector2(1f, 0.5f);
		folderArrowButtonRT.anchorMax = new Vector2(1f, 0.5f);
		folderArrowButtonRT.pivot = new Vector2(1f, 0.5f);
		// Folder/parent/UI/FolderArrowButton<Button>.
		var folderArrowButtonButton = FolderArrowButtonImage.gameObject.AddComponent<Button>();
		folderArrowButtonButton.onClick.AddListener(() =>
		{
			folderArrowButtonButton.interactable = false;
			if (folderGO.activeSelf)
			{ // 表示から非表示へ.
				folderCanvasGroup.DOFade(0f, MENU_OPEN_DELAY)
					.OnComplete(() =>
					{
						folderGO.SetActive(false);
						folderArrowButtonButton.interactable = true;
					});
			}
			else
			{ // 非表示から表示へ.
				folderGO.SetActive(true);
				folderCanvasGroup.alpha = 0.0f;
				folderCanvasGroup.DOFade(1f, MENU_OPEN_DELAY)
					.OnComplete(() =>
					{
						folderArrowButtonButton.interactable = true;
					});
				folderRT.anchoredPosition = new Vector2(LayoutUtility.GetPreferredWidth(uiRT), 0f); // 位置をUIのPreferredWidthへ移動する.
				// Folder.children.UI<ContentSizeFitter>.SetDirty()が呼ばれるタイミングが悪いのでOnEnable()で強制的にSetDirty()する.
				foreach (Transform folderChild in folderRT)
				{
					var uiTF = folderChild.Find("UI");
					if (uiTF != null)
					{
						var uiContentSizeFitter = uiTF.GetComponent<ContentSizeFitter>();
						uiContentSizeFitter.enabled = false;
						this.StartCoroutineActionDelayOneFrame(() =>
						{
							uiContentSizeFitter.enabled = true;
						});
					}
				}

			}
		});
		return folderGO;
	}

	protected GameObject FindUI(Dictionary<string, object> aSettings)
	{
		var uiTF = transform.Find("UI");
		if (uiTF != null)
		{ // 既にUIが存在するので返す.
			return uiTF.gameObject;
		}
		// UIを作成する.
		var minSize = (Vector2) aSettings["MinSize"];
		// UI.
		var uiGO = gameObject.InstantiateEmptyToChild("UI");
		// UI<Image>.
		var uiImage = uiGO.AddComponent<Image>();
		uiImage.color = (Color) aSettings["BG_Color"];
		// UI<LayoutElement>
		var layoutElement = uiGO.AddComponent<LayoutElement>();
		// UI<HorizontalLayoutGroup>.
		var horizontalLayoutGroup = uiGO.AddComponent<HorizontalLayoutGroup>();
		horizontalLayoutGroup.childForceExpandHeight = false;
		horizontalLayoutGroup.childForceExpandWidth = false;
		horizontalLayoutGroup.childControlHeight = false;
		horizontalLayoutGroup.childControlWidth = false;
		// UI<ContentSizeFitter>.
		var uiContentSizeFitter = uiGO.AddComponent<ContentSizeFitter>();
		uiContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		// UI<RectTransform>
		var uiRT = uiGO.transform as RectTransform;
		uiRT.anchoredPosition = Vector2.zero;
		uiRT.sizeDelta = minSize; // sizeDelta.x:width:はContentSizeFitterにより指定される.
		uiRT.anchorMin = Vector2.up;
		uiRT.anchorMax = Vector2.up;
		uiRT.pivot = Vector2.up;
		return uiGO;
	}

	Action<DebugMenuBase> onEnableAction = null;
	void OnEnable()
	{
		if(onEnableAction != null)
		{
			onEnableAction(this);
		}
	}

	Dictionary<string, object> MeargeSettings(Dictionary<string, object> aSettings)
	{
		var settings = (aSettings == null)
			? defaultSettings
			: aSettings
			.Concat(defaultSettings.Where(pair => !aSettings.ContainsKey(pair.Key)))
			.ToDictionary(pair => pair.Key, pair => pair.Value);
		return settings;
	}
}