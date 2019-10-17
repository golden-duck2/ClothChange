using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Jam
{
	public class AtlasImage : Image
	{
		[SerializeField] SpriteAtlas _spriteAtlas;
		public SpriteAtlas atlas
		{
			get { return _spriteAtlas; }
			set { _spriteAtlas = value; }
		}

		[SerializeField] string _spriteName;
		public string spriteName
		{
			get { return _spriteName; }
			set
			{
				_spriteName = value;
				OnEnable();
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.sprite = (atlas != null) ? atlas.GetSprite(spriteName) : null;
		}
	}
}