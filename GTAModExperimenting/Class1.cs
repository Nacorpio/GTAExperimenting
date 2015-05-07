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

    public delegate void ButtonEventHandler(Object tag, Object sender, EventArgs e);

    public class UIButton : UIElement {

        public event ButtonEventHandler Click;
        public event ButtonEventHandler Select;

        private string name = "";

        private UIText mText = null;
        private UIText mDescription = null;

        private bool selected = false;

        public UIButton(String text, String desc, int x, int y, Color color) {
            mText = new UIText(text, new Point(x + 5, y + 5), 0.5f, color, 4, false);
            mDescription = new UIText(desc, new Point(x + 5, y + 24), 0.40f, Color.White, 1, false);
            name = text;
        }

        public void OnClick(EventArgs e) {
            if (Click != null) {
                if (selected)
                    Click(this, this, e);
            }
        }

        public void OnSelect(UIButton btn, EventArgs e) {
            if (Select != null) {
                if (selected)
                    Select(btn, this, e);
            }
        }

        public void Draw(int xMod, int yMod) {
            throw new NotImplementedException();
        }

        public void Draw() {
            mText.Draw();
            mDescription.Draw();
        }

        public string Name {
            get { return name; }
        }

        public Point Location { get; set; }
        public Color Color { get; set; }
        public bool Enabled { get; set; }

        public void toggleSelect() {
            selected = !selected;
            switch (selected) {
                case true:
                    // Text = "> " + Text + " <";
                    Size = 0.6f;
                    mDescription.Size = 0.45f;
                    break;
                case false:
                    // Text = Text.Replace(">", "").Replace("<", "").Trim();
                    Size = 0.5f;
                    mDescription.Size = 0.4f;
                    break;
            }
        }

        public float Size {
            get { return mText.Size; }
            set { mText.Size = value; }
        }

        public Color ForeColor {
            get { return mText.Color; }
            set { mText.Color = value;  }
        }

        public string Text {
            get { return mText.Text; }
            set { mText.Text = value; }
        }

        public bool Selected {
            get { return selected; }
            set { selected = value;  }
        }

    }

    public class UIToggleButton : UIButton {

        public event ButtonEventHandler Toggled;
        private bool tgld = false;

        public UIToggleButton(string text, string desc, int x, int y) : base(text, desc, x, y, Color.White) {
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
            get { return tgld; }
        }

    }

    public class UINumericalButton : UIButton {

        public event ButtonEventHandler Increment;
        public event ButtonEventHandler Decrement;

        private static int value = -1;

        private int maxValue = 100;
        private int minValue = 0;

        private int incrementBy = 1;
        private int decrementBy = 1;

        public UINumericalButton(string text, string desc, int x, int y, int val, int max, int min, Color color) : base(text, desc, x, y, color) {
            value = val;
            maxValue = max;
            minValue = min;
        }

        public void OnIncrement(EventArgs e) {
            if (Increment != null) {
                Increment(this, this, EventArgs.Empty);
            }
        }

        public void onDecrement(EventArgs e) {
            if (Decrement != null) {
                Decrement(this, this, EventArgs.Empty);
            }
        }

        public void increment() {
            if (value + incrementBy <= maxValue) {
                value += incrementBy;
                Text = Name + " (" + value.ToString() + ")";
            }
        }

        public void decrement() {
            if (value - decrementBy >= minValue) {
                value -= decrementBy;
                Text = Name + " (" + value.ToString() + ")";
            }
        }

        public int IncrementBy {
            get { return incrementBy; }
            set { incrementBy = value; }
        }

        public int DecrementBy {
            get { return decrementBy; }
            set { decrementBy = value; }
        }

        public int Value { get { return value; }}

        public int MaxValue { get { return maxValue; }}

        public int MinValue { get { return minValue; }}

    }

    public class UISwitchButton : UIButton {

        public event ButtonEventHandler Switch;

        private string _currentItem = "";
        private int _currentIndex = -1;
        private readonly string[] _items;

        public UISwitchButton(string text, string desc, string[] items, int x, int y) : base(text, desc, x, y, Color.White) {
            this._items = items;
        }

        private void OnSwitch() {
            if (Switch != null) {
                Switch(this, this, EventArgs.Empty);
            }
        }

        public void Next() {
            if (_currentIndex + 1 <= _items.Length - 1) {
                _currentIndex += 1;
                Select(_currentIndex);
            }
            else {
                _currentIndex = 0;
                Select(_currentIndex);
            }
        }

        public void Previous() {
            if (_currentIndex - 1 >= 0) {
                _currentIndex -= 1;
                Select(_currentIndex);
            }
            else {
                _currentIndex = _items.Length - 1;
                Select(_currentIndex);
            }
        }

        private void Select(int index) {
            _currentItem = _items[_currentIndex];
            OnSwitch();
            Text = Name + " (" + _currentItem + ")";
        }

        public string Value { get { return _currentItem; }}

        public string[] Items { get { return _items; }}

    }

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
                UINumericalButton numButton = (UINumericalButton) listButtons[index];
                numButton.increment();
            }
        }

        private void ToggleIndex(int index) {
            if (listButtons[index] is UIToggleButton) {
                UIToggleButton toggleButton = (UIToggleButton) listButtons[index];
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
                        }
                        
                        else {
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

        public Point Location { get; set; }

        public Color Color { get; set; }

        public bool Enabled { get; set; }

        public bool Visible {
            get {
                return _visible;
            }
            set {
                _visible = value;
            }
        }

    }

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
