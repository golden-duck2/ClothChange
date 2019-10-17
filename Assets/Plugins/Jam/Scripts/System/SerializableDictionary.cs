using UnityEngine;
using System;
using System.Collections.Generic;

namespace Jam
{
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		TKey[] keyArray = null;
		[SerializeField]
		TValue[] valArray = null;

		// シリアル化(保存)前に呼ばれる.
		//  DictionaryをSerializeFieldに設定し、この関数終了後、SerializeFieldが保存される.
		public void OnBeforeSerialize()
		{
			// キーを配列にコピーする.
			keyArray = new TKey[Keys.Count];
			Keys.CopyTo(keyArray, 0);
			// 値を配列にコピーする.
			valArray = new TValue[Values.Count];
			Values.CopyTo(valArray, 0);
			//		if (this.Count > 0)
			//		{
			//			itemList = new SerializableList<KeyValuePair<TKey, TValue>>();
			//			foreach (KeyValuePair<TKey, TValue> kv in this)
			//			{
			//				itemList.Add(kv);
			//			}
			//		}
		}

		// デシリアル化(読み込み)後に呼ばれる.
		//  SerializeFieldに値が読み込まれているので、Dictionaryへ値を反映する.
		public void OnAfterDeserialize()
		{
			Clear();
			if (keyArray != null)
			{
				for (var idx = 0; idx < keyArray.Length; ++idx)
				{
					this[keyArray[idx]] = valArray[idx];
				}
			}
			keyArray = null;
			valArray = null;
		}
	}
}