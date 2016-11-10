﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PepesComing.Ui {
    public class HoizontalLayout : Element {
        private List<Element> elements;
        private Rectangle position;
        private int padding;

        public HoizontalLayout(Rectangle position, int padding, Sprites sprites, GraphicsDevice graphics) : base(sprites, graphics) {
            this.position = position;
            this.padding = padding;
            elements = new List<Element>();
        }

        public void AddElement(Element element) {
            elements.Add(element);
            RecalculateLayout();
        }

        public override Rectangle Position {
            get {
                return position;
            }

            set {
                position = value;
                RecalculateLayout();
            }
        }

        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            foreach (Element e in elements) {
                e.RenderElement(graphics, spriteBatch, sprites);
            }
        }

        public override bool Update(Controller controller) {
            bool handled = false;
            foreach (Element e in elements) {
                if (e.Update(controller)) handled = true;
            }

            return handled;
        }

        private void RecalculateLayout() {
            int elementHeight = (this.position.Height - (elements.Count - 1) * padding) / elements.Count;

            for (int i = 0; i < elements.Count; i++) {
                int pad = i == 0 ? 0 : padding;
                elements[i].Position = new Rectangle(position.X, position.Y + (i * elementHeight) + (pad * i), position.Width, elementHeight);
            }
        }
    }
}