using UnityEngine;
using System.Collections;


namespace Jam
{
	/// <summary>
	/// Memo.
	///  Inspectorにメモを付ける.
	/// </summary>
	public class Memo : MonoBehaviour
	{
#if UNITY_EDITOR
//		[SerializeField, MultilineAttribute]
		[SerializeField, TextAttribute]
		public string memo = "";
#endif
	}
}