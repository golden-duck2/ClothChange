#if UNITY_EDITOR
#define JAM_DEBUG_DETAIL
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
	/// <summary>
	/// Component pool base.
	/// </summary>
	public class ComponentPoolBase<T> : MonoBehaviour where T : Component
	{
		const string POOL_NAME = "[Jam.POOL]";

		[SerializeField]
		GameObject prefab;// 生成するPrefab.
		[SerializeField]
		public Transform targetHierarchy;// 生成するGameObjectを配置するヒエラルキー.
		Stack<T> poolStack;
		public List<T> rentedList;

		static Transform poolHierarchy;// プールヒエラルキー.
		static Dictionary<int, Stack<T>> poolStackDic = new Dictionary<int, Stack<T>>();

		public void SetValue(GameObject aPrefab, Transform aTargetHierarchy)
		{
			prefab = aPrefab;
			targetHierarchy = aTargetHierarchy;
		}

		void Awake()
		{
			// targetHierarchyが設定されていないときは自分を設定する.
			if (targetHierarchy == null)
			{
				targetHierarchy = transform;
			}
		}

		void Initialize()
		{
			// プールを用意する.
			if (poolHierarchy == null)
			{
				var poolGO = GameObjectEx.InstantiateEmptyToRoot(POOL_NAME);
				DontDestroyOnLoad(poolGO);
				poolGO.SetActive(false);
				poolHierarchy = poolGO.transform;
			}
			// プールスタックを用意する.
			var prefabInstanceID = prefab.GetInstanceID();
			if (poolStackDic.ContainsKey(prefabInstanceID))
			{
				poolStack = poolStackDic[prefabInstanceID];
			}
			else
			{
				poolStack = new Stack<T>();
				poolStackDic[prefabInstanceID] = poolStack;
			}
			rentedList = new List<T>();
		}

		// プールから借りる.
		public virtual T Rent(string aNewName="")
		{
			if (poolStack == null)
			{
				Initialize();
			}
			// プールから借りる.
			T t;
			if (poolStack .Count > 0)
			{   // プールされている.
				t = poolStack.Pop();
				t.transform.SetParent(targetHierarchy);
			}
			else
			{   // プールされていないので生成する.
				t = targetHierarchy.gameObject.InstantiatePrefabToChild(prefab).GetComponent<T>();
			}
#if JAM_DEBUG_DETAIL
			if (t == null)
			{
				Debug.LogWarning("t is null.");
			}
#endif
			t.transform.position = prefab.transform.position;
			t.transform.rotation = prefab.transform.rotation;
			t.transform.localScale = prefab.transform.localScale;
			if (!string.IsNullOrEmpty(aNewName))
			{
				t.name = aNewName;
			}
			rentedList.Add(t);
			return t;
		}

		// プールに返す.
		public virtual void Return(T aInstance)
		{
			if (poolStack == null)
			{
				Initialize();
			}
			poolStack.Push(aInstance);
			aInstance.transform.SetParent(poolHierarchy);
			rentedList.Remove(aInstance);
		}

		// プールに全て返す.
		public virtual void ReturnAll()
		{
			if (poolStack == null)
			{
				return;
			}
			foreach (var rented in rentedList)
			{
				poolStack.Push(rented);
				rented.transform.SetParent(poolHierarchy);
			}
			rentedList.Clear();
		}
	}
#if false
	/// <summary>
	/// Component pool base.
	/// </summary>
	public class ComponentPoolBase<T> : MonoBehaviour where T : UnityEngine.MonoBehaviour
	{
		class TPool : ObjectPool<T>
		{
			ComponentPoolBase<T> objectPoolBase;

			public TPool(ComponentPoolBase<T> aObjectPoolBase)
			{
				objectPoolBase = aObjectPoolBase;
			}

			// オブジェクトが空のときにInstantiateする関数
			protected override T CreateInstance()
			{
				//				if (typeof(T) is Transform)
				if (typeof(T) == typeof(Transform))
				{   // T is Transform.
					return GameObject.Instantiate(objectPoolBase.prefab, objectPoolBase.targetHierarchy).transform as T;
				}
				else
				{
					return GameObject.Instantiate(objectPoolBase.prefab, objectPoolBase.targetHierarchy).GetComponent<T>();
				}
			}
		}

		[SerializeField]
		GameObject prefab;// 生成するPrefab.
		[SerializeField]
		public Transform targetHierarchy;// 生成するGameObjectを配置するヒエラルキー.

		TPool tPool;

		void Awake()
		{
			tPool = new TPool(this);
		}

		// プールから借りる.
		public virtual T Rent()
		{
			// プールから借りる.
			var instance = tPool.Rent();
			return instance;
		}

		// プールに返す.
		public virtual void Return(T aInstance)
		{
			tPool.Return(aInstance);
		}
	}
#endif
}
