using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using System.Drawing;

namespace GTAModExperimenting {

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
            get {
                return name;
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
            get {
                return mText.Size;
            }
            set {
                mText.Size = value;
            }
        }

        public Color ForeColor {
            get {
                return mText.Color;
            }
            set {
                mText.Color = value;
            }
        }

        public string Text {
            get {
                return mText.Text;
            }
            set {
                mText.Text = value;
            }
        }

        public bool Selected {
            get {
                return selected;
            }
            set {
                selected = value;
            }
        }

    }

}
