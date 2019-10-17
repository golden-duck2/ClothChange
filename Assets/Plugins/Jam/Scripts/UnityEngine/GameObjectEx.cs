using UnityEngine;


//【Unity】Transform.SetParentでやってたことがUnity5.4だとInstantiateで指定できるようになったんですね。 
//  http://halcyonsystemblog.blog.fc2.com/blog-entry-330.html

namespace Jam
{
	/// <summary>
	/// GameObject extension.
	/// </summary>
	public static class GameObjectEx
	{
		/// <summary>
		/// Get RectTransform.
		/// </summary>
		public static RectTransform GetRectTransform(this GameObject aSelf)
		{
			return aSelf.transform as RectTransform;
		}

		/// <summary>
		/// Get an array of game objects.
		/// </summary>
		public static GameObject[] GetRootGameObjectsArray()
		{
			return UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
		}

		/// <summary>
		/// HierarchyのルートからインアクティブなGameObjectも対象にしたGameObject.Find().
		/// </summary>
		/// <param name="aPath"></param>
		/// <returns>GameObject.</returns>
		public static GameObject FindFromRoot(string aPath)
		{
			// 先頭に'/'がなければ追加する.
			//  GameObject.Find()は先頭に'/'がないものは、パスの途中も検索対象とするため.
			if (!aPath.StartsWith("/"))
			{
				aPath = "/" + aPath;
			}
			// 末尾に'/'があれば削除する.
			//		if (aPath.EndsWith("/"))
			//		{
			aPath = aPath.TrimEnd('/');
			//		}
			var ret = GameObject.Find(aPath);
			if (ret == null)
			{
				// GameObject.Find(aTargetObjectPath)ではインアクティブなGameObjectは見つけられないため、まずrootを見つけ、rootの子を探す.
				var pathArray = aPath.Split(new char[] { '/' });
				// pathArray[0] == ""
				// pathArray[1] == ルート
				// pathArray[2] == ルートの子
				//			Debug.Log("aPath:->"+ aPath+"<- " + aPath.Length);
				//			Debug.Log("pathArray:");
				//			foreach (var str in pathArray)
				//			{
				//				Debug.Log(" "+str+". " + str.Length);
				//			}
				//			Debug.Log("------------");
				if (pathArray.Length >= 3)
				{ // ルートに子がある.(先頭以外に'/'を含む階層構造になっている.)
					// Find the root.
					var rootName = "/" + pathArray[1];
					//				Debug.Log("rootName:" + rootName);
					var rootGO = GameObject.Find(rootName);
					if (rootGO != null)
					{ // Find the target from root.
						var targetObjectPath = aPath.Substring(rootName.Length + 1);
						//					Debug.Log("targetObjectPath:" + targetObjectPath);
						var tf = rootGO.transform.Find(targetObjectPath);
						if (tf != null)
						{
							ret = tf.gameObject;
						}
					}
				}
			}
			return ret;
		}

		static public T GetOrAddComponent<T>(this GameObject aGO) where T : Component
		{
			var ret = aGO.GetComponent<T>();
			if (ret == null)
			{
				ret = aGO.AddComponent<T>();
			}
			return ret;
		}

		/// <summary>
		/// Instantiate a GameObject and add it to the specified parent.
		/// </summary>
		static public GameObject InstantiateEmptyToChild(this GameObject aParent, string aInstanceName = null)
		{
			var go = InstantiateEmptyToRoot(aInstanceName);
			if (aParent != null)
			{
				Transform t = go.transform;
				t.parent = aParent.transform;
				t.localPosition = Vector3.zero;
				t.localRotation = Quaternion.identity;
				t.localScale = Vector3.one;
				go.layer = aParent.layer;
			}
			return go;
		}

