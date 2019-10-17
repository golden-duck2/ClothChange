using UnityEngine;
using UnityEditor;

// 動かないので封印する.
//  以前は動いていたが、コンパイル中に EditorApplication.update が呼ばれなくなったのかもしれない.
/*
namespace Jam
{
    /// <summary>
    /// Stop playing at the start of compilation.
    /// </summary>
    [InitializeOnLoad]
    public class StopPlayingAtCompile
    {
        static StopPlayingAtCompile()
        {
//            Debug.Log("at StopPlayingAtCompile.StopPlayingAtCompile()");
            var use = EditorPrefs.GetBool(JamConstEditorParams.StopPlayingAtCompile_USE_KEY, false);
            SetEnable(use);
        }

        public static void SetEnable(bool useFlag)
        {
//            Debug.Log("at StopPlayingAtCompile.SetEnable(useFlag:" + useFlag + ")");
            if (useFlag)
            {
                EditorApplication.update += Update;
            }
            else
            {
                EditorApplication.update -= Update;
            }
        }

        static void Update()
        {
//            Debug.Log("at Update() isCompiling:" + EditorApplication.isCompiling + ", isPlaying:" + EditorApplication.isPlaying);
            if (EditorApplication.isCompiling && EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                Debug.Log("Stop playing! because of isCompiling is true.");
            }
        }
    }
}
*/
