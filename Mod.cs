using KitchenLib;
using KitchenLib.Logging;
using KitchenLib.Logging.Exceptions;
using KitchenMods;
using System.Linq;
using UnityEngine;
using System.Reflection;
using AstartesProject.Definitions;
using KitchenLib.Event;
using Kitchen;
using System.Collections.Generic;
using KitchenLib.Customs;

namespace AstartesProject
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.jcaleb2014.astartesproject";
        public const string MOD_NAME = "Astartes Project";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "JCaleb2014";
        public const string MOD_GAMEVERSION = ">=1.1.9";

        public static AssetBundle bundle;
        public static KitchenLogger Logger;

        public static List<int> OutfitIDS = new List<int>();
        public static List<int> HatIDS = new List<int>();

        public static List<string> OutfitNames = new List<string>();
        public static List<string> HatNames = new List<string>();

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).FirstOrDefault() ?? throw new MissingAssetBundleException(MOD_GUID);
            Logger = InitLogger();

            RegisterHat<Mk4_Helmet>("Mk.4 Helmet");

            ModsPreferencesMenu<PauseMenuAction>.RegisterMenu("Astartes Project", typeof(OutfitSelectionMenu<PauseMenuAction>), typeof(PauseMenuAction));

            Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) =>
            {
                args.Menus.Add(typeof(OutfitSelectionMenu<PauseMenuAction>), new OutfitSelectionMenu<PauseMenuAction>(args.Container, args.Module_list));
            };
        }

        public void RegisterOutfit<T>(string displayName) where T : CustomPlayerCosmetic, new()
        {
            CustomPlayerCosmetic outfit = AddGameDataObject<T>();
            OutfitIDS.Add(outfit.ID);
            OutfitNames.Add(displayName);
        }

        public void RegisterHat<T>(string displayName) where T : CustomPlayerCosmetic, new()
        {
            CustomPlayerCosmetic outfit = AddGameDataObject<T>();
            HatIDS.Add(outfit.ID);
            HatNames.Add(displayName);
        }
    }
}

