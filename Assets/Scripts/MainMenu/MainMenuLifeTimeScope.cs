using VContainer;
using VContainer.Unity;
namespace Assets.Scripts.MainMenu
{
    public class MainMenuLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IMainMenuViewModel, MainMenuViewModel>(Lifetime.Scoped);
        }
    }
}
