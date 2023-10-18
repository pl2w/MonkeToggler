using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;
using MonkeToggler.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeToggler.Patches
{
    public class MonkeTogglerController : DevHoldable
    {
        public static MonkeTogglerController instance;
        List<PluginInfo> disableableMods = new List<PluginInfo>();
        Text mainText;
        int currentModIndex;

        public void Start()
        {
            instance = this;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MonkeToggler.Resources.monketoggler");
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();

            GameObject pager = Instantiate(bundle.LoadAsset<GameObject>("Pager"));
            pager.transform.SetParent(transform);

            bundle.Unload(false);

            mainText = pager.transform.Find("Canvas/Main").GetComponent<Text>();

            transform.position = GorillaLocomotion.Player.Instance.transform.position;

            InitializeToggler();
        }

        private void InitializeToggler()
        {
            mainText.text = "LOADING MODS...";

            // get all the bepinex plugins and put them in the 'disableableMods' list if they can be disabled/enabled
            foreach (PluginInfo item in Chainloader.PluginInfos.Values)
            {
                MethodInfo enableMethod = AccessTools.Method(item.Instance.GetType(), "OnEnable");
                MethodInfo disableMethod = AccessTools.Method(item.Instance.GetType(), "OnDisable");
                
                if(enableMethod != null && disableMethod != null)
                {
                    disableableMods.Add(item);
                }
            }

            RefreshMainText();
        }

        public void RefreshMainText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("MONKETOGGLER");

            foreach (PluginInfo item in disableableMods)
            {
                string hexColor = (item.Instance.enabled ? "00FF00" : "FF0000");
                sb.AppendLine($"> <color=#{hexColor}>{item.Metadata.Name}</color>").AppendLine();
            }

            mainText.text = sb.ToString();
        }

        public void OnButtonPress(ButtonType type)
        {
            // just incase
            try
            {
                switch (type)
                {
                    case ButtonType.Up:
                        if (currentModIndex > 0)
                            currentModIndex--;
                        break;
                    case ButtonType.Down:
                        if (currentModIndex < disableableMods.Count - 1)
                            currentModIndex++;
                        break;
                    case ButtonType.Toggle:
                        disableableMods[currentModIndex].Instance.enabled = !disableableMods[currentModIndex].Instance.enabled;
                        break;
                }

                RefreshMainText();
            }
            catch (Exception e)
            {
                mainText.text = e.Message;
            }
        }
    }

    public enum ButtonType
    {
        Up,
        Down,
        Toggle
    }
}