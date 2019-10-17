using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace Jam
{
	/// <summary>
	/// ScreenShot.
	/// </summary>
	public static class ScreenShot
	{
		/// <summary>
		/// キャプチャを取りファイル保存する.
		/// </summary>
		/// <param name="fileName"></param>
		public static void SaveToFile(string fileName = "")
		{
			if (string.IsNullOrEmpty(fileName))
			{
#if UNITY_EDITOR
				fileName = "./Output/ScreenShot/";
#endif
				// 現在時刻からファイル名を決定
				fileName += DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

			}
			// フォルダを作成する.
			var parentDirPath = Directory.GetParent(fileName).FullName;
			DirectoryEx.CreateDirectoryIfNotExists(parentDirPath);
			// キャプチャを撮り、ファイル保存する.
#if UNITY_2017_1_OR_NEWER
			UnityEngine.ScreenCapture.CaptureScreenshot(fileName);
#else
			Application.CaptureScreenshot(filename);
#endif
			Debug.Log("@ScreenShot.SaveToFile() fileName:" + fileName);
		}
	}
}
