using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Jam
{
	/// <summary>
	/// Log a hierarchy tree menu.
	/// </summary>
	public static class LogHierarchyTreeMenu
	{
		/// <summary>
		/// Log a hierarchy tree.
		/// </summary>
		[MenuItem(EditorConstParams.LogHierarchyTree_MENU_ITEM_NAME, validate = false, priority = EditorConstParams.LogHierarchyTree_MENU_PRIORITY)]
		public static void LogHierarchyTree()
		{
			if (!IsValidate())
			{
				return;
			}
			var hierarchyTreeString = TransformEx.ToStringHierarchyTree();
//hierarchyTreeString = TransformEx.ToStringHierarchyTree("", GameObjectEx.FindFromRoot("/Canvas/ViewAnchor/LoginView/LoginButton").transform);
			{
				var dirPath = Path.GetDirectoryName(EditorConstParams.LogHierarchyTree_FILE_PATH);
				// ディレクトリが存在しなければディレクトリを作成する.
				DirectoryEx.CreateDirectoryIfNotExists(dirPath);
			}
			File.WriteAllText(EditorConstParams.LogHierarchyTree_FILE_PATH, hierarchyTreeString, Encoding.UTF8);
		}

		/// <summary>
		/// 実行できるかを取得.
		/// </summary>
		[MenuItem(EditorConstParams.LogHierarchyTree_MENU_ITEM_NAME, true)]
		public static bool IsValidate()
		{
			return !EditorApplication.isCompiling;
		}
	}
}
