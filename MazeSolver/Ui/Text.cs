using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    class Text : Element {
        private string text;
        private Microsoft.Xna.Framework.Vector2 position;
        private Microsoft.Xna.Framework.Color color;

        public Text(string text, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color){
            this.text = text;
            this.position = position;
            this.color = color;
        }

        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            spriteBatch.DrawString(sprites.Font, text, position, color);
        }
    }
}
