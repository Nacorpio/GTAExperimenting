using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAModExperimenting {
    public class UISwitchButton : UIButton {

        public event ButtonEventHandler Switch;

        private string _currentItem = "";
        private int _currentIndex = -1;
        private string[] _items;

        public UISwitchButton(string text, string desc, string[] items, int x, int y) : base(text, desc, x, y, Color.White) {
            this._items = items;
        }

        private void OnSwitch() {
            if (Switch != null) {
                Switch(this, this, EventArgs.Empty);
            }
        }

        private bool hasNext() {
            if (_currentIndex + 1 <= _items.Length - 1) {
                return true;
            }
            return false;
        }

        private bool hasPrevious() {
            if (_currentIndex - 1 >= 0) {
                return true;
            }
            return false;
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

        public string[] Items {
            get { return _items; }
            set { _items = value; }
        }

    }
}
