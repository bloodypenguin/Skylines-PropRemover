using ICities;

namespace PropRemover
{
    public static class UIHelperBaseExtension
    {
        public static void AddCheckbox(this UIHelperBase group, string text, string propertyName)
        {
            var property = typeof (Options).GetProperty(propertyName);
            group.AddCheckbox(text, (bool)property.GetValue(OptionsHolder.Options, null),
                b =>
                {
                    property.SetValue(OptionsHolder.Options, b, null);
                    OptionsLoader.SaveOptions();
                });
        } 
    }
}