using ICities;

namespace PropRemover
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame && mode != LoadMode.NewGameFromScenario)
            {
                return;
            }
            PropRemover.Initialize();
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            PropRemover.Dispose();
        }
    }
}