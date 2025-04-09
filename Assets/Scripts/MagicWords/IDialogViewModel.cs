using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    public interface IDialogViewModel
    {
        public ReactiveCollection<Dialogue> DialogueList { get; }
        Task InitializeAsync(string url);
        Avatar GetAvatar(string name);
        TMP_SpriteAsset GetSpriteAsset();
        void SetSpriteAssetMaterial(Material mat);
        // bool TryGetEmojiUrl(string emojiKey, out string emojiUrl);
    }
}
