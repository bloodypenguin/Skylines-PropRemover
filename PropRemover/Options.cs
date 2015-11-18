using System;
using System.IO;
using System.Xml.Serialization;
using Debug = UnityEngine.Debug;

namespace PropRemover
{

    public struct Options
    {
        public bool removeSmoke;
        public bool removeSteam;
        public bool removeClownHeads;
        public bool removeIceCones;
        public bool removeDoughnutSquirrels;
        public bool removeRandom3dBillboards;
    }

    public static class OptionsLoader
    {
        public static void LoadOptions()
        {
            Mod.Options = ModOptions.None;
            Options options;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Options));
                using (StreamReader streamReader = new StreamReader("CSL-PropRemover.xml"))
                {
                    options = (Options)xmlSerializer.Deserialize(streamReader);
                }
            }
            catch (FileNotFoundException)
            {
                options = new Options
                {
                    removeSmoke = true,
                    removeSteam = true,
                    removeClownHeads = true,
                    removeIceCones = true,
                    removeDoughnutSquirrels = true,
                    removeRandom3dBillboards = true
                };
                SaveOptions(options);
                // No options file yet
            }
            catch (Exception e)
            {
                Debug.LogError("Unexpected " + e.GetType().Name + " loading options: " + e.Message + "\n" + e.StackTrace);
                return;
            }
            if (options.removeSmoke)
            {
                Mod.Options |= ModOptions.Smoke;
            }
            if (options.removeSteam)
            {
                Mod.Options |= ModOptions.Steam; 
            }
            if (options.removeClownHeads)
            {
                Mod.Options |= ModOptions.ClownHeads;   
            }
            if (options.removeIceCones)
            {
                Mod.Options |= ModOptions.IceCreamCones;       
            }
            if (options.removeDoughnutSquirrels)
            {
                Mod.Options |= ModOptions.DoughnutSquirrels;
            }
            if (options.removeRandom3dBillboards)
            {
                Mod.Options |= ModOptions.Random3DBillboards;        
            }
        }

        public static void SaveOptions()
        {
            Options options = new Options();
            if ((Mod.Options & ModOptions.Steam) != 0)
            {
                options.removeSmoke = true;
            }
            if ((Mod.Options & ModOptions.Smoke) != 0)
            {
                options.removeSteam = true;
            }
            if ((Mod.Options & ModOptions.ClownHeads) != 0)
            {
                options.removeClownHeads = true;
            }
            if ((Mod.Options & ModOptions.IceCreamCones) != 0)
            {
                options.removeIceCones = true;
            }
            if ((Mod.Options & ModOptions.DoughnutSquirrels) != 0)
            {
                options.removeDoughnutSquirrels = true;
            }
            if ((Mod.Options & ModOptions.Random3DBillboards) != 0)
            {
                options.removeRandom3dBillboards = true;
            }
            SaveOptions(options);
        }

        public static void SaveOptions(Options options)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Options));
                using (StreamWriter streamWriter = new StreamWriter("CSL-PropRemover.xml"))
                {
                    xmlSerializer.Serialize(streamWriter, options);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Unexpected " + e.GetType().Name + " saving options: " + e.Message + "\n" + e.StackTrace);
            }
        }
    }
}