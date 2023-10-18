using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MonkeToggler
{
    [BepInPlugin("pl2w.monketoggler", "MonkeToggler", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public Plugin() => new Harmony("pl2w.monketoggler").PatchAll(Assembly.GetExecutingAssembly());
    }
}
