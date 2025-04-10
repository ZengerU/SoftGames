using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.TextCore;
using UnityEngine.UI;

namespace _02___Magic_Words
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] Vector2Int emojiSize = new(128, 128);
        [SerializeField] RawImage avatarImage;
        [SerializeField] TextMeshProUGUI text;
        ImageCollection<Avatar> _avatars;
        ImageCollection<Emoji> _emojis;

        Dictionary<string, uint> _emojiUnicodeMap;


        internal void Setup(ImageCollection<Avatar> avatars, ImageCollection<Emoji> emojis)
        {
            _avatars = avatars;
            _emojis = emojis;
            CreateEmojiSpriteAsset();
        }

        void CreateEmojiSpriteAsset()
        {
            _emojiUnicodeMap = new Dictionary<string, uint>();
            
            var spriteAsset = text.spriteAsset;
            spriteAsset.spriteCharacterTable.Clear();
            spriteAsset.spriteGlyphTable.Clear();

            var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);

            
            var coord = Vector2Int.zero;
            uint idx = 0;

            foreach (var emoji in _emojis)
            {
                if (emoji.Texture == null)
                    continue;

                texture = texture.AddTexture(emoji.Texture, coord, emojiSize, out coord);

                var glyphRect = new GlyphRect(coord.x * emojiSize.x, coord.y * emojiSize.y, emojiSize.x, emojiSize.y);
                var glyph = new TMP_SpriteGlyph(idx, new GlyphMetrics(128, 128, 0, 0, 0), glyphRect, 1, 0);
                var character = new TMP_SpriteCharacter(idx, glyph);

                idx++;

                spriteAsset.spriteCharacterTable.Add(character);
                spriteAsset.spriteGlyphTable.Add(glyph);
                
                _emojiUnicodeMap.Add(emoji.Name, idx);
            }
            spriteAsset.spriteSheet = texture;
        }

        internal void DisplayDialog(DialogueEntry entry)
        {
            DisplayAvatar(_avatars[entry.Name]);
            DisplayText(entry.Text);
        }

        void DisplayText(string entryText)
        {
            text.SetText(ConvertText(entryText));
        }

        string ConvertText(string entryText)
        {
            foreach (var (emojiName, id) in _emojiUnicodeMap)
            {
                entryText = entryText.Replace("{" + emojiName + "}", $"<sprite={id}>");
            }

            return entryText;
        }

        void DisplayAvatar(Avatar avatar)
        {
            var rect = avatarImage.rectTransform;
            var value = avatar.AvatarPosition == AvatarPosition.Left ? 0 : 1;

            rect.anchorMin = rect.anchorMin.WithValue(x: value);
            rect.anchorMax = rect.anchorMax.WithValue(x: value);
            rect.pivot = rect.pivot.WithValue(x: value);

            avatarImage.texture = avatar.Texture;
        }
    }
}