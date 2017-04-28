using System.ComponentModel;
using PropRemover.OptionsFramework.Attibutes;

namespace PropRemover
{
    [Options("PropRemover", "CSL-PropRemover")]
    public class Options
    {
        public Options()
        {
            removeSmoke = true;
            removeSteam = true;
            removeClownHeads = true;
            removeIceCones = true;
            removeDoughnutSquirrels = true;
            removeRandom3dBillboards = true;
            removeOctopodes = true;
            removeFlatBillboards = true;
            removeNeonChirpy = true;
            removeWallFlags = true;
            removeSolarPanels = true;
            removeAnimatedBillboards = true;
            removeLogoBillboards = false;
        }

        [Checkbox("Steam")]
        public bool removeSmoke { set; get; }
        [Checkbox("Smoke")]
        public bool removeSteam { set; get; }
        [Checkbox("Clown Heads")]
        public bool removeClownHeads { set; get; }
        [Checkbox("Ice Cream Cones")]
        public bool removeIceCones { set; get; }
        [Checkbox("Doughnut Squirrels")]
        public bool removeDoughnutSquirrels { set; get; }
        [Checkbox("Random 3D Billboards")]
        public bool removeRandom3dBillboards { set; get; }
        [Checkbox("Octopodes")]
        public bool removeOctopodes { set; get; }
        [Checkbox("Flat Billboards")]
        public bool removeFlatBillboards { set; get; }
        [Checkbox("Animated Billboards")]
        public bool removeAnimatedBillboards { set; get; }
        [Checkbox("Logo Billboards")]
        public bool removeLogoBillboards { set; get; }
        [Checkbox("Chirpy Holograms")]
        public bool removeNeonChirpy { set; get; }
        [Checkbox("Wall Flags")]
        public bool removeWallFlags { set; get; }
        [Checkbox("Rooftop Solar Panels")]
        public bool removeSolarPanels { set; get; }
    }
}