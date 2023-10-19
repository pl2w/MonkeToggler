using MonkeToggler.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace MonkeToggler.Toggler
{
    public class TogglerButton : GorillaPressableButton
    {
        public ButtonType type;

        public override void Start()
        {
            gameObject.layer = 18;

            onPressButton = new UnityEvent();
            onPressButton.AddListener(new UnityAction(ButtonActivate));
        }

        public void ButtonActivate()
        {
            MonkeTogglerController.instance.OnButtonPress(type);
        }
    }
}
