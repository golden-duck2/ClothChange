using System;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Jam
{
	[CustomEditor(typeof(AtlasImage), true)]
	[CanEditMultipleObjects]
	public class AtlasImageEditor : ImageEditor
	{
		SerializedProperty _spriteAtlas;
		SerializedProperty _spriteName;

		string[] atlasSpriteNames = ConstParams.STRING_NONE_ARRAY;
		int spriteNameIndex = 0;

		protected override void OnEnable()
		{
			_spriteAtlas = serializedObject.FindProperty("_spriteAtlas");
			UpdateAtlasSpriteNames();
			_spriteName = serializedObject.FindProperty("_spriteName");
			spriteNameIndex = Array.IndexOf(atlasSpriteNames, _spriteName.stringValue);
			if (spriteNameIndex < 0)
			{	// スプライト名が見つからない.
				spriteNameIndex = 0;
				_spriteName.stringValue = "";
			}
			// アトラスに変更があった時は、Inspector表示したタイミングで更新されるようにする.
			UpdateSourceImage();
			base.OnEnable();
		}

		public override void OnInspectorGUI()
		{
			var bUpdateSourceImage = false;
			serializedObject.Update();
			// Sprite Atlas.
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(_spriteAtlas, new GUIContent(_spriteAtlas.displayName));
			if (EditorGUI.EndChangeCheck())
			{
				UpdateAtlasSpriteNames();
				spriteNameIndex = 0;
				_spriteName.stringValue = atlasSpriteNames[spriteNameIndex];
				bUpdateSourceImage = true;
			}
			// Sprite Name.
			EditorGUI.BeginChangeCheck();
			spriteNameIndex = EditorGUILayout.Popup("Sprite Name", spriteNameIndex, atlasSpriteNames);
			if (EditorGUI.EndChangeCheck())
			{
				_spriteName.stringValue = atlasSpriteNames[spriteNameIndex];
				bUpdateSourceImage = true;
			}
			if(bUpdateSourceImage)
			{
				UpdateSourceImage();
			}
			serializedObject.ApplyModifiedProperties();
			base.OnInspectorGUI();
		}

		protected void UpdateAtlasSpriteNames()
		{
				var spriteAtlas = _spriteAtlas.objectReferenceValue as SpriteAtlas;
				if (spriteAtlas)
				{
					atlasSpriteNames = spriteAtlas.GetSpriteNames();
					if (atlasSpriteNames == null || atlasSpriteNames.Length == 0)
					{
						atlasSpriteNames = ConstParams.STRING_NONE_ARRAY;
					}
				}
				else
				{
					atlasSpriteNames = ConstParams.STRING_NONE_ARRAY;
				}
		}

		protected virtual void UpdateSourceImage()
		{
			var m_Type = serializedObject.FindProperty("m_Type");
			var m_Sprite = serializedObject.FindProperty("m_Sprite");
			var spriteAtlas = _spriteAtlas.objectReferenceValue as SpriteAtlas;
			var newSprite = spriteAtlas?.GetSprite(_spriteName.stringValue);
			m_Sprite.objectReferenceValue = newSprite;
			// ※https://github.com/tenpn/unity3d-ui/blob/master/UnityEditor.UI/UI/ImageEditor.cs SpriteGUI()をコピー.
			if (newSprite)
			{
				Image.Type oldType = (Image.Type) m_Type.enumValueIndex;
				if (newSprite.border.SqrMagnitude() > 0)
				{
					m_Type.enumValueIndex = (int) Image.Type.Sliced;
				}
				else if (oldType == Image.Type.Sliced)
				{
					m_Type.enumValueIndex = (int) Image.Type.Simple;
				}
			}
		}
	}
}