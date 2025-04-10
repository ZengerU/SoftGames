using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;

namespace MagicWords
{
    public static class SpriteAssetUtil
    {
        public static Dictionary<string, uint> PopulateSpriteAsset(TMP_SpriteAsset spriteAsset, Vector2Int emojiSize, IEnumerable<Emoji> emojis)
        {
            var result = new Dictionary<string, uint>();

            spriteAsset.spriteInfoList = new List<TMP_Sprite>();
            spriteAsset.spriteCharacterTable.Clear();
            spriteAsset.spriteGlyphTable.Clear();

            var texture = (Texture2D)spriteAsset.spriteSheet;

            var coord = Vector2Int.one * -1;
            uint idx = 0;

            foreach (var emoji in emojis)
            {
                texture = texture.AddTexture(emoji.Texture, coord, emojiSize, out coord);
                emoji.Index = idx;

                var sprite = emoji.Sprite;
                TMP_Sprite spriteData = new()
                {
                    id = (int)idx,
                    sprite = emoji.Sprite,
                    name = emoji.Name,
                    x = sprite.textureRect.x,
                    y = sprite.textureRect.y,
                    width = sprite.textureRect.width,
                    height = sprite.textureRect.height,
                    xOffset = sprite.textureRectOffset.x,
                    yOffset = sprite.textureRectOffset.y,
                    xAdvance = sprite.textureRectOffset.x,
                    pivot = new Vector2(0.5f, 0.5f),
                    hashCode = TMP_TextUtilities.GetSimpleHashCode(sprite.name),
                };

                var glyphRect = new GlyphRect(coord.x * emojiSize.x, coord.y * emojiSize.y, emojiSize.x, emojiSize.y);
                var glyph = new TMP_SpriteGlyph(idx, new GlyphMetrics(128, 128, 0, 128, 128), glyphRect, 1, 0);
                var character = new TMP_SpriteCharacter(idx, glyph);

                spriteAsset.spriteCharacterTable.Add(character);
                spriteAsset.spriteGlyphTable.Add(glyph);
                spriteAsset.spriteInfoList.Add(spriteData);

                result.Add(emoji.Name, idx);

                idx++;
            }

            spriteAsset.UpdateLookupTables();

            spriteAsset.spriteSheet = texture;

            return result;
        }
    }
}