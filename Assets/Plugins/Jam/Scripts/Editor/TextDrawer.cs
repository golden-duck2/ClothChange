using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Jam
{
	[CustomPropertyDrawer(typeof(TextAttribute))]
	internal sealed class TextDrawer : PropertyDrawer
	{
		const int kLineHeight = 13;
		float height = 0;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				label = EditorGUI.BeginProperty(position, label, property);
				position = EditorGUIMultiFieldPrefixLabel(position, 0, label, 1);
				EditorGUI.BeginChangeCheck();
				int indentLevel = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 0;
				string stringValue = EditorGUI.TextArea(position, property.stringValue);
				EditorGUI.indentLevel = indentLevel;
				if (EditorGUI.EndChangeCheck())
				{
					property.stringValue = stringValue;
					CalcHeight(stringValue);
				}
				EditorGUI.EndProperty();
			}
			else
			{
				EditorGUI.LabelField(position, label.text, "Use Text with string.");
			}
		}

		void CalcHeight(string aStr)
		{
			var linesArray = aStr.SplitLines();
			height = (float)(16 + ((linesArray.Length - 1) * kLineHeight));
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (height <= 0)
			{
				CalcHeight(property.stringValue);
			}
			return ((!EditorGUIUtility.wideMode) ? 16f : 0f) + height;
		}

		internal static Rect EditorGUIMultiFieldPrefixLabel(Rect totalPosition, int id, GUIContent label, int columns)
		{
			Rect result;
			if (!LabelHasContent(label))
			{
				result = EditorGUI.IndentedRect(totalPosition);
			}
			else if (EditorGUIUtility.wideMode)
			{
				Rect labelPosition = new Rect(totalPosition.x + EditorGUIIndent, totalPosition.y, EditorGUIUtility.labelWidth - EditorGUIIndent, 16f);
				Rect rect = totalPosition;
				rect.xMin += EditorGUIUtility.labelWidth;
				if (columns > 1)
				{
					labelPosition.width -= 1f;
					rect.xMin -= 1f;
				}
				if (columns == 2)
				{
					float num = (rect.width - 4f) / 3f;
					rect.xMax -= num + 2f;
				}
				EditorGUI.HandlePrefixLabel(totalPosition, labelPosition, label, id);
				result = rect;
			}
			else
			{
				Rect labelPosition2 = new Rect(totalPosition.x + EditorGUIIndent, totalPosition.y, totalPosition.width - EditorGUIIndent, 16f);
				Rect rect2 = totalPosition;
				rect2.xMin += EditorGUIIndent + 15f;
				rect2.yMin += 16f;
				EditorGUI.HandlePrefixLabel(totalPosition, labelPosition2, label, id);
				result = rect2;
			}
			return result;
		}

		internal static bool LabelHasContent(GUIContent label)
		{
			return label == null || label.text != string.Empty || label.image != null;
		}

		internal static float EditorGUIIndent
		{
			get
			{
				return (float)EditorGUI.indentLevel * 15f;
			}
		}
	}
}
