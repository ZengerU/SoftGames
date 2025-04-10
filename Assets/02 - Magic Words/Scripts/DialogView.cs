using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagicWords
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
            _emojiUnicodeMap = SpriteAssetUtil.PopulateSpriteAsset(text.spriteAsset, emojiSize, _emojis);
        }

        internal void DisplayDialog(DialogueEntry entry)
        {
            _avatars.TryGetValue(entry.Name, out var avatarEntry);
            DisplayAvatar(avatarEntry);
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
            if (avatar == null)
            {
                avatarImage.texture = null;
                return;
            }
            
            var rect = avatarImage.rectTransform;
            var value = avatar.AvatarPosition == AvatarPosition.Left ? 0 : 1;

            rect.anchorMin = rect.anchorMin.WithValue(x: value);
            rect.anchorMax = rect.anchorMax.WithValue(x: value);
            rect.pivot = rect.pivot.WithValue(x: value);

            avatarImage.texture = avatar.Texture;
        }
    }
}