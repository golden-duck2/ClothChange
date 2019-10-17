using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Jam
{
	/// <summary>
	/// ReadOnlyInPlayMode attribute. to make read only in play mode.
	/// </summary>
	public class ReadOnlyInPlayModeAttribute : PropertyAttribute
	{
	}

#if UNITY_EDITOR
	/// <summary>
	/// ReadOnlyInPlayMode property drawer. to make read only in play mode.
	/// </summary>
	[CustomPropertyDrawer(typeof(ReadOnlyInPlayModeAttribute))]
	public class ReadOnlyInPlayModeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var isPlaying = UnityUtil.isPlaying;
			EditorGUI.BeginDisabledGroup(isPlaying);
			EditorGUI.PropertyField(position, property, label);
			EditorGUI.EndDisabledGroup();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}
	}
#endif // UNITY_EDITOR
}
