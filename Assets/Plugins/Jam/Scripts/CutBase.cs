using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
	public class CutBase : MonoBehaviour
	{
		void Start()
		{
			CutManager.Regist(this);
		}
	}
}
