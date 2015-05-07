using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;

namespace GTAModExperimenting {

    public class UIMenu : UIElement {

        private string _id;

        private bool _visible = true;
        private int _selectedIndex = 0;

        // TODO: Add a caption for the menu.
        private UIText mCaption = null;

        private readonly List<UIButton> listButtons = new List<UIButton>();

        public UIMenu(string id) {
            _id = id;
        }

        public UIButton Selected {
            get {
                return listButtons[_selectedIndex];
            }
        }

        public List<UIButton> Buttons {
            get {
                return listButtons;
            }
        }

        private void SelectNext() {
            if (_selectedIndex + 1 != listButtons.Count) {
                _selectedIndex += 1;
                SelectIndex(_selectedIndex);
                SelectIndex(_selectedIndex - 1);
            } else {
                SelectIndex(_selectedIndex = 0);
                SelectIndex(listButtons.Count - 1);
            }
        }

        private void SelectPrevious() {
            if (_selectedIndex - 1 != -1) {
                _selectedIndex -= 1;
                SelectIndex(_selectedIndex);
                SelectIndex(_selectedIndex + 1);
            } else {
                SelectIndex(_selectedIndex = listButtons.Count - 1);
                SelectIndex(0);
            }
        }

        private void SelectIndex(int index) {
            UIButton btn = listButtons[index];
            btn.toggleSelect();
            btn.OnSelect(btn, EventArgs.Empty);
        }

        private void UseSelected() {
            UIButton btn = listButtons[_selectedIndex];
            if (btn is UIToggleButton) {
                ToggleSelected();
            }
        }

        private void ToggleSelected() {
            ToggleIndex(_selectedIndex);
        }

        private void IncrementSelected() {
            IncrementIndex(_selectedIndex);
        }

        private void DecrementSelected() {
            DecrementIndex(_selectedIndex);
        }

        private void DecrementIndex(int index) {
            if (listButtons[index] is UINumericalButton) {
                UINumericalButton numButton = (UINumericalButton)listButtons[index];
                numButton.decrement();
            }
        }

        private void IncrementIndex(int index) {
            if (listButtons[index] is UINumericalButton) {
                UINumericalButton numButton = (UINumericalButton)listButtons[index];
                numButton.increment();
            }
        }

        private void ToggleIndex(int index) {
            if (listButtons[index] is UIToggleButton) {
                UIToggleButton toggleButton = (UIToggleButton)listButtons[index];
                toggleButton.Toggle();
            }
        }

        private const int y_spacing = 40;

        public UISwitchButton AddSwitchButton(String text, String desc, String[] items) {
            if (listButtons.Count == 1)
                SelectIndex(0);
            UISwitchButton btn = new UISwitchButton(text, desc, items, 5, y_spacing * listButtons.Count);
            listButtons.Add(btn);
            return btn;
        }

        public UIButton AddButton(String text, String desc) {
            if (listButtons.Count == 1)
                SelectIndex(0);
            UIButton btn = new UIButton(text, desc, 5, y_spacing * listButtons.Count, Color.White);
            listButtons.Add(btn);
            return btn;
        }

        public UIToggleButton AddToggleButton(String text, String desc) {
            if (listButtons.Count == 1)
                SelectIndex(0);
            UIToggleButton btn = new UIToggleButton(text, desc, 5, y_spacing * listButtons.Count);
            listButtons.Add(btn);
            return btn;
        }

        public UINumericalButton AddNumericalButton(String text, String desc, int value, int max, int min) {
            if (listButtons.Count == 1)
                SelectIndex(0);
            UINumericalButton btn = new UINumericalButton(text, desc, 5, y_spacing * listButtons.Count, value, max, min, Color.White);
            listButtons.Add(btn);
            return btn;
        }

        public void Draw(int xMod, int yMod) {
            throw new NotImplementedException();
        }

        public void ProcessKey(Keys key) {
            if (Selected != null) {
                switch (key) {
                    case Keys.NumPad2:
                        SelectNext();
                        break;
                    case Keys.NumPad8:
                        SelectPrevious();
                        break;
                    case Keys.NumPad6:
                        if (Selected is UINumericalButton) {
                            IncrementSelected();
                        } else if (Selected is UISwitchButton) {
                            UISwitchButton switchButton = (UISwitchButton)Selected;
                            switchButton.Next();
                        } else {
                            UseSelected();
                        }
                        break;
                    case Keys.NumPad4:

                        if (Selected is UINumericalButton) {
                            DecrementSelected();
                        }

                        if (Selected is UISwitchButton) {
                            UISwitchButton switchButton = (UISwitchButton)Selected;
                            switchButton.Previous();
                        } else {
                            UseSelected();
                        }

                        break;
                    case Keys.NumPad5:
                        Selected.OnClick(EventArgs.Empty);
                        break;
                }
            }
        }

        public void Draw() {
            if (_visible) {
                for (int i = 0; i < listButtons.Count; i++) {
                    UIButton e = listButtons[i];
                    e.Draw();
                }
            }
        }

        public Point Location {
            get;
            set;
        }

        public Color Color {
            get;
            set;
        }

        public bool Enabled {
            get;
            set;
        }

        public bool Visible {
            get {
                return _visible;
            }
            set {
                _visible = value;
            }
        }

    }

}
