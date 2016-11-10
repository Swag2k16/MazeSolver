using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    public abstract class Element {
        protected Sprites sprites;
        protected GraphicsDevice graphics;

        public Element(Sprites sprites, GraphicsDevice graphics) {
            this.sprites = sprites;
            this.graphics = graphics;
        }

        public abstract Rectangle Position { get; protected set; }

        public abstract void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites);
    }
}
