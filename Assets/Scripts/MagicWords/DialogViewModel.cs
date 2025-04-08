using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using VContainer;
using UniRx;
using System;
namespace Assets.Scripts.MagicWords
{
    public class DialogViewModel : IDialogViewModel
    {
        private DialogueModel _dialogueModel;

        private Dictionary<string, Emoji> emojiDictionary = new Dictionary<string, Emoji>();
        private Dictionary<string, Avatar> avatarDictionary = new Dictionary<string, Avatar>();

        public ReactiveCollection<Dialogue> DialogueList { get; private set; } = 
            new ReactiveCollection<Dialogue>();

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
            string jsonResponse = await WebRequestHelper.GetJsonData(url);
            _dialogueModel = JsonUtility.FromJson<DialogueModel>(jsonResponse);

           
            // Populate emoji dictionary
            foreach (var emoji in _dialogueModel.emojies)
            {
                emojiDictionary.Add(emoji.name, emoji);
            }

            // Populate avatar dictionary
            foreach (var avatar in _dialogueModel.avatars)
            {
                avatarDictionary.Add(avatar.name, avatar);
            }

            foreach (var dialog in _dialogueModel.dialogue)
            {
                DialogueList.Add(dialog);
                await Task.Delay(100);
            }
        }

        public Avatar GetAvatar(string name)
        {
            if (avatarDictionary.TryGetValue(name, out Avatar avatar))
            {
                return avatar;
            }
            return null;
        }

        public bool TryGetEmojiUrl(string emojiKey, out string emojiUrl)
        {
            emojiUrl = null;
            if (emojiDictionary.TryGetValue(emojiKey, out var emoji))
            {
                emojiUrl = emoji.url;
                return true;
            }
            return false;
        }
    }
}
