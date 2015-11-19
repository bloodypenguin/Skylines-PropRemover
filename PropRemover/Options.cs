using System;
using System.IO;
using System.Xml.Serialization;
using Debug = UnityEngine.Debug;

namespace PropRemover
{
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
        public static Options Options = new Options();
    }

    public static class OptionsLoader
    {
        private const string FileName = "CSL-PropRemover.xml";

        public static void LoadOptions()
        {
            try
            {
                try
                {
                    var xmlSerializer = new XmlSerializer(typeof(Options));
                    using (var streamReader = new StreamReader(FileName))
                    {
                        OptionsHolder.Options = (Options)xmlSerializer.Deserialize(streamReader);
                    }
                }
                catch (FileNotFoundException) 
                {
                    // No options file yet
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
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(Options));
                using (var streamWriter = new StreamWriter(FileName))
                {
                    xmlSerializer.Serialize(streamWriter, OptionsHolder.Options);
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