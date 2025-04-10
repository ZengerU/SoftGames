using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace _02___Magic_Words
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] string dataUrl;
        [SerializeField] DialogView dialogView;

        ImageCollection<Emoji> _emojis;
        ImageCollection<Avatar> _avatars;
        DialogData _dialog;

        int _currentDialogIndex = 0;

        async void Awake()
        {
            _emojis = new ImageCollection<Emoji>();
            _avatars = new ImageCollection<Avatar>();

            _dialog = await DialogUtil.GetDialog(dataUrl);

            _emojis.AddRange(_dialog.Emojis);
            _avatars.AddRange(_dialog.Avatars);

            dialogView.Setup(_avatars, _emojis);
            DisplayDialog();
        }

        internal void DisplayDialog()
        {
            dialogView.DisplayDialog(_dialog.Dialogue[_currentDialogIndex]);
            _currentDialogIndex++;
            _currentDialogIndex %= _dialog.Dialogue.Count;
        }
    }

    internal class ImageCollection<T> : KeyedCollection<string, T> where T : Image
    {
        protected override string GetKeyForItem(T img) => img.Name;

        internal void AddRange(List<T> images)
        {
            foreach (var img in images)
            {
                Add(img);
            }
        }
    }
}