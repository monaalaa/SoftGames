using UnityEngine;
using VContainer;
namespace Assets.Scripts.MagicWords
{
    public class DialogView : MonoBehaviour
    {
        private const string jsonURL = "https://private-624120-softgamesassignment.apiary-mock.com/v2/magicwords";

        private IDialogViewModel _dialogViewModel;

        [Inject]
        private void Constructor(IDialogViewModel dialogViewModel)
        {
            _dialogViewModel = dialogViewModel;
            _dialogViewModel.InitializeAsync(jsonURL);
        }
    
    }
}
