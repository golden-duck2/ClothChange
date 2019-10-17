using UnityEngine;

// Awake()の前にインスタンスにアクセスでき、問題が起きやすいのでデバグにのみ使用する.
#if RELEASE
#else
namespace Jam
{
	/// <summary>
	/// Singleton Base MonoBehaviour auto instance.
	/// </summary>
	/// <remarks>
	/// アプリ内で一度インスタンス化したら削除しないマネージャのようなものに使用する.
	/// </remarks>
	public class SingletonBaseMBAI<T> : MonoBehaviour
	where T : UnityEngine.Component
	{
		static T instanceImpl;
		static bool _isApplicationQuitting = false; // アプリケーションの終了処理中フラグ.

		public static bool isApplicationQuitting // アプリケーションの終了処理中フラグ.
		{
			get
			{
				return _isApplicationQuitting;
			}
		}

		public static T instance
		{
			get
			{
				if (instanceImpl == null && !isApplicationQuitting)
				{
					instanceImpl = FindObjectOfType<T>();
					if (instanceImpl == null)
					{
						GameObjectEx.InstantiateEmptyToRoot<T>();
					}
				}
				return instanceImpl;
			}
		}

		public virtual void Awake()
		{
			if (instanceImpl == null)
			{
				instanceImpl = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				if (instanceImpl != this)
				{
					Debug.LogWarning("There are multiple same type instances. Type:" + typeof(T).ToString());
					Destroy(gameObject);
				}
			}
		}

		void OnDestroy()
		{
			if (instanceImpl == this)
			{
				instanceImpl = null;
			}
		}

		void OnApplicationQuit()
		{
			_isApplicationQuitting = true;
		}
	}
}
#endif