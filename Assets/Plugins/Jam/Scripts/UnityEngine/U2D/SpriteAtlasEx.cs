using UnityEngine;
using UnityEngine.U2D;

namespace Jam
{
    /// <summary>
    /// SpriteAtlas extension.
    /// </summary>
    public static class SpriteAtlasEx
    {
        /// <summary>
        /// Clone all the Sprite in this atlas and return.(Jpn.アトラス内のSpriteをクローンして配列として返す.)
        /// </summary>

        public static Sprite[] GetSprites(this SpriteAtlas aSelf)
        {
            Sprite[] spriteArray = new Sprite[aSelf.spriteCount];
            aSelf.GetSprites(spriteArray);
            return spriteArray;
        }

        /// <summary>
        /// Return the Sprite name array in this atlas.(Jpn.アトラス内のSprite名の配列を返す.)
        /// </summary>

        public static string[] GetSpriteNames(this SpriteAtlas aSelf)
        {
            Sprite[] spriteArray = new Sprite[aSelf.spriteCount];
            aSelf.GetSprites(spriteArray);
            string[] nameArray = new string[aSelf.spriteCount];
            for (var idx = 0; idx < aSelf.spriteCount; ++idx)
            {
                nameArray[idx] = spriteArray[idx].name.Replace("(Clone)", "");
            }
            return nameArray;
        }
    }
}