using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Jam
{
    /// <summary>
    /// Add AtlasImage menu.(Jpn.AtlasImageメニューの追加.)
    /// </summary>
    public static class AtlasImageMenu
    {
        static bool inprogress = false;

        [MenuItem("GameObject/UI/AtlasImage", validate = false, priority = 10)]
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
                go = GameObjectEx.InstantiateEmptyToChild<AtlasImage>(parent, "AtlasImage").gameObject;
            }
            else
            {
                go = GameObjectEx.InstantiateEmptyToRoot<AtlasImage>("AtlasImage").gameObject;
            }
            Selection.SetActiveObjectWithContext(go, null);
            inprogress = false;
        }
    }
}