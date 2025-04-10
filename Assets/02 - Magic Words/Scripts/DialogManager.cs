using System.Threading.Tasks;
using UnityEngine;

namespace MagicWords
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] string dataUrl;
        [SerializeField] DialogView dialogView;

        ImageCollection<Emoji> _emojis;
        ImageCollection<Avatar> _avatars;
        DialogData _dialog;

        int _currentDialogIndex = 0;

        void Awake()
        {
            _emojis = new ImageCollection<Emoji>();
            _avatars = new ImageCollection<Avatar>();
            _ = SetupDialog();
        }

        async Task SetupDialog()
        {
            _dialog = await DialogUtil.GetDialog(dataUrl);

            _emojis.AddRange(_dialog.Emojis);
            _avatars.AddRange(_dialog.Avatars);

            dialogView.Setup(_avatars, _emojis);
            DisplayDialog();
        }

        public void DisplayDialog()
        {
            dialogView.DisplayDialog(_dialog.Dialogue[_currentDialogIndex]);
            _currentDialogIndex++;
            _currentDialogIndex %= _dialog.Dialogue.Count;
        }
    }
}