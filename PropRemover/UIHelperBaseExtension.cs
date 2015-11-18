using ColossalFramework;
using ICities;

namespace PropRemover
{
    public static class UIHelperBaseExtension
    {
        public static void AddCheckbox(this UIHelperBase group, string text, ModOption flag)
        {
            group.AddCheckbox(text, OptionsHolder.Options.IsFlagSet(flag),
                b =>
                {
                    if (b)
                    {
                        OptionsHolder.Options |= flag;
                    }
                    else
                    {
                        OptionsHolder.Options &= ~flag;
                    }
                    OptionsLoader.SaveOptions();
                });
        } 
    }
}