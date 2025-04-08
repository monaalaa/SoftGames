using UnityEngine.SceneManagement;

namespace Assets.Scripts.MainMenu
{
    public class MainMenuViewModel : IMainMenuViewModel
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
