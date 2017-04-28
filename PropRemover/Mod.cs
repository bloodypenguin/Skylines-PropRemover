using ICities;
using PropRemover.OptionsFramework.Extensions;

namespace PropRemover
{
    public class Mod : IUserMod
    {
        public string Name => "Prop Remover";

        public string Description => "Removes most annoying props from all buildings";

        public void OnSettingsUI(UIHelperBase helper)
        {
            helper.AddOptionsGroup<Options>();
        }
    }
}