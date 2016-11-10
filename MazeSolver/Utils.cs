using Microsoft.Xna.Framework;

namespace PepesComing {
    public static class Utils {

        // Defines a generic struct for the four cardinal directions
        public struct Cardinal<T> {
            public T North;
            public T East;
            public T South;
            public T West;
        }

        public static bool VectorInRectangle(Rectangle rectangle, Vector2 vector) {
            return vector.X >= rectangle.X &&
                vector.X <= rectangle.X + rectangle.Width &&
                vector.Y >= rectangle.Y &&
                vector.Y <= rectangle.Y + rectangle.Height;
        }

    }
}
