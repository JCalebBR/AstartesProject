using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace AstartesProject.Definitions
{
    public class Mk4_Helmet : CustomPlayerCosmetic
    {
        public override string UniqueNameID => "Mk4_Helmet";
        public override CosmeticType CosmeticType => CosmeticType.Hat;
        public override GameObject Visual => Mod.bundle.LoadAsset<GameObject>("Mk4_Helmet");
        public override bool BlockHats => true;

        public override void OnRegister(GameDataObject gameDataObject)
        {
            GameObject Prefab = ((PlayerCosmetic)gameDataObject).Visual;

            PlayerOutfitComponent playerOutfitComponent = Prefab.AddComponent<PlayerOutfitComponent>();
            playerOutfitComponent.Renderers.Add(GameObjectUtils.GetChildObject(Prefab, "Mk.4 Helmet").GetComponent<SkinnedMeshRenderer>());
        }
    }
}