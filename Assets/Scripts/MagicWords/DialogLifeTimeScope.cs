using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.MagicWords
{
    public class DialogLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<DialogueModel>(Lifetime.Singleton);
            builder.Register<IDialogViewModel, DialogViewModel>(Lifetime.Singleton);
        }
    }
}
