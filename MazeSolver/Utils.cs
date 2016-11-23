using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PepesComing {
    public static class Utils {

        public enum Compass {
            North, South, East, West
        }

        // Defines a generic struct for the four cardinal directions
        public struct Cardinal<T> {
            public T North;
            public T East;
            public T South;
            public T West;
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

        public static List<T> AsList<T>(this Cardinal<T> card) {
            var list = new List<T>();
            list.Add(card.North);
            list.Add(card.East);
            list.Add(card.South);
            list.Add(card.West);
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
