using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Policy;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GTA;
using GTA.Math;
using GTA.Native;

namespace GTAModExperimenting
{

    public class Experimenting : Script {

        private readonly UIMenu _menu1 = null;
        private readonly UIMenu _menu2 = null;

        private readonly Player _player = null;
       
        public Experimenting() {

            Tick += OnTick;
            KeyDown += OnKeyDown;

            _menu1 = new UIMenu("Main Menu");
            _menu2 = new UIMenu("Sub Menu #1");
            _menu2.Visible = false;

            Initialize();

            _player = Game.Player;

        }

        private void Initialize() {

            UIButton btnKill = _menu1.AddButton("Kill", "Kills the player");
            UIButton btnOpenSub = _menu1.AddButton("Open Sub Menu", "Closes this menu");

            UIToggleButton btnToggle = _menu1.AddToggleButton("Toggle", "An example toggle");
            UINumericalButton btnNumeric = _menu1.AddNumericalButton("Numeric", "An example numeric", 0, 100, 0);
            UISwitchButton btnSwitch = _menu1.AddSwitchButton("Switch", "An example switch", new string[] {"Element1", "Element2", "Element3"});

            UIButton exampleButton = _menu2.AddButton("Example", "An example button");
            UIButton backButton = _menu2.AddButton("Go back", "Go back to main-menu");

            btnKill.Click += OnButtonKillClick;
            btnOpenSub.Click += OnButtonCloseMenuClick;
            backButton.Click += BackButtonOnClick;

        }

        private void BackButtonOnClick(object tag, object sender, EventArgs eventArgs) {
            _menu1.Open();
            _menu2.Close();
        }

        private void OnButtonCloseMenuClick(object tag, object sender, EventArgs eventArgs) {
            _menu1.Close();
            _menu2.Open();
        }

        private void OnButtonKillClick(object tag, object sender, EventArgs e) {
            _player.Character.Kill();
        }

        private void OnKeyDown(object sender, KeyEventArgs e) {
            if (_menu1.Visible) _menu1.ProcessKey(e.KeyCode);
            if (_menu2.Visible) _menu2.ProcessKey(e.KeyCode);  
        }

        private void OnTick(Object obj, EventArgs e) {
            _menu1.Draw();
            _menu2.Draw();
        }

    }

}
