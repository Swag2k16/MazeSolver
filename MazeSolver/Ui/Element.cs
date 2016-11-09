using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepesComing.Ui {
    public abstract class Element {
        public abstract void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites);

    }
}
