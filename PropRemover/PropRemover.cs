using System.Linq;
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
                        if (prop.m_finalProp != null)
                        {
                            if (
                                (!OptionsHolder.Options.removeSmoke || !prop.m_finalProp.name.Contains("Smoke") && !prop.m_finalProp.name.Contains("smoke")) &&
                                (!OptionsHolder.Options.removeSteam || !prop.m_finalProp.name.Contains("Steam") && !prop.m_finalProp.name.Contains("steam")) &&
                                (!OptionsHolder.Options.removeClownHeads || !prop.m_finalProp.name.Contains("Clown") && !prop.m_finalProp.name.Contains("clown")) &&
                                (!OptionsHolder.Options.removeIceCones || !prop.m_finalProp.name.Contains("Cream") && !prop.m_finalProp.name.Contains("cream")) &&
                                (!OptionsHolder.Options.removeDoughnutSquirrels || !prop.m_finalProp.name.Contains("Squirrel") && !prop.m_finalProp.name.Contains("squirrel")) &&
                                (!OptionsHolder.Options.removeRandom3dBillboards || prop.m_finalProp.name != "Billboard_3D_variation") &&
                                (!OptionsHolder.Options.removeFlatBillboards || !BillboardCategories.Contains(prop.m_finalProp.editorCategory))
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
                buildingInfo.m_props = fastList.ToArray();
            }
        }
    }
}
