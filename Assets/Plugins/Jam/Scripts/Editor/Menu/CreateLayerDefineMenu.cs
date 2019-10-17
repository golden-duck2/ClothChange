using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Jam
{
	/// <summary>
	/// Create layer define menu.
	/// </summary>
	public static class CreateLayerDefineMenu
	{
		/// <summary>
		/// レイヤー名を定数で管理するクラスを作成します
		/// </summary>
		[MenuItem(EditorConstParams.CreateLayerDefine_MENU_ITEM_NAME, validate = false, priority = EditorConstParams.CreateLayerDefine_MENU_PRIORITY)]
		public static void Create()
		{
			if (!CanCreate())
			{
				return;
			}

			CreateLayerDefine.CreateScript();

			EditorUtility.DisplayDialog(EditorConstParams.CreateLayerDefine_FILENAME, "作成が完了しました", "OK");
		}

		/// <summary>
		/// ファイルを作成できるか取得.
		/// </summary>
		[MenuItem(EditorConstParams.CreateLayerDefine_MENU_ITEM_NAME, true)]
		public static bool CanCreate()
		{
			return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
		}
	}
}
