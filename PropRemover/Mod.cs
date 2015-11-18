using System;
using ICities;

namespace PropRemover
{
    [Flags]
    public enum ModOptions : long
    {
        None = 0,
        Steam = 1,
        Smoke = 2,
        ClownHeads = 4,
        IceCreamCones = 8,
        DoughnutSquirrels = 16,
        Random3DBillboards = 32
    }

    public class Mod : IUserMod
    {
        public static ModOptions Options = ModOptions.None;

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
            UIHelperBase group = helper.AddGroup("Prop Remover Options");
            group.AddCheckbox("Steam", (Options & ModOptions.Steam) != 0,
                (b) =>
                {
                    if (b)
                    {
                        Options |= ModOptions.Steam;
                    }
                    else
                    {
                        Options &= ~ModOptions.Steam;
                    }
                    OptionsLoader.SaveOptions();
                });
            group.AddCheckbox("Smoke", (Options & ModOptions.Smoke) != 0,
                (b) =>
                {
                    if (b)
                    {
                        Options |= ModOptions.Smoke;
                    }
                    else
                    {
                        Options &= ~ModOptions.Smoke;
                    }
                    OptionsLoader.SaveOptions();
                });
            group.AddCheckbox("Clown Heads", (Options & ModOptions.ClownHeads) != 0,
                (b) =>
                {
                    if (b)
                    {
                        Options |= ModOptions.ClownHeads;
                    }
                    else
                    {
                        Options &= ~ModOptions.ClownHeads;
                    }
                    OptionsLoader.SaveOptions();
                });
            group.AddCheckbox("Ice Cream Cones", (Options & ModOptions.IceCreamCones) != 0,
                (b) =>
                {
                    if (b)
                    {
                        Options |= ModOptions.IceCreamCones;
                    }
                    else
                    {
                        Options &= ~ModOptions.IceCreamCones;
                    }
                    OptionsLoader.SaveOptions();
                });
            group.AddCheckbox("Doughnut Squirrels", (Options & ModOptions.DoughnutSquirrels) != 0,
                (b) =>
                {
                    if (b)
                    {
                        Options |= ModOptions.DoughnutSquirrels;
                    }
                    else
                    {
                        Options &= ~ModOptions.DoughnutSquirrels;
                    }
                    OptionsLoader.SaveOptions();
                });
            group.AddCheckbox("Random 3D Billboards", (Options & ModOptions.Random3DBillboards) != 0,
                    (b) =>
                    {
                        if (b)
                        {
                            Options |= ModOptions.Random3DBillboards;
                        }
                        else
                        {
                            Options &= ~ModOptions.Random3DBillboards;
                        }
                        OptionsLoader.SaveOptions();
                    });

        }
    }

    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
            {
                PropRemover.Initialize();
            }
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            PropRemover.Dispose();
        }
    }
}