using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// ※参考
//  https://bitbucket.org/Unity-Technologies/ui/src/0bd08e22bc17bdf80bf7b997a4b43877ae4ee9ac/UnityEngine.UI/UI/Core/DefaultControls.cs?at=5.2&fileviewer=file-view-default
//  https://bitbucket.org/Unity-Technologies/ui/src/0bd08e22bc17bdf80bf7b997a4b43877ae4ee9ac/UnityEditor.UI/UI/MenuOptions.cs?at=5.2&fileviewer=file-view-default

namespace Jam
{
	/// <summary>
	/// DefaultControls extension.
	/// ※実機でも実行可能だが、デバグでのみ使用すること.
	/// </summary>
	public static class DefaultControlsEx
	{
		public static GameObject CreateButton(Transform aParent)
		{
			var go = DefaultControls.CreateButton(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateDropdown(Transform aParent)
		{
			var go = DefaultControls.CreateDropdown(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateImage(Transform aParent)
		{
			var go = DefaultControls.CreateImage(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateInputField(Transform aParent)
		{
			var go = DefaultControls.CreateInputField(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreatePanel(Transform aParent)
		{
			var go = DefaultControls.CreatePanel(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateRawImage(Transform aParent)
		{
			var go = DefaultControls.CreateRawImage(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateScrollbar(Transform aParent)
		{
			var go = DefaultControls.CreateScrollbar(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateScrollView(Transform aParent)
		{
			var go = DefaultControls.CreateScrollView(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateSlider(Transform aParent)
		{
			var go = DefaultControls.CreateSlider(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateText(Transform aParent)
		{
			var go = DefaultControls.CreateText(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		public static GameObject CreateToggle(Transform aParent)
		{
			var go = DefaultControls.CreateToggle(GetStandardResources());
			go.transform.SetParent(aParent);
			return go;
		}

		private const string kStandardSpritePath = "UISprite";
		private const string kBackgroundSpritePath = "Background";
		private const string kInputFieldBackgroundPath = "InputFieldBackground";
		private const string kKnobPath = "Knob";
		private const string kCheckmarkPath = "Checkmark";
		private const string kDropdownArrowPath = "DropdownArrow";
		private const string kMaskPath = "UIMask";
		static private DefaultControls.Resources s_StandardResources;

		static private DefaultControls.Resources GetStandardResources()
		{
			if (s_StandardResources.standard == null)
			{
				// ※実行時に高速に検索する手段が見つからないため、デバグでのみ使用することとする.
				var spriteArray = Resources.FindObjectsOfTypeAll<Sprite>();
				var targetNameList = new List<string>();
				targetNameList.Add(kStandardSpritePath);
				targetNameList.Add(kBackgroundSpritePath);
				targetNameList.Add(kInputFieldBackgroundPath);
				targetNameList.Add(kKnobPath);
				targetNameList.Add(kCheckmarkPath);
				targetNameList.Add(kDropdownArrowPath);
				targetNameList.Add(kMaskPath);
				foreach (var sprite in spriteArray)
				{
					var sn = sprite.name;
					foreach (var tn in targetNameList)
					{
						if (sn.Equals(tn))
						{   // Found!
							switch (tn)
							{
								case kStandardSpritePath:
									s_StandardResources.standard = sprite;
									break;
								case kBackgroundSpritePath:
									s_StandardResources.background = sprite;
									break;
								case kInputFieldBackgroundPath:
									s_StandardResources.inputField = sprite;
									break;
								case kKnobPath:
									s_StandardResources.knob = sprite;
									break;
								case kCheckmarkPath:
									s_StandardResources.checkmark = sprite;
									break;
								case kDropdownArrowPath:
									s_StandardResources.dropdown = sprite;
									break;
								case kMaskPath:
									s_StandardResources.mask = sprite;
									break;
							}
							targetNameList.Remove(tn);
							break;
						}
					}
					if (targetNameList.Count == 0)
					{
						break;
					}
				}
				if (s_StandardResources.standard == null)
				{
					Debug.LogWarning("s_StandardResources.standard is null.");
				}
				if (s_StandardResources.background == null)
				{
					Debug.LogWarning("s_StandardResources.background is null.");
				}
				if (s_StandardResources.inputField == null)
				{
					Debug.LogWarning("s_StandardResources.inputField is null.");
				}
				if (s_StandardResources.knob == null)
				{
					Debug.LogWarning("s_StandardResources.knob is null.");
				}
				if (s_StandardResources.checkmark == null)
				{
					Debug.LogWarning("s_StandardResources.checkmark is null.");
				}
				if (s_StandardResources.dropdown == null)
				{
					Debug.LogWarning("s_StandardResources.dropdown is null.");
				}
				if (s_StandardResources.mask == null)
				{
					Debug.LogWarning("s_StandardResources.mask is null.");
				}

			}
			return s_StandardResources;
		}
	}
}