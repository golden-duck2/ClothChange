using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Jam
{
	public class SaveDataManagerBase<T> : SingletonBaseMB<T> where T : UnityEngine.Component
	{
		protected override void Awake()
		{
			base.Awake();
			//		Debug.Log("@at SaveDataManagerBase<T>.Awake() " + GetType());
			Load();
		}

		public virtual void Save()
		{
		}

		public virtual void Load()
		{
			//		Debug.Log("at SaveDataManagerBase.Load()" + transform.GetPath());
		}

		public void Set(string aKey, bool aValue)
		{
			data[aKey] = aValue.ToString();
		}

		public void Set(string aKey, int aValue)
		{
			data[aKey] = aValue.ToString();
		}

		public void Set(string aKey, float aValue)
		{
			data[aKey] = aValue.ToString();
		}

		public void Set(string aKey, string aValue)
		{
			data[aKey] = aValue;
		}

		public void Set<Ty>(string key, Ty obj) where Ty : class, new()
		{
			string json = JsonUtility.ToJson(obj);
			//a		Debug.Log("json:" + json);
			data[key] = json;
		}

		public bool GetBool(string aKey, bool aDefault = false)
		{
			if (!data.ContainsKey(aKey))
			{
				return aDefault;
			}
			return data[aKey].ParseBool(aDefault);
		}

		public int GetInt(string aKey, int aDefault = int.MaxValue)
		{
			if (!data.ContainsKey(aKey))
			{
				return aDefault;
			}
			return data[aKey].ParseInt(aDefault);
		}

		public float GetFloat(string aKey, float aDefault = float.NaN)
		{
			if (!data.ContainsKey(aKey))
			{
				return aDefault;
			}
			return data[aKey].ParseFloat(aDefault);
		}

		public string GetString(string aKey, string aDefault = "")
		{
			if (!data.ContainsKey(aKey))
			{
				return aDefault;
			}
			return data[aKey];
		}

		public Ty GetClass<Ty>(string aKey, Ty aDefault) where Ty : class, new()
		{
			if (!data.ContainsKey(aKey))
			{
				return aDefault;
			}
			string json = data[aKey];
			return JsonUtility.FromJson<Ty>(json);
		}

		public void Clear()
		{
			data.Clear();
		}

		protected SerializableDictionary<string, string> data = new SerializableDictionary<string, string>();
	}
}
