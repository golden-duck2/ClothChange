using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
#if true
	/// <summary>
	/// Prefab pool.
	/// </summary>
	public class PrefabPool : ComponentPoolBase<Transform>
	{	
	}
#else
	/// <summary>
	/// Prefab pool.
	/// </summary>
	public class PrefabPool : MonoBehaviour
	{
		[SerializeField]
		GameObject prefab;// 生成するPrefab.
		[SerializeField]
		public Transform targetHierarchy;// 生成するGameObjectを配置するヒエラルキー.
		Transform poolHierarchy;// プールヒエラルキー.

		void Awake()
		{
			// プールを作成する.
			var poolHierarchyGO = gameObject.InstantiateEmptyToChild(prefab.name + "_pool");
			poolHierarchyGO.SetActive(false);
			poolHierarchy = poolHierarchyGO.transform;
		}

		// プールから借りる.
		public virtual Transform Rent()
		{
			// プールから借りる.
			Transform ret;
			if (poolHierarchy.childCount > 0)
			{   // プールされている.
				ret = poolHierarchy.GetChild(0);
				ret.SetParent(targetHierarchy);
			}
			else
			{	// プールされていないので生成する.
				ret = targetHierarchy.gameObject.InstantiatePrefabToChild(prefab).transform;
			}
			return ret;
		}

		// プールに返す.
		public virtual void Return(Transform aInstance)
		{
			aInstance.SetParent(poolHierarchy);
		}
	}
#endif
}
