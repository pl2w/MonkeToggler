using BepInEx;
using HarmonyLib;
using System.Reflection;

namespace MonkeToggler
{
    [BepInPlugin("pl2w.monketoggler", "MonkeToggler", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public Plugin() => new Harmony("pl2w.monketoggler").PatchAll(Assembly.GetExecutingAssembly());
    }
}
