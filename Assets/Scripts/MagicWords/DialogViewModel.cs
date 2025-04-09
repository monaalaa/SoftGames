using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using VContainer;
using UniRx;
using System;
using System.Text.RegularExpressions;
using TMPro;
using System.Linq;
namespace Assets.Scripts.MagicWords
{
    public class DialogViewModel : IDialogViewModel
    {
        private DialogueModel _dialogueModel;

        private Dictionary<string, string> emojiDictionary = new Dictionary<string, string>();
        private Dictionary<string, Avatar> avatarDictionary = new Dictionary<string, Avatar>();
        private static readonly Regex emojiTagRegex = new(@"\{(.*?)\}");

        private SpriteAssetGenerator spriteAssetGenerator = new SpriteAssetGenerator();
        private TMP_SpriteAsset spriteAsset;
        private Material spriteAssetMaterial;

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

        public void SetSpriteAssetMaterial(Material mat)
        {
            spriteAssetMaterial = mat;
        }

        private async Task LoadDialogueDataAsync(string url)
        {
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
                avatarDictionary.Add(avatar.name, avatar);
            }
            spriteAsset = await spriteAssetGenerator.CreateFromUrlsAsync(emojiDictionary, spriteAssetMaterial);

            foreach (var dialog in _dialogueModel.dialogue)
            {
                dialog.text = ReplaceEmojiTags(dialog.text);
                Debug.Log(dialog.text);
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

        public TMP_SpriteAsset GetSpriteAsset() { return spriteAsset; }

        public string ReplaceEmojiTags(string input)
        {
            return emojiTagRegex.Replace(input, match =>
            {
                string tag = match.Groups[1].Value;
                int index = emojiDictionary.Keys.ToList().IndexOf(tag);
                if(index < 0 )
                    index = 0;
                return $"<sprite={index}>";
            });
        }

    }
}
