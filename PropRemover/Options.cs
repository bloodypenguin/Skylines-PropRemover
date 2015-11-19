using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ColossalFramework;
using Debug = UnityEngine.Debug;

namespace PropRemover
{

    [Flags]
    public enum ModOption : long
    {
        None = 0,
        [Description("removeSteam")]
        Steam = 1,
        [Description("removeSmoke")]
        Smoke = 2,
        [Description("removeClownHeads")]
        ClownHeads = 4,
        [Description("removeIceCones")]
        IceCreamCones = 8,
        [Description("removeDoughnutSquirrels")]
        DoughnutSquirrels = 16,
        [Description("removeRandom3dBillboards")]
        Random3DBillboards = 32,
        [Description("removeOctopodes")]
        Octopodes = 64,
        [Description("removeFlatBillboards")]
        FlatBillboards = 128,
        [Description("removeNeonChirpy")]
        NeonChirpy = 256
    }

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
        }

        public bool removeSmoke { set; get; }
        public bool removeSteam { set; get; }
        public bool removeClownHeads { set; get; }
        public bool removeIceCones { set; get; }
        public bool removeDoughnutSquirrels { set; get; }
        public bool removeRandom3dBillboards { set; get; }
        public bool removeOctopodes { set; get; }
        public bool removeFlatBillboards { set; get; }
        public bool removeNeonChirpy { set; get; }
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
            try
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
                catch (FileNotFoundException) // No options file yet
                {
                    options = new Options();
                    SaveOptions(options);
                }
                foreach (var option in from option in Util.GetValues<ModOption>()
                                       where option != ModOption.None
                                       let isEnabled = (bool)typeof(Options).GetProperty(option.GetEnumDescription()).GetValue(options, null)
                                       where isEnabled
                                       select option)
                {
                    OptionsHolder.Options |= option;
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Unexpected {0} while loading options: {1}\n{2}",
                    e.GetType().Name, e.Message, e.StackTrace);
            }
        }

        public static void SaveOptions()
        {
            var options = new Options();
            foreach (var option in Util.GetValues<ModOption>().Where(option => option != ModOption.None))
            {
                typeof(Options).GetProperty(option.GetEnumDescription()).SetValue(options, OptionsHolder.Options.IsFlagSet(option), null);
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
                Debug.LogErrorFormat("Unexpected {0} while saving options: {1}\n{2}",
                    e.GetType().Name, e.Message, e.StackTrace);
            }
        }
    }
}