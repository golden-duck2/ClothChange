using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    /// <summary>
    /// Editor constant parameters.
    /// </summary>
    public static class EditorConstParams
    {
        // CreateLayerDefine.
        public const string CreateLayerDefine_FILENAME = "LayerDefine.cs";                                      // ファイル名.
        public const string CreateLayerDefine_PATH = "Assets/" + CreateLayerDefine_FILENAME;                    // ファイルパス.
        public const string CreateLayerDefine_MENU_ITEM_NAME = "Tools/Jam/Create " + CreateLayerDefine_FILENAME;// アイテム名.
        public const int CreateLayerDefine_MENU_PRIORITY = 2;                                                   // プライオリティ.

        // LogMenu.LogHierarchyTree.
        public const string LogHierarchyTree_FILE_PATH = "Assets/../Output/hierarchyTree.txt";                 // ファイルパス.
        public const string LogHierarchyTree_MENU_ITEM_NAME = "Tools/Jam/Log/HierarchyTree";                    // アイテム名.
        public const int LogHierarchyTree_MENU_PRIORITY = 3;                                                    // プライオリティ.

        // Unity2019辺りから動かないので封印する.
        /*
				// StopPlayingAtCompile.
				public const string StopPlayingAtCompile_USE_KEY = "StopPlayingAtCompile_USE";
				public const string StopPlayingAtCompile_MENU_ITEM_NAME = "Tools/Jam/Stop playing at compile";
		    */

    }
}
