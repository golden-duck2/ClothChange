using UnityEngine;

namespace Jam
{
	/// <summary>
	/// Singleton Base MonoBehaviour.
	/// </summary>
	/// <remarks>
	/// シーンに一つだけインスタンスがある状態で使用する.
	/// </remarks>
	public class SingletonBaseMB<T> : MonoBehaviour
	where T : UnityEngine.Component
	{
		protected static T instanceImpl;

		/// <summary>
		/// Get the instance.
		/// </summary>
		public static T instance
		{
			get
			{
				if (instanceImpl == null)
				{
					Debug.LogWarning("at T:" + typeof(T) + ".instance. instance is null.");
				}
				return instanceImpl;
			}
		}

		/// <summary>
		/// Get the instance.(Check the hierarchy.)
		/// </summary>
		/// ※Awake()が呼ばれる前にinstanceにアクセスできるため、EditModeでも呼ばれる関数にのみ使用する.
		public static T instanceCH
		{
			get
			{
				if (instanceImpl == null)
				{
					var ins = FindObjectOfType<T>();
					if (ins == null)
					{ // instance is null.
						Debug.LogWarning("instance is null.");
					}
					return ins;
				}
				return instanceImpl;
			}
		}

		protected virtual void Awake()
		{
			if (instanceImpl != null)
			{
				Debug.LogError("at T:" + typeof(T) + ".Awake(). instance is already registed. A:" + instanceImpl.transform.GetPath() + ", B:" + transform.GetPath());
				return;
			}
			instanceImpl = this as T;
		}

		//		protected virtual void Start()
		//		{
		//		}

		//		protected virtual void Update()
		//		{
		//		}

		protected virtual void OnDestroy()
		{
			//		Debug.Log("at SingletonBaseMB.OnDestroy() end. instance:" + instanceImpl.gameObject);
			instanceImpl = null;
		}
	}
}