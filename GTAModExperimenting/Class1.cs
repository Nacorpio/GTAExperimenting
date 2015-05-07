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

        private UIMenu mMenu1 = null;
        private Player player = null;
       
        public Experimenting() {

            Tick += OnTick;
            KeyDown += OnKeyDown;

            mMenu1 = new UIMenu("Main Menu");

            Initialize();

            player = Game.Player;

        }

        private void Initialize() {

            UIButton btnKill = mMenu1.AddButton("Kill", "Kills the player");
            UIButton btnClose = mMenu1.AddButton("Close Menu", "Closes this menu");
            UIToggleButton btnToggle = mMenu1.AddToggleButton("Toggle", "An example toggle");
            UINumericalButton btnNumeric = mMenu1.AddNumericalButton("Numeric", "An example numeric", 0, 100, 0);
            UISwitchButton btnSwitch = mMenu1.AddSwitchButton("Switch", "An example switch", new string[] {"Element1", "Element2"});

            btnKill.Click += OnButtonKillClick;
            btnClose.Click += OnButtonCloseMenuClick;

        }

        private void OnButtonCloseMenuClick(object tag, object sender, EventArgs eventArgs) {
            mMenu1.Visible = false;
        }

        private void OnButtonKillClick(object tag, object sender, EventArgs e) {
            player.Character.Kill();
        }

        private void OnKeyDown(object sender, KeyEventArgs e) {
            mMenu1.ProcessKey(e.KeyCode);
        }

        private void OnTick(Object obj, EventArgs e) {
            mMenu1.Draw();
        }

    }

}
