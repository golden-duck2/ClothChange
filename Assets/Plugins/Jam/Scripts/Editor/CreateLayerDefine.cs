using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Jam
{
	public static class CreateLayerDefine
	{
		// 無効な文字を管理する配列
		static readonly string[] INVALUD_CHARS =
		{
			" ", "!", "\"", "#", "$",
			"%", "&", "\'", "(", ")",
			"-", "=", "^",  "~", "\\",
			"|", "[", "{",  "@", "`",
			"]", "}", ":",  "*", ";",
			"+", "/", "?",  ".", ">",
			",", "<"
		};

		/// <summary>
		/// スクリプトを作成する.
		/// </summary>
		public static void CreateScript()
		{
			var sb = StringBuilderEx.sb.Clear();
			sb.AppendLine("/// <summary>");
			sb.AppendLine("/// レイヤー定義.");
			sb.AppendLine("/// </summary>");
			sb.AppendFormatLine("public static class {0}", Path.GetFileNameWithoutExtension(EditorConstParams.CreateLayerDefine_FILENAME));
			sb.AppendLine("{");

			sb.AppendLine("\t// layer.");
			foreach (var n in InternalEditorUtility.layers.
				Select(c => new { var = RemoveInvalidChars(c), val = LayerMask.NameToLayer(c) }))
			{
				sb.Append("\t").AppendFormatLine(@"public const int {0} = {1};", n.var, n.val);
			}
			sb.AppendLine("\t// layer mask.");
			foreach (var n in InternalEditorUtility.layers.
				Select(c => new { var = RemoveInvalidChars(c), val = LayerMask.NameToLayer(c) }))
			{
				sb.Append("\t").AppendFormatLine(@"public const int {0}Mask = 1<<{1};", n.var, n.val);
			}
			sb.Append("\t").AppendLine(@"public const int AllMask = 0x7fffffff;");
			sb.AppendLine("}");

			var dirPath = Path.GetDirectoryName(EditorConstParams.CreateLayerDefine_PATH);
			// ディレクトリが存在しなければディレクトリを作成する.
			DirectoryEx.CreateDirectoryIfNotExists(dirPath);
			// ファイルに書き込む.
			File.WriteAllText(EditorConstParams.CreateLayerDefine_PATH, sb.ToString(), Encoding.UTF8);
			AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
		}

		/// <summary>
		/// 無効な文字を削除する.
		/// </summary>
		static string RemoveInvalidChars(string str)
		{
			Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
			return str;
		}
	}
}
