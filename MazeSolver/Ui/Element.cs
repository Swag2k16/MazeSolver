using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    public abstract class Element {
        public abstract void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites);
    }
}