		/// <summary>
		/// Instantiate a GameObject and add it to the specified parent and attaches the specified script to it.
		/// </summary>
		static public T InstantiateEmptyToChild<T>(this GameObject aParent, string aInstanceName = null) where T : Component
		{
			var go = aParent.InstantiateEmptyToChild(string.IsNullOrEmpty(aInstanceName) ? Util.GetNameOfType<T>() : aInstanceName);
			return go.AddComponent<T>();
		}

		/// <summary>
		/// Instantiate a GameObject and add it to the root.
		/// </summary>
		static public GameObject InstantiateEmptyToRoot(string aInstanceName = null)
		{
			var go = new GameObject(aInstanceName);
			return go;
		}

		/// <summary>
		/// Instantiate a GameObject and add it to the root and attaches the specified script to it.
		/// </summary>
		static public T InstantiateEmptyToRoot<T>(string aName = null) where T : Component
		{
			var go = InstantiateEmptyToRoot(string.IsNullOrEmpty(aName) ? Util.GetNameOfType<T>() : aName);
			return go.AddComponent<T>();
		}

		/// <summary>
		/// Instantiate a Prefab and add it to the specified parent.
		/// </summary>
		static public GameObject InstantiatePrefabToChild(this GameObject aParent, GameObject aPrefab)
		{
			/*		if (aParent == null)
					{
						Debug.LogError("aParent is null.");
					}
					var go = InstantiatePrefabToRoot(aPrefab, aPrefab.name);
					if (go != null && aParent != null)
					{
						Transform t = go.transform;
						t.SetParent(aParent.transform);
						t.localPosition = Vector3.zero;
						t.localRotation = Quaternion.identity;
						t.localScale = Vector3.one;
						go.layer = aParent.layer;
					}
					return go;
			*/
			if (aParent == null)
			{
				Debug.LogError("aParent is null.");
				return InstantiatePrefabToRoot(aParent);
			}
			var go = GameObject.Instantiate<GameObject>(aPrefab, aParent.transform);
			if (go != null && aParent != null)
			{
				Transform t = go.transform;
				//			t.SetParent(aParent.transform);
				t.localPosition = Vector3.zero;
				t.localRotation = Quaternion.identity;
				t.localScale = Vector3.one;
				go.layer = aParent.layer;
			}
			return go;
		}

		/// <summary>
		/// Instantiate a Prefab and add it to the root.
		/// </summary>
		static public GameObject InstantiatePrefabToRoot(GameObject aPrefab, string aInstanceName = null)
		{
			var go = GameObject.Instantiate(aPrefab) as GameObject;
			//		if (go != null)// && !string.IsNullOrEmpty(aInstanceName))
			//		{
			//			go.name = aInstanceName;
			//		}
			if (go != null)
			{
				if (string.IsNullOrEmpty(aInstanceName))
				{
					go.name = aPrefab.name;
				}
				else
				{
					go.name = aInstanceName;
				}
			}
			return go;
		}
		/*
		public static void Test()
		{
			var targetName = "Main Camera";
			var targetGO = GameObjectEx.FindFromRoot(targetName);
			Debug.Log("GameObjectEx.FindFromRoot(" + targetName + ") returns " + ((targetGO == null) ? "null" : targetGO.transform.GetPath()) + ".");

			targetName = "/Main Camera";
			targetGO = GameObjectEx.FindFromRoot(targetName);
			Debug.Log("GameObjectEx.FindFromRoot(" + targetName + ") returns " + ((targetGO == null) ? "null" : targetGO.transform.GetPath()) + ".");

			targetName = "/Main Camera/Canvas";
			targetGO = GameObjectEx.FindFromRoot(targetName);
			Debug.Log("GameObjectEx.FindFromRoot(" + targetName + ") returns " + ((targetGO == null) ? "null" : targetGO.transform.GetPath()) + ".");

			targetName = "Main Camera";
			targetGO = GameObject.Find(targetName);
			Debug.Log("GameObject.Find(" + targetName + ") returns " + ((targetGO == null) ? "null" : targetGO.transform.GetPath()) + ".");
		}
		*/
	}
}