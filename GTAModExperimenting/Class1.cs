using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;

namespace GTAModExperimenting
{

    public class Experimenting : Script {

        private readonly UIMenu _mainMenu1;

        private readonly UIMenu _subSpawn;
        private readonly UIMenu _subVehicle;
        private readonly UIMenu _subPed;
        private readonly UIMenu _subPlayer;

        private readonly Player _player = null;
       
        public Experimenting() {

            Tick += OnTick;
            KeyDown += OnKeyDown;

            _mainMenu1 = new UIMenu("Main Menu");
            _subSpawn = new UIMenu("Spawning");
            _subPed = new UIMenu("Ped");
            _subPlayer = new UIMenu("Player");
            _subVehicle = new UIMenu("Vehicle");

            _subSpawn.Visible = false;
            _subVehicle.Visible = false;
            _subPlayer.Visible = false;
            _subPed.Visible = false;

            Initialize();

            _player = Game.Player;

        }

        private void Initialize() {

            // Parents
            UIButton parentSpawnButton = _mainMenu1.AddButton("Spawn", "Spawning functions");
            UIButton parentVehicleButton = _mainMenu1.AddButton("Vehicle", "Vehicle functions");
            UIButton parentPedButton = _mainMenu1.AddButton("Ped", "Ped functions");
            UIButton parentPlayerButton = _mainMenu1.AddButton("Player", "Player functions");

            // Parents:Events
            parentSpawnButton.Click += ParentSpawnButtonOnClick;
            parentVehicleButton.Click += ParentVehicleButtonOnClick;
            parentPedButton.Click += ParentPedButtonOnClick;
            parentPlayerButton.Click += ParentPlayerButtonOnClick;

            // Spawning
            UIButton subSpawnVehicleButton = _subSpawn.AddButton("Spawn Vehicle", "Spawns a vehicle with a specified model");
            UIButton subSpawnPedButton = _subSpawn.AddButton("Spawn Ped", "Spawns a ped with a specified model");
            UIButton subSpawnObjectButton = _subSpawn.AddButton("Spawn Object", "Spawns an object with a specified model");
            UIToggleButton subVehicleWarpTButton = _subSpawn.AddToggleButton("Warp into vehicle", "Whether to warp into a newly spawned vehicle");
            UIButton subSpawnBackButton = _subSpawn.AddButton("Go Back", "Goes back to the main-menu");

            // Spawning:Events
            subSpawnBackButton.Click += GoBackMainMenuOnClick;

            // Vehicle
            UIButton subVehicleModButton = _subVehicle.AddButton("Mod", "Mods the current vehicle");
            UIButton subVehicleSettingsButton = _subVehicle.AddButton("Settings", "Settings of the current vehicle");
            UIButton subVehicleBackButton = _subVehicle.AddButton("Go Back", "Goes back to the main-menu");

            // Vehicle:Events
            subVehicleBackButton.Click += GoBackMainMenuOnClick;

            // Ped
            UIButton subPedButton = _subPed.AddButton("", "");

            // Player
            UIButton subPlayerButton = _subPlayer.AddButton("", "");


        }

        private void GoBackMainMenuOnClick(object tag, object sender, EventArgs eventArgs) {
            _subVehicle.Close();
            _subPed.Close();
            _subPlayer.Close();
            _subSpawn.Close();
            _subSpawn.Close();
            _mainMenu1.Open();
        }

        private void ParentPlayerButtonOnClick(object tag, object sender, EventArgs eventArgs) {
            _subPlayer.Open();
            _mainMenu1.Close();
        }

        private void ParentPedButtonOnClick(object tag, object sender, EventArgs eventArgs) {
            _subPed.Open();
            _mainMenu1.Close();
        }

        private void ParentVehicleButtonOnClick(object tag, object sender, EventArgs eventArgs) {
            _subVehicle.Open();
            _mainMenu1.Close();
        }

        private void ParentSpawnButtonOnClick(object tag, object sender, EventArgs eventArgs) {
            _subSpawn.Open();
            _mainMenu1.Close();
        }

        private void OnKeyDown(object sender, KeyEventArgs e) {
            if (_mainMenu1.Visible)
                _mainMenu1.ProcessKey(e.KeyCode);
            if (_subSpawn.Visible)
                _subSpawn.ProcessKey(e.KeyCode);
            if (_subPed.Visible)
                _subPed.ProcessKey(e.KeyCode);
            if (_subPlayer.Visible)
                _subPlayer.ProcessKey(e.KeyCode);
            if (_subVehicle.Visible)
                _subVehicle.ProcessKey(e.KeyCode);
        }

        private void OnTick(Object obj, EventArgs e) {
            _mainMenu1.Draw();
            _subSpawn.Draw();
            _subPed.Draw();
            _subPlayer.Draw();
            _subVehicle.Draw();
        }

    }

}
