using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    public abstract class Element {
        protected Sprites sprites;
        protected GraphicsDevice graphics;

        public abstract Rectangle Position { get; set; }

        public Element(Sprites sprites, GraphicsDevice graphics) {
            this.sprites = sprites;
            this.graphics = graphics;
        }

        public abstract void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites);
        public abstract bool Update(Controller controller);
    }
}
