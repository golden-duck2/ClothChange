using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Jam
{
	/// <summary>
	/// HideInInspectorInPlayMode attribute. to make hide.
	/// </summary>
	public class HideInInspectorInPlayModeAttribute : PropertyAttribute
	{
	}

#if UNITY_EDITOR
	/// <summary>
	/// HideInInspectorInPlayMode property drawer. to make hide.
	/// </summary>
	[CustomPropertyDrawer(typeof(HideInInspectorInPlayModeAttribute))]
	public class HideInInspectorInPlayModeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var isPlaying = UnityUtil.isPlaying;
			if (!isPlaying)
			{
				EditorGUI.PropertyField(position, property, label);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var isPlaying = UnityUtil.isPlaying;
			return (!isPlaying) ? EditorGUI.GetPropertyHeight(property, label, true) : 0.0f;
		}
	}
#endif // UNITY_EDITOR
}