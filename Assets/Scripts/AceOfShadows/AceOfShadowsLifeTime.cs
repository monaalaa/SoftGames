using VContainer;
using VContainer.Unity;

public class AceOfShadowsLifeTime : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<CardViewModel>(Lifetime.Singleton);
    }
}
