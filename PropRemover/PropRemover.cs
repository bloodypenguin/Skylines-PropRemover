using System.Linq;
using PropRemover.OptionsFramework;
using UnityEngine;

namespace PropRemover
{

    public class PropRemover : MonoBehaviour
    {

        private static readonly string gameObjectName = "PropRemover";
        private static readonly string[] BillboardCategories = {
            "PropsBillboardsSmallBillboard",
            "PropsBillboardsMediumBillboard",
            "PropsBillboardsLargeBillboard",
            "PropsSpecialBillboardsRandomSmallBillboard",
            "PropsSpecialBillboardsRandomMediumBillboard",
            "PropsSpecialBillboardsRandomLargeBillboard",
        };

        public static void Initialize()
        {
            Dispose();
            var removerGo = new GameObject(gameObjectName);
            removerGo.AddComponent<PropRemover>();
        }

        public static void Dispose()
        {
            var removerGo = GameObject.Find(gameObjectName);
            if (removerGo == null)
            {
                return;
            }
            Destroy(removerGo);
        }

        public void Update()
        {
            RemoveProps();
            Dispose();
        }

        public static void RemoveProps()
        {
            var prefabs = Resources.FindObjectsOfTypeAll<BuildingInfo>();
            foreach (var buildingInfo in prefabs)
            {
                var fastList = new FastList<BuildingInfo.Prop>();
                if (buildingInfo == null)
                {
                    continue;
                }
                if (buildingInfo.m_props != null)
                {
                    var props = buildingInfo.m_props;
                    foreach (var prop in props.Where(prop => prop != null))
                    {
                        if (prop.m_finalProp != null && !prop.m_finalProp.m_isCustomContent)
                        {
                            if (
                                (OptionsWrapper<Options>.Options.removeSmoke &&
                                 (prop.m_finalProp.name.Contains("Smoke") || prop.m_finalProp.name.Contains("smoke"))) ||
                                (OptionsWrapper<Options>.Options.removeSteam &&
                                 (prop.m_finalProp.name.Contains("Steam") || prop.m_finalProp.name.Contains("steam"))) ||
                                (OptionsWrapper<Options>.Options.removeClownHeads &&
                                 (prop.m_finalProp.name.Contains("Clown") || prop.m_finalProp.name.Contains("clown"))) ||
                                (OptionsWrapper<Options>.Options.removeIceCones &&
                                 (prop.m_finalProp.name.Contains("morelloscone") || prop.m_finalProp.name.Contains("Cream") || prop.m_finalProp.name.Contains("cream"))) ||
                                (OptionsWrapper<Options>.Options.removeDoughnutSquirrels &&
                                 (prop.m_finalProp.name.Contains("Squirrel") ||
                                  prop.m_finalProp.name.Contains("squirrel"))) ||
                                (OptionsWrapper<Options>.Options.removeRandom3dBillboards &&
                                 prop.m_finalProp.name == "Billboard_3D_variation") ||
                                (OptionsWrapper<Options>.Options.removeFlatBillboards &&
                                 prop.m_finalProp.name == "Hologram Ad Game Arcade") ||
                                (OptionsWrapper<Options>.Options.removeAnimatedBillboards &&
                                 prop.m_finalProp.editorCategory == "PropsSpecialBillboardsAnimatedBillboard") ||
                                (OptionsWrapper<Options>.Options.removeLogoBillboards &&
                                 (prop.m_finalProp.editorCategory == "PropsBillboardsLogo" || prop.m_finalProp.editorCategory == "PropsBillboardsRandomLogo") && !prop.m_finalProp.name.Contains("roofad")) ||
                                (OptionsWrapper<Options>.Options.removeNeonChirpy &&
                                 BillboardCategories.Contains(prop.m_finalProp.editorCategory)) ||
                                (OptionsWrapper<Options>.Options.removeOctopodes &&
                                 (prop.m_finalProp.name.Contains("Octopus") || prop.m_finalProp.name.Contains("octopus"))) ||
                                (OptionsWrapper<Options>.Options.removeWallFlags &&
                                 prop.m_finalProp.name == "flag_pole_wall") ||
                                (OptionsWrapper<Options>.Options.removeSolarPanels &&
                                 prop.m_finalProp.name.Contains("Solar panel"))
                                 )
                            {
                                continue;
                            }
                            fastList.Add(prop);
                        }
                        else
                        {
                            fastList.Add(prop);
                        }
                    }
                }
                buildingInfo.m_props = fastList.ToArray();
            }
        }
    }
}
