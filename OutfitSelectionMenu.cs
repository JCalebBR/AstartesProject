using Kitchen.Modules;
using KitchenLib;
using UnityEngine;
using Kitchen;

namespace AstartesProject
{
    public class OutfitSelectionMenu<T> : KLMenu<T>
    {
        public OutfitSelectionMenu(Transform container, ModuleList module_list) : base(container, module_list)
        {
        }

        private int SelectedOutfit = 0;
        private int SelectedHat = 0;

        public override void Setup(int player_id)
        {
            PlayerManager pm = Unity.Entities.World.DefaultGameObjectInjectionWorld.GetExistingSystem<PlayerManager>();
            if (pm == null)
            {
                AddInfo("This menu is only available when you're the host.");

                AddButton(base.Localisation["MENU_BACK_SETTINGS"], delegate (int i)
                {
                    this.RequestPreviousMenu();
                }, 0, 1f, 0.2f);
                return;
            }
            Player player;
            pm.GetPlayer(player_id, out player, false);

            CPlayerCosmetics cosmetics = pm.EntityManager.GetComponentData<CPlayerCosmetics>(player.Entity);

            AddLabel("Outfit Selector");
            AddSelect<int>(OutfitOptions);
            OutfitOptions.OnChanged += delegate (object _, int result)
            {
                SelectedOutfit = result;
            };
            AddButton("Apply Outfit", delegate (int i)
            {
                cosmetics.Set(KitchenData.CosmeticType.Outfit, SelectedOutfit);
                pm.EntityManager.SetComponentData(player.Entity, cosmetics);
            }, 0, 1f, 0.2f);

            New<SpacerElement>();

            AddLabel("Hat Selector");
            AddSelect<int>(HatOptions);
            HatOptions.OnChanged += delegate (object _, int result)
            {
                SelectedHat = result;
            };
            AddButton("Apply Hat", delegate (int i)
            {
                cosmetics.Set(KitchenData.CosmeticType.Hat, SelectedHat);
                pm.EntityManager.SetComponentData(player.Entity, cosmetics);
            }, 0, 1f, 0.2f);

            New<SpacerElement>(true);
            New<SpacerElement>(true);
            AddButton(base.Localisation["MENU_BACK_SETTINGS"], delegate (int i)
            {
                this.RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }

        private Option<int> OutfitOptions = new Option<int>(Mod.OutfitIDS, Mod.OutfitIDS[0], Mod.OutfitNames);
        private Option<int> HatOptions = new Option<int>(Mod.HatIDS, Mod.HatIDS[0], Mod.HatNames);
    }
}