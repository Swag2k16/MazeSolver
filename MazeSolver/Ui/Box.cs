using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PepesComing.Ui {
    class Box : Element {
        public Sprite Sprite { private get; set; }


        public Box(int x, int y, int width, int height, Sprite texture)
            : base(x, y, width, height, true) {
            Sprite = texture;
        }

        public override void RenderElement(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Sprite.Texture(), destinationRectangle: new Rectangle(X, Y, Width, Height), sourceRectangle: new Rectangle(0, 0, 16, 16), color: Microsoft.Xna.Framework.Color.White);
        }
    }
}
