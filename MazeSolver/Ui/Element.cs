using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    public abstract class Element {
        protected Sprites sprites;

        public Element(Sprites sprites) {
            this.sprites = sprites;
        }

        public abstract void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites);
    }
}
