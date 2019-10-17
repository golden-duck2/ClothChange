using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Jam
{
	/// <summary>
	/// ReadOnlyInEditMode attribute. to make read only in edit mode.
	/// </summary>
	public class ReadOnlyInEditModeAttribute : PropertyAttribute
	{
	}

#if UNITY_EDITOR
	/// <summary>
	/// ReadOnlyInEditMode property drawer. to make read only in edit mode.
	/// </summary>
	[CustomPropertyDrawer(typeof(ReadOnlyInEditModeAttribute))]
	public class ReadOnlyInEditModeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var isPlaying = UnityUtil.isPlaying;
			EditorGUI.BeginDisabledGroup(!isPlaying);
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
