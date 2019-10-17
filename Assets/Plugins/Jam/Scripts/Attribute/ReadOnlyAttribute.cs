using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Jam
{
	/// <summary>
	/// ReadOnly attribute. to make read only.
	/// </summary>
	public class ReadOnlyAttribute : PropertyAttribute
	{
	}

#if UNITY_EDITOR
	/// <summary>
	/// ReadOnly property drawer. to make read only.
	/// </summary>
	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public class ReadOnlyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginDisabledGroup(true);
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
