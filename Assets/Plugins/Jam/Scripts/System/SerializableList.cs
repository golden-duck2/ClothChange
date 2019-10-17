using UnityEngine;
using System;
using System.Collections.Generic;

namespace Jam
{
	[Serializable]
	public class SerializableList<T> : List<T>, ISerializationCallbackReceiver
	{
		[SerializeField]
		public List<T> items;

		public void OnBeforeSerialize()
		{
			items = this;
		}

		public void OnAfterDeserialize()
		{
			Clear();
			foreach (var item in items)
			{
				Add(item);
			}
		}
	}
}
