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


/*public class DebugMenuMB : SingletonBaseMBAI<DebugMenuMB>
    {
        public GameObject rootGO = null;
        public DebugMenuBase rootBase = null;
        //        RectTransform root = default;
        public static Dictionary<string, object> defaultSettings = new Dictionary<string, object>()
        {
            //             { "Font", Resources.FindObjectsOfTypeAll<Font>() [0] },
            { "MinSize", new Vector2(150, 20) }, { "BG_Color", new Color(0.5f, 0.5f, 0.5f, 0.8f) }, { "Text_Color", new Color(1f, 1f, 1f, 1f) }, { "key1", "value1" }
        };

        public override void Awake()
        {
            base.Awake();
            // Initialize members.
            rootGO = gameObject;
            // Initialize default settings.
            defaultSettings["Text_Font"] = Resources.FindObjectsOfTypeAll<Font>() [0];
            // DebugMenu-Canvas.
            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            // DebugMenu-GraphicRaycaster.
            //            var graphicRaycaster = 
            gameObject.AddComponent<GraphicRaycaster>();
            // DebugMenu-VerticalLayoutGroup.
            var verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.childForceExpandHeight = false;
            verticalLayoutGroup.childForceExpandWidth = false;
            verticalLayoutGroup.childControlHeight = false;
            verticalLayoutGroup.childControlWidth = false;

            // Build Root.
            var root = gameObject.InstantiateEmptyToChild<DebugMenuText>();
            root.Build("Root", defaultSettings);
            rootBase = root;
        }
    }*/

public class DebugMenu : SingletonBaseMBAI<DebugMenu>
//public static class DebugMenu
{
	public class HotkeyData
	{
		public KeyCode keyCode;
		public DebugMenuButton debugMenuButton;
		public HotkeyData(KeyCode keyCode, DebugMenuButton debugMenuButton)
		{
			this.keyCode = keyCode;
			this.debugMenuButton = debugMenuButton;
		}
	}
	public List<HotkeyData> hotkeyDataList = new List<HotkeyData>();
	static DebugMenuRoot _root = null;
	public DebugMenuRoot root
	{
		get
		{
			if (_root == null)
			{
				// DebugMenu-Canvas.
				var canvas = gameObject.AddComponent<Canvas>();
				//var canvas = GameObjectEx.InstantiateEmptyToRoot<Canvas>();
				//                DontDestroyOnLoad(canvas.gameObject);
				canvas.renderMode = RenderMode.ScreenSpaceOverlay;
				// DebugMenu-GraphicRaycaster.
				//            var graphicRaycaster = 
				var cs = canvas.gameObject.AddComponent<CanvasScaler>();
				cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				cs.referenceResolution = new Vector2(800, 600);
				canvas.gameObject.AddComponent<GraphicRaycaster>();
				//					// DebugMenu-VerticalLayoutGroup.
				//					var verticalLayoutGroup = canvas.gameObject.AddComponent<VerticalLayoutGroup>();
				//					verticalLayoutGroup.childForceExpandHeight = false;
				//					verticalLayoutGroup.childForceExpandWidth = false;
				//					verticalLayoutGroup.childControlHeight = false;
				//					verticalLayoutGroup.childControlWidth = true;

				// Build Root.
				_root = canvas.gameObject.InstantiateEmptyToChild<DebugMenuRoot>();
				_root.Build("DM", DebugMenuBase.defaultSettings);
			}
			return _root;
		}
	}

	void Start()
	{
		this.UpdateAsObservable().Subscribe(_ =>
		{
			foreach(var hotkeyData in hotkeyDataList)
			{
				if (Input.GetKeyDown(hotkeyData.keyCode))
				{
					var button = hotkeyData.debugMenuButton.gameObject.GetComponent<Button>();
					button.onClick.Invoke();
				}
			}
//			if (Input.GetKeyDown(KeyCode.Q))
//			{
//				ScreenShot.SaveToFile();
//			}
		});
	}
}
