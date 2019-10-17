using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Jam
{
    /// <summary>
    /// Add AtlasButton menu.(Jpn.AtlasButtonメニューの追加.)
    /// </summary>
    public static class AtlasButtonMenu
    {
        static bool inprogress = false;

        [MenuItem("GameObject/UI/AtlasButton", validate = false, priority = 10)]
        public static void Add()
        {
            if (inprogress)
            {
                return;
            }
            inprogress = true;
            GameObject go;
            if (Selection.gameObjects.Length >= 1)
            {
                var parent = Selection.gameObjects[0];
                go = GameObjectEx.InstantiateEmptyToChild<AtlasImage>(parent, "AtlasButton").gameObject;
            }
            else
            {
                go = GameObjectEx.InstantiateEmptyToRoot<AtlasImage>("AtlasButton").gameObject;
            }
            go.GetRectTransform().sizeDelta = new Vector2(160, 30);
            go.AddComponent<Button>();
            var text = GameObjectEx.InstantiateEmptyToChild<Text>(go, "Text");
            text.text = "Button";
            text.alignment = TextAnchor.MiddleCenter;
            text.color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255);
            var textRT = text.rectTransform;
            textRT.sizeDelta = Vector2.zero;
            textRT.anchorMin = Vector2.zero;
            textRT.anchorMax = Vector2.one;
            Selection.SetActiveObjectWithContext(go, null);
            inprogress = false;
        }
    }
}