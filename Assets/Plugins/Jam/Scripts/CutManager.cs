using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
	/// <summary>
	/// Cut manager.
	/// </summary>
	public class CutManager : SingletonBaseMB<CutManager>
	{
		public static void Regist(CutBase aCutBase)
		{
			instance.cutDic.Add(aCutBase.name, aCutBase);
		}


		protected override void Awake()
		{
//			DontDestroyOnLoad(gameObject);
		}

		Dictionary<string, CutBase> cutDic = new Dictionary<string, CutBase>();

	}
}
