using System;
using System.IO;
using System.Xml.Serialization;
using Debug = UnityEngine.Debug;

namespace PropRemover
{

    [Flags]
    public enum ModOption : long
    {
        None = 0,
        Steam = 1,
        Smoke = 2,
        ClownHeads = 4,
        IceCreamCones = 8,
        DoughnutSquirrels = 16,
        Random3DBillboards = 32
    }

    public struct Options
    {
        public bool removeSmoke;
        public bool removeSteam;
        public bool removeClownHeads;
        public bool removeIceCones;
        public bool removeDoughnutSquirrels;
        public bool removeRandom3dBillboards;
    }

    public static class OptionsHolder
    {
        public static ModOption Options = ModOption.None;
    }

    public static class OptionsLoader
    {
        private const string FileName = "CSL-PropRemover.xml";

        public static void LoadOptions()
        {
            OptionsHolder.Options = ModOption.None;
            Options options;
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(Options));
                using (var streamReader = new StreamReader(FileName))
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
                Debug.LogErrorFormat("Unexpected {0} loading options: {1}\n{2}",
                    e.GetType().Name, e.Message, e.StackTrace);
                return;
            }
            if (options.removeSmoke)
            {
                OptionsHolder.Options |= ModOption.Smoke;
            }
            if (options.removeSteam)
            {
                OptionsHolder.Options |= ModOption.Steam;
            }
            if (options.removeClownHeads)
            {
                OptionsHolder.Options |= ModOption.ClownHeads;
            }
            if (options.removeIceCones)
            {
                OptionsHolder.Options |= ModOption.IceCreamCones;
            }
            if (options.removeDoughnutSquirrels)
            {
                OptionsHolder.Options |= ModOption.DoughnutSquirrels;
            }
            if (options.removeRandom3dBillboards)
            {
                OptionsHolder.Options |= ModOption.Random3DBillboards;
            }
        }

        public static void SaveOptions()
        {
            var options = new Options();
            if ((OptionsHolder.Options & ModOption.Steam) != 0)
            {
                options.removeSmoke = true;
            }
            if ((OptionsHolder.Options & ModOption.Smoke) != 0)
            {
                options.removeSteam = true;
            }
            if ((OptionsHolder.Options & ModOption.ClownHeads) != 0)
            {
                options.removeClownHeads = true;
            }
            if ((OptionsHolder.Options & ModOption.IceCreamCones) != 0)
            {
                options.removeIceCones = true;
            }
            if ((OptionsHolder.Options & ModOption.DoughnutSquirrels) != 0)
            {
                options.removeDoughnutSquirrels = true;
            }
            if ((OptionsHolder.Options & ModOption.Random3DBillboards) != 0)
            {
                options.removeRandom3dBillboards = true;
            }
            SaveOptions(options);
        }

        public static void SaveOptions(Options options)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(Options));
                using (var streamWriter = new StreamWriter(FileName))
                {
                    xmlSerializer.Serialize(streamWriter, options);
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Unexpected {0} saving options: {1}\n{2}",
                    e.GetType().Name, e.Message, e.StackTrace);
            }
        }
    }
}