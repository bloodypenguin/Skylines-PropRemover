using System.Linq;
using ICities;

namespace PropRemover
{
    public class Mod : IUserMod
    {
        public string Name
        {
            get
            {
                OptionsLoader.LoadOptions();
                return "Prop Remover";
            }
        }

        public string Description
        {
            get { return "Removes most annoying props from all buildings"; }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            var group = helper.AddGroup("Prop Remover Options");
            var properties = typeof (Options).GetProperties();
            foreach (var name in from property in properties select property.Name)
            {
                var description = OptionsHolder.Options.GetPropertyDescription(name);
                group.AddCheckbox(description, name);

            }
        }
    }
}