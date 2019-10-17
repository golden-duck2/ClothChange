using UnityEngine;
using System.Collections.Generic;

namespace Jam
{
	public class InGameDataManager : SaveDataManagerBase<InGameDataManager>
	{
		public static void RegistPausable(PausableIF aPausable)
		{
			if (instanceImpl == null)
			{   // 初期化処理中とOnDestroy()後は抜ける.
				Debug.LogWarning("at InGameDataManager.RegistPausable() instance is null.");
				return;
			}
			instance.pausableList.Add(aPausable);
		}

		public static void UnregistPausable(PausableIF aPausable)
		{
			if (instanceImpl == null)
			{   // 初期化処理中とOnDestroy()後は抜ける.
				Debug.LogWarning("at InGameDataManager.UnregistPausable() instance is null.");
				return;
			}
			instance.pausableList.Remove(aPausable);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="aFlag"></param>
		public void SetPause(bool aFlag)
		{
			foreach (var pausable in pausableList)
			{
				pausable.SetPause(aFlag);
			}
		}

		List<PausableIF> pausableList = new List<PausableIF>();
	}
}
