using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PepesComing.Ui {
    class Slider : Element {
        public const int RailHeight = 5;
        public const int HandleWidth = 30;
        private readonly Box rail;
        private readonly Box handle;


        private int steps;
        public int currentValue;
        private int handleStep;

        public Slider(int x = 0, int y = 0, int width = 0, int height = 0, int steps = 1, int currentValue = 1)
            : base(x, y, width, height, true) {
            this.steps = steps;
            this.currentValue = currentValue;

            rail = new Box(x, y + height / 2 - RailHeight / 2, width, RailHeight, Sprite.RED);
            handle = new Box(x, y, HandleWidth, height, Sprite.DARK_RED);   
        }

        public override void CalculateLayout() {
            base.CalculateLayout();
            rail.X = X;
            rail.Y = Y+Height/2-RailHeight/2;
            rail.Width = Width;
            rail.Height = RailHeight;

            handleStep = rail.Width / steps;

            handle.X = rail.X + currentValue * handleStep - HandleWidth / 2;
            handle.Y = Y;
            handle.Width = HandleWidth;
            handle.Height = Height;
        }

        public override void RenderElement(SpriteBatch spriteBatch) {
            rail.RenderElement(spriteBatch);
            handle.RenderElement(spriteBatch);
        }

        protected override void MouseDown() {
            currentValue = (int)MathHelper.Clamp((Controller.Instance.MousePosition.X - rail.X) / handleStep, 0, steps);
            handle.X = rail.X + currentValue * handleStep - HandleWidth / 2;
        }

        protected override void MouseUp() {
            base.MouseUp();
        }
    }
}
