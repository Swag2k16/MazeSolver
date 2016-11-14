using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    public abstract class Element {
        protected Sprites sprites;

        public abstract Rectangle Position { get; set; }

        public Element(Sprites sprites) {
            this.sprites = sprites;
        }

        public abstract void RenderElement(SpriteBatch spriteBatch, Sprites sprites);
        public abstract bool Update(Controller controller);
    }
}
