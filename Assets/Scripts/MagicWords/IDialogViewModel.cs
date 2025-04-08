using System.Threading.Tasks;
using UniRx;

namespace Assets.Scripts.MagicWords
{
    public interface IDialogViewModel
    {
        public ReactiveCollection<Dialogue> DialogueList { get; }
        Task InitializeAsync(string url);
        Avatar GetAvatar(string name);
        bool TryGetEmojiUrl(string emojiKey, out string emojiUrl);
    }
}
