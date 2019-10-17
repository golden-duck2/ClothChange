using UnityEngine;
using UnityEditor;

// http://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html

namespace Jam
{
	[CustomPropertyDrawer(typeof(ColorHtmlPropertyAttribute))]
	public class ColorHtmlPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Rect htmlField = new Rect(position.x, position.y, position.width - 100, position.height);
			Rect colorField = new Rect(position.x + htmlField.width, position.y, position.width - htmlField.width, position.height);

			string htmlValue = EditorGUI.TextField(htmlField, label, "#" + ColorUtility.ToHtmlStringRGBA(property.colorValue));

			Color newCol;
			if (ColorUtility.TryParseHtmlString(htmlValue, out newCol))
			{
				property.colorValue = newCol;
			}
			else
			{
				Debug.LogWarning("ColorUtility.TryParseHtmlString(htmlValue:" + htmlValue + ", out newCol) returns false.");
			}

			property.colorValue = EditorGUI.ColorField(colorField, property.colorValue);
		}
	}
}
