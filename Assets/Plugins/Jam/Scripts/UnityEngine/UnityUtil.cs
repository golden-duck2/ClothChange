using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Jam
{
	/// <summary>
	/// Unity utility.
	///  複数コードに "using UnityEditor" が入らないようにここで処理する.
	/// </summary>
	public static class UnityUtil
	{
		/// <summary>
		/// Get a active scene name.
		/// </summary>
		public static string GetSceneName()
		{
			return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		}

		/// <summary>
		/// Is it in play mode?
		///  ※Playボタンを押した直後の初期化処理中もしばらくfalseが来て、PLayボタンが押されたかの判定としては使用できない為注意する.
		///  ※Application.Quit()後もしばらくtrueが来て、Application.Quit()されたかの判定として使用できない為注意する.
		/// </summary>
		/// <value>
		/// true : 実行中.
		/// false : エディットモード.
		/// </value>
		public static bool isPlaying
		{
			get
			{
#if UNITY_EDITOR
				return Application.isPlaying;
				//			return Application.isPlaying || EditorApplication.isPlaying;
#else // UNITY_EDITOR
			return true;
#endif // UNITY_EDITOR
			}
		}

		public static void ApplicationQuit()
		{
			Application.Quit();
#if UNITY_EDITOR
			EditorApplication.ExecuteMenuItem("Edit/Play");
#endif // UNITY_EDITOR
		}
	}
}