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
            group.AddCheckbox("Steam", ModOption.Steam);
            group.AddCheckbox("Smoke", ModOption.Smoke);
            group.AddCheckbox("Clown Heads", ModOption.ClownHeads);
            group.AddCheckbox("Ice Cream Cones", ModOption.IceCreamCones);
            group.AddCheckbox("Doughnut Squirrels", ModOption.DoughnutSquirrels);
            group.AddCheckbox("Random 3D Billboards", ModOption.Random3DBillboards);
            group.AddCheckbox("Octopodes", ModOption.Octopodes);
            group.AddCheckbox("Flat Billboards", ModOption.FlatBillboards);
            group.AddCheckbox("Neon Chirpy", ModOption.NeonChirpy);
        }
    }
}