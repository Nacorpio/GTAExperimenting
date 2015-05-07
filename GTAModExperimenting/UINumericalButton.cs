using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAModExperimenting {
    public class UINumericalButton : UIButton {

        public event ButtonEventHandler Increment;
        public event ButtonEventHandler Decrement;

        private static int value = -1;

        private int maxValue = 100;
        private int minValue = 0;

        private int incrementBy = 1;
        private int decrementBy = 1;

        public UINumericalButton(string text, string desc, int x, int y, int val, int max, int min, Color color)
            : base(text, desc, x, y, color) {
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
            get {
                return incrementBy;
            }
            set {
                incrementBy = value;
            }
        }

        public int DecrementBy {
            get {
                return decrementBy;
            }
            set {
                decrementBy = value;
            }
        }

        public int Value {
            get {
                return value;
            }
        }

        public int MaxValue {
            get {
                return maxValue;
            }
        }

        public int MinValue {
            get {
                return minValue;
            }
        }

    }
}
