using System.Threading.Tasks;

namespace Assets.Scripts.MagicWords
{
    public interface IDialogViewModel
    {
        Task InitializeAsync(string url);
    }
}
