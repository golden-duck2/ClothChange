using System.Collections;
using UnityEngine;

namespace Jam
{
	/// <summary>
	/// グラフベース.
	/// </summary>
	public class GraphBase : MonoBehaviour
	{
		[SerializeField]
		protected RectTransform viewRTF;

		protected bool needUpdate = false;
		protected IEnumerator updateCoroutine = null;


		protected virtual void Awake()
		{
		}

//		public void SetVAxisInfo(int aMainDivisions, int aSubDivisions, float aMinValue, float aMaxValue)
//		{
//		}


		protected void RegistUpdate()
		{
			needUpdate = true;
		}

		private void LateUpdate()
		{
			if (needUpdate)
			{
				needUpdate = false;
				if (updateCoroutine == null)
				{
					// 更新コルーチンの開始.
					updateCoroutine = UpdateCoroutine();
					StartCoroutine(updateCoroutine);
				}
				else
				{
					Debug.LogError("updateCoroutine != null");
				}
			}

		}

		protected virtual IEnumerator UpdateCoroutine()
		{
			yield break;
		}
	}
}