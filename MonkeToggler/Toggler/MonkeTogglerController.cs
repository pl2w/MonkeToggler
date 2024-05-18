using BepInEx;
using BepInEx.Bootstrap;
using GorillaNetworking;
using HarmonyLib;
using MonkeToggler.Toggler;
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
        Text mainText, pageText;

        int currentModIndex;
        int maxModsPerPage = 8;
        int currentPage, maxPages;

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
            pageText = pager.transform.Find("Canvas/PageText").GetComponent<Text>();
            pager.transform.Find("pager/LeftButton").gameObject.AddComponent<TogglerButton>().type = ButtonType.Down;
            pager.transform.Find("pager/MiddleButton").gameObject.AddComponent<TogglerButton>().type = ButtonType.Toggle;
            pager.transform.Find("pager/RightButton").gameObject.AddComponent<TogglerButton>().type = ButtonType.Up;

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

            maxPages = Mathf.CeilToInt(disableableMods.Count / maxModsPerPage);

            RefreshMainText();
        }

        public void RefreshMainText()
        {
            currentPage = Mathf.CeilToInt(currentModIndex / maxModsPerPage);

            pageText.text = $"{currentPage + 1}/{maxPages + 1}";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"MONKETOGGLER");

            for (int i = currentPage * 8; i < disableableMods.Count; i++)
            {
                sb.AppendLine($"<color=#{(currentModIndex == i ? "FFFFFFFF" : "FFFFFF00")}>></color> <color=#{(disableableMods[i].Instance.enabled ? "00FF00" : "FF0000")}>{disableableMods[i].Metadata.Name.ToUpper()}</color>");
            }
            
            mainText.text = sb.ToString();
        }

        public void OnButtonPress(ButtonType type)
        {
            if(!Plugin.inModded)
            {
                mainText.text = "GO INTO A MODDED LOBBY!";
                return;
            }   

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
                        if(currentModIndex < disableableMods.Count - 1)
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
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UH OH :( AN ERROR HAS OCCURED!").AppendLine("‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
                sb.AppendLine(e.Message);
                mainText.text = sb.ToString();
            }
        }

#if DEBUG
        void OnGUI()
        {
            if(GUI.Button(new Rect(5,5,250,25), "up"))
            {
                OnButtonPress(ButtonType.Up);
            }
            if (GUI.Button(new Rect(5, 35, 250, 25), "down"))
            {
                OnButtonPress(ButtonType.Down);
            }
            if (GUI.Button(new Rect(5, 65, 250, 25), "toggle"))
            {
                OnButtonPress(ButtonType.Toggle);
            }
        }
#endif
    }

    public enum ButtonType
    {
        Up,
        Down,
        Toggle
    }
}