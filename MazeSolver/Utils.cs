using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PepesComing {
    public static class Utils {

        public enum Compass {
            North,
            East,
            South,
            West,
        }

        public static Compass Left(this Compass direction) {
            return (Compass)(direction > 0 ? (int)direction - 1 : 3);
        }

        public static Compass Right(this Compass direction) {
            return (Compass)((int)direction < 3 ? (int)direction + 1 : 0);
        }

        // Defines a generic struct for the four cardinal directions
        public struct Cardinal<T> {
            public T North;
            public T East;
            public T South;
            public T West;
        }

        public struct Directional<T> {
            public Compass Direction { get; private set; }
            public T Value { get; private set; }

            public Directional(Compass direction, T value) {
                Direction = direction;
                Value = value;
            }
        }

        public static void Set<T>(this Cardinal<T> card, Compass direction, T value) {
            switch (direction) {
                case Compass.North:
                    card.North = value;
                    break;
                case Compass.East:
                    card.East = value;
                    break;
                case Compass.South:
                    card.South = value;
                    break;
                case Compass.West:
                    card.West = value;
                    break;
            }
        }

        public static T Get<T>(this Cardinal<T> card, Compass direction) {
            switch (direction) {
                case Compass.North:
                    return card.North;
                case Compass.East:
                    return card.East;
                case Compass.South:
                    return card.South;
                case Compass.West:
                    return card.West;
            }
            return card.North;
        }

        public static List<Directional<T>> AsList<T>(this Cardinal<T> card) {
            var list = new List<Directional<T>>();
            list.Add(new Directional<T>(Compass.North, card.North));
            list.Add(new Directional<T>(Compass.East, card.East));
            list.Add(new Directional<T>(Compass.South, card.South));
            list.Add(new Directional<T>(Compass.West, card.West));
            return list;
        }

        public static bool VectorInRectangle(Rectangle rectangle, Vector2 vector) {
            return vector.X >= rectangle.X &&
                vector.X <= rectangle.X + rectangle.Width &&
                vector.Y >= rectangle.Y &&
                vector.Y <= rectangle.Y + rectangle.Height;
        }

        public static IList<T> Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = Game.rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}
