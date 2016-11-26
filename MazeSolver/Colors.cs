using XnaColor = Microsoft.Xna.Framework.Color;

namespace PepesComing {
    public enum Color {
        WHITE
    }

    public static class Colors {

        public static XnaColor Xna(this Color color) {
            switch (color) {
                case Color.WHITE:
                    return XnaColor.White;
            }
            return XnaColor.White;
        }
    }


}
