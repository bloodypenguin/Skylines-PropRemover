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
            group.AddCheckbox("Steam", "removeSteam");
            group.AddCheckbox("Smoke", "removeSmoke");
            group.AddCheckbox("Clown Heads", "removeClownHeads");
            group.AddCheckbox("Ice Cream Cones", "removeIceCones");
            group.AddCheckbox("Doughnut Squirrels", "removeDoughnutSquirrels");
            group.AddCheckbox("Random 3D Billboards", "removeRandom3dBillboards");
            group.AddCheckbox("Octopodes", "removeOctopodes");
            group.AddCheckbox("Flat Billboards", "removeFlatBillboards");
            group.AddCheckbox("Neon Chirpy", "removeNeonChirpy");
        }
    }
}