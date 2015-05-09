using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;

namespace GTAModExperimenting {
    class UIForm : UIElement {

        private readonly UIRectangle mBackRectangle;
        private readonly UIText mCaptionText;
        private readonly UIText mDescriptionText;

        public UIForm(string caption, string desc, int x, int y, int width, int height) {
            mBackRectangle = new UIRectangle(new Point(x, y), new Size(width, height), Color.Black);
            mCaptionText = new UIText(caption, new Point(x, y), 0.50f, Color.White, 1, false);
            mDescriptionText = new UIText(desc, new Point(x, y + 20), 0.35f, Color.White, 4, false);
        }

        public void Draw(int xMod, int yMod) {
            throw new NotImplementedException();
        }

        public void Draw() {
            mBackRectangle.Draw();
            mCaptionText.Draw();
            mDescriptionText.Draw();
        }

        public string Description {
            get { return mDescriptionText.Text; }
            set { mDescriptionText.Text = value; }
        }

        public string Caption {
            get { return mCaptionText.Text; }
            set { mCaptionText.Text = value; }
        }

        public Point Location { get; set; }

        public Color Color { get; set; }

        public bool Enabled { get; set; }

    }
}
