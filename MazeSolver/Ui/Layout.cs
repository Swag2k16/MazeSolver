using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PepesComing.Ui {
    public abstract class Layout : Element {
        private readonly List<Element> elements;
        private readonly bool vertical;
        private readonly bool maximize;
        private readonly int padding;

        public Layout(bool vertical, bool maximize, int padding)
            : base(false) {
            elements = new List<Element>();
            this.vertical = vertical;
            this.maximize = maximize;
            this.padding = padding;
        }

        public Layout(int x, int y, int width, int height, bool vertical, bool maximize, int padding)
            : base(false) {
            elements = new List<Element>();
            this.vertical = vertical;
            this.maximize = maximize;
            this.padding = padding;

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void AddElements(params Element[] elements) {
            this.elements.AddRange(elements);
        }

        public override void RenderElement(SpriteBatch spriteBatch) {
            foreach (Element e in elements) {
                e.RenderElement(spriteBatch);
            }
        }

        public override bool Update() {
            bool handled = false;
            elements.ForEach(e => {
                if (e.Update()) {
                    handled = true;
                }
            });

            if (base.Update()) handled = true;
            return handled;
        }

        public override void CalculateLayout() {
            int elementDimension = vertical ? Height : Width;

            if (maximize) {
                // Calculate maximum element length
                int elementLength = (elementDimension - (elements.Count - 1) * padding) / elements.Count;

                // Resize each element
                for (int i = 0; i < elements.Count; i++) {
                    int pad = i == 0 ? 0 : padding;

                    if (vertical) {
                        elements[i].X = X;
                        elements[i].Y = Y + (i * elementLength) + (pad * i);
                        elements[i].Width = Width;
                        elements[i].Height = elementLength;
                    } else {
                        elements[i].X = X + (i * elementLength) + (pad * i);
                        elements[i].Y = Y;
                        elements[i].Width = elementLength;
                        elements[i].Height = Height;
                    }

                    elements[i].CalculateLayout();
                }
            } else {
                int totalLength = 0;
                elements.ForEach(e => {
                    totalLength += vertical ? e.Height : e.Width;
                });

                int spacing = elements.Count > 1 ? (elementDimension - totalLength) / (elements.Count - 1) : 0;
                int end = vertical ? Y : X;

                elements.ForEach(e => {
                    if (vertical) {
                        e.X = X;
                        e.Y = end;
                        e.Width = Width;
                        end += e.Height + spacing;
                    } else {
                        e.X = end;
                        e.Y = Y;
                        e.Height = Height;
                        end += e.Width + spacing;
                    }
                    e.CalculateLayout();
                });
            }
        }
    }
}