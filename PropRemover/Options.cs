using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
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

                }
                foreach (var option in from option in Util.GetValues<ModOption>() 
                                       where option != ModOption.None
                                       let field = typeof(Options).GetField(option.GetEnumDescription())
                                       where (bool)field.GetValue(options) select option)
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
            object options = new Options(); //boxing first
            foreach (var option in Util.GetValues<ModOption>().
                Where(option => (OptionsHolder.Options & option) != 0))
            {
                typeof(Options).GetField(option.GetEnumDescription()).SetValue(options, true);
            }
            SaveOptions((Options)options);
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