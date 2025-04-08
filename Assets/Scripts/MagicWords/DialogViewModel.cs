using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using VContainer;

namespace Assets.Scripts.MagicWords
{
    public class DialogViewModel : IDialogViewModel
    {
        private DialogueModel _dialogueModel;

        private Dictionary<string, string> emojiDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> avatarDictionary = new Dictionary<string, string>();

        [Inject]
        private void Constructor(DialogueModel dialogueModel)
        {
            _dialogueModel = dialogueModel;
        }
        public async Task InitializeAsync(string url)
        {
            await LoadDialogueDataAsync(url);
        }
        private async Task LoadDialogueDataAsync(string url)
        {
            // Simulate loading JSON data from a URL asynchronously (use UnityWebRequest in actual Unity code)
            string jsonResponse = await WebRequestHelper.GetJsonData(url);
            _dialogueModel = JsonUtility.FromJson<DialogueModel>(jsonResponse);

            // Populate emoji dictionary
            foreach (var emoji in _dialogueModel.emojies)
            {
                emojiDictionary.Add(emoji.name, emoji.url);
            }

            // Populate avatar dictionary
            foreach (var avatar in _dialogueModel.avatars)
            {
                avatarDictionary.Add(avatar.name, avatar.url);
            }
        }
    }
}
