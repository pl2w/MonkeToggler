using BepInEx;
using HarmonyLib;
using System.Reflection;
using Utilla;

namespace MonkeToggler
{
    [BepInPlugin("pl2w.monketoggler", "MonkeToggler", "1.0.0")]
    [BepInDependency("org.legoandmars.gorillatag.utilla")]
    [ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        public static bool inModded = false;

        public Plugin() => new Harmony("pl2w.monketoggler").PatchAll(Assembly.GetExecutingAssembly());

        [ModdedGamemodeJoin]
        public void OnJoin()
        {
            inModded = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave()
        {
            inModded = false;
        }
    }
}
