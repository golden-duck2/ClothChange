using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Jam
{
	/// <summary>
	/// Memo.
	///  Inspectorにメモを付ける.
	/// </summary>
	public class DebugStringMemo : MonoBehaviour
	{
#if UNITY_EDITOR
		[SerializeField, TextAttribute]
		public string memo = "";

		private void OnEnable()
		{
			var rtf = transform as RectTransform;
			memo = rtf.ToDebugString();
		}
#endif
	}
}