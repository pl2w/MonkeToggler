using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MonkeToggler.Patches
{
    [HarmonyPatch(typeof(GorillaTagger), "Awake"), HarmonyWrapSafe]
    internal class OnGameInitPatch
    {
        public static void Postfix()
        {
            GameObject.DontDestroyOnLoad(new GameObject("MonkeTogglerMain", typeof(MonkeTogglerController)));
        }
    }
}
