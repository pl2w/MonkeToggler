using MonkeToggler.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeToggler.Toggler
{
    internal class TogglerButton : GorillaPressableButton
    {
        public ButtonType type;

        TogglerButton() => gameObject.layer = 13;

        public override void ButtonActivation()
        {
            base.ButtonActivation();
            MonkeTogglerController.instance.OnButtonPress(type);
        }
    }
}
