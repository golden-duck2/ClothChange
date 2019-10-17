using System.IO;
using UnityEngine;


namespace Jam
{
	/// <summary>
	/// Directory extension.
	/// </summary>
	public static class DirectoryEx
	{
		/// <summary>
		/// ディレクトリが存在しなければディレクトリを作成する.
		/// </summary>
		/// <param name="path"></param>
		public static void CreateDirectoryIfNotExists(string path)
		{
			if (!Directory.Exists(path))
			{   // ディレクトリが存在しない.
				// ディレクトリを作成する.
				Directory.CreateDirectory(path);
			}
		}
	}
}
