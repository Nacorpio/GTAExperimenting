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

        private UISwitchButton createdBlipSwitchButton = null;
        private void Initialize() {

            UIButton modSpoilerButton = _menu1.AddButton("Add Spoiler", "Adds a spoiler to this vehicle");
            UIButton modWindowTintButton = _menu1.AddButton("Add Engine", "Adds a respectable engine to this vehicle");

            modSpoilerButton.Click += ModSpoilerButtonOnClick;
            modWindowTintButton.Click += ModWindowTintButtonOnClick;

        }

        private void ModWindowTintButtonOnClick(object tag, object sender, EventArgs eventArgs) {
            Ped playerPed = _player.Character;
            if (playerPed.IsInVehicle()) {

                Vehicle playerVehicle = playerPed.CurrentVehicle;
                playerVehicle.SetMod(VehicleMod.Engine, 4, true);
                Function.Call(Hash.MOD);

            }
        }

        private void ModSpoilerButtonOnClick(object tag, object sender, EventArgs eventArgs) {

            Ped playerPed = _player.Character;
            if (playerPed.IsInVehicle()) {

                Vehicle playerVehicle = playerPed.CurrentVehicle;
                playerVehicle.SetMod(VehicleMod.Spoilers, 0, true);

            }

        }

        private void OnKeyDown(object sender, KeyEventArgs e) {
            if (_menu1.Visible) _menu1.ProcessKey(e.KeyCode);
        }

        private void OnTick(Object obj, EventArgs e) {
            _menu1.Draw();
        }

    }

}
