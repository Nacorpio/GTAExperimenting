using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;

namespace GTAModExperimenting
{

    public class Experimenting : Script {

        private List<Blip> blips = new List<Blip>();

        private readonly UIMenu _menu1;
        private readonly Player _player = null;
       
        public Experimenting() {

            Tick += OnTick;
            KeyDown += OnKeyDown;

            _menu1 = new UIMenu("Main Menu");

            Initialize();

            _player = Game.Player;

        }

        private void Initialize() {

            UIButton radiusBlipButton = _menu1.AddButton("Create Radius Blip", "Creates a blip with a radius at the current coordinates");
            UIButton blipButton = _menu1.AddButton("Create Blip", "Creates a blip at the current coordinates");

            radiusBlipButton.Click += RadiusBlipButtonOnClick;
            blipButton.Click += BlipButtonOnClick;

        }

        private void BlipButtonOnClick(object tag, object sender, EventArgs eventArgs) {
            Blip blip = Blip.Create(_player.Character.Position);
        }

        private void RadiusBlipButtonOnClick(object tag, object sender, EventArgs eventArgs) {
            Blip blip = Blip.Create(_player.Character.Position, 100f);
        }


        private void OnKeyDown(object sender, KeyEventArgs e) {
            if (_menu1.Visible) _menu1.ProcessKey(e.KeyCode);
        }

        private void OnTick(Object obj, EventArgs e) {
            _menu1.Draw();
        }

    }

}
