using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets.Scripts.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _aceofShadows;
        [SerializeField]
        private Button _magicWords;
        [SerializeField]
        private Button _phoenixFlame;

        private IMainMenuViewModel _mainMenuViewModel;

        [Inject]
        private void Constructor(IMainMenuViewModel mainMenuViewModel)
        {
            _mainMenuViewModel = mainMenuViewModel;
        }

        private void Start()
        {
            _aceofShadows.onClick.AddListener(() => OnClickButton("AceofShadows"));
            _magicWords.onClick.AddListener(() => OnClickButton("MagicWords"));
            _phoenixFlame.onClick.AddListener(() => OnClickButton("PhoenixFlame"));
        }

        private void OnClickButton(string sceneName)
        {
            _mainMenuViewModel.LoadScene(sceneName);
        } 
    }
}
