using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace Jam
{
	public static class PlayerPrefsEx
	{
		/// <summary>
		/// Returns the value corresponding to key in the preference file if it exists.
		/// </summary>
		public static uint GetUInt(string aKey, uint aDefaultValue=0)
		{
			return (uint)PlayerPrefs.GetInt(aKey, (int)aDefaultValue);
		}

		/// <summary>
		/// Sets the value of the preference identified by key.
		/// </summary>
		static public void SetUInt(string aKey, uint aValue)
		{
			PlayerPrefs.SetInt(aKey, (int)aValue);
		}
	}
}
