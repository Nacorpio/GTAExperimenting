using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAModExperimenting {
    public class UIToggleButton : UIButton {

        public event ButtonEventHandler Toggled;
        private bool tgld = false;

        public UIToggleButton(string text, string desc, int x, int y)
            : base(text, desc, x, y, Color.White) {
            update();
        }

        private void OnToggle() {
            if (Toggled != null) {
                Toggled(this, this, EventArgs.Empty);
            }
        }

        public void Toggle() {
            tgld = !tgld;
            OnToggle();
            update();
        }

        private void update() {
            switch (tgld) {
                case true:
                    Text = Name + " (ON)";
                    break;
                case false:
                    Text = Name + " (OFF)";
                    break;
            }
        }

        public bool IsToggled {
            get {
                return tgld;
            }
        }

    }
}
