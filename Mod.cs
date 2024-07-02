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

            AddGameDataObject<Mk4_Helmet>();
        }
    }
}