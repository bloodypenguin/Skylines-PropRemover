using System;
using System.Reflection;
using ICities;
using UnityEngine;

namespace PropRemover
{

    public class PropRemover : MonoBehaviour
    {
        private static GameObject removerGo;

        public static void Initialize()
        {
            Dispose();
            removerGo = new GameObject("PropRemover");
            removerGo.AddComponent<PropRemover>();
        }

        public static void Dispose()
        {
            if (removerGo != null)
            {
                Destroy(removerGo);
                removerGo = null;
            }
        }


        public void Update()
        {
            PropRemover.RemoveProps();
            Dispose();
        }

        public static void RemoveProps()
        {
            BuildingInfo[] prefabs = Resources.FindObjectsOfTypeAll<BuildingInfo>();
            for (int j = 0; j < prefabs.Length; j++)
            {
                BuildingInfo buildingInfo = prefabs[j];
                FastList<BuildingInfo.Prop> fastList = new FastList<BuildingInfo.Prop>();
                if (buildingInfo != null)
                {
                    if (buildingInfo.m_props != null)
                    {
                        BuildingInfo.Prop[] props = buildingInfo.m_props;
                        for (int k = 0; k < props.Length; k++)
                        {
                            BuildingInfo.Prop prop = props[k];
                            if (prop != null)
                            {
                                if (prop.m_finalProp != null)
                                {
                                    if (
                                        ((Mod.Options & ModOptions.Smoke) == ModOptions.None || !prop.m_finalProp.name.Contains("Smoke") && !prop.m_finalProp.name.Contains("smoke")) &&
                                        ((Mod.Options & ModOptions.Steam) == ModOptions.None || !prop.m_finalProp.name.Contains("Steam") && !prop.m_finalProp.name.Contains("steam")) &&
                                        ((Mod.Options & ModOptions.ClownHeads) == ModOptions.None || !prop.m_finalProp.name.Contains("Clown") && !prop.m_finalProp.name.Contains("clown")) &&
                                        ((Mod.Options & ModOptions.IceCreamCones) == ModOptions.None || !prop.m_finalProp.name.Contains("Cream") && !prop.m_finalProp.name.Contains("cream")) &&
                                        ((Mod.Options & ModOptions.DoughnutSquirrels) == ModOptions.None || !prop.m_finalProp.name.Contains("Squirrel") && !prop.m_finalProp.name.Contains("squirrel")) &&
                                        ((Mod.Options & ModOptions.Random3DBillboards) == ModOptions.None || prop.m_finalProp.name != "Billboard_3D_variation")
                                        )
                                    {
                                        fastList.Add(prop);
                                    }
                                }
                                else
                                {
                                    fastList.Add(prop);
                                }
                            }
                        }
                    }
                    buildingInfo.m_props = fastList.ToArray();
                }
            }
        }
    }
}
