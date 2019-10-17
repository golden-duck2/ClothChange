using UnityEngine;
using UnityEditor;

// 動かないので封印する.
//  以前は動いていたが、コンパイル中に EditorApplication.update が呼ばれなくなったのかもしれない.
/*
namespace Jam
{
    /// <summary>
    /// Stop playing at the start of compilation menu.
    /// </summary>
    public static class StopPlayingAtCompileMenu
    {
		/// <summary>
		/// デバグに設定を変更する.
		/// </summary>
		[MenuItem(JamConstEditorParams.StopPlayingAtCompile_MENU_ITEM_NAME, false)]
        public static void SwitchParameter()
        {
            var use = EditorPrefs.GetBool(JamConstEditorParams.StopPlayingAtCompile_USE_KEY, false);
            EditorPrefs.SetBool(JamConstEditorParams.StopPlayingAtCompile_USE_KEY, !use);
            StopPlayingAtCompile.SetEnable(!use);
        }

        [MenuItem(JamConstEditorParams.StopPlayingAtCompile_MENU_ITEM_NAME, true)]
        public static bool IsValidateStopPlayingAtCompile()
        {
            // メニューのチェックマークを変更.
            //  ※メニュー表示時にチェックさせるためにここで実装.
            var use = EditorPrefs.GetBool(JamConstEditorParams.StopPlayingAtCompile_USE_KEY, false);
            Menu.SetChecked(JamConstEditorParams.StopPlayingAtCompile_MENU_ITEM_NAME, use);
            return true;
        }
    }
}
*/
