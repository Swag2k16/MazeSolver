using System;
using Microsoft.Xna.Framework.Graphics;
using PepesComing.Ui;

public class Window : Element {

    public static int TITLE_HEIGHT = 30;
    public static int CLOSE_WIDTH = 30;

    private Panel title;
    private Button close;
    private Panel content;

    public Window(int x, int y, int width, int height, string name, Element windowContent)
        : base(x, y, width, height, true) {
        Text text = new Text(name);
        title = new Panel(x, y, width, TITLE_HEIGHT, text, 0, PepesComing.Sprite.DARK_RED);
        close = new Button("X", x + width - CLOSE_WIDTH, y, CLOSE_WIDTH, TITLE_HEIGHT);
        Layout layout = new VerticalLayout(width: width, height: height - TITLE_HEIGHT, padding:30);
        layout.AddElements(windowContent);
        content = new Panel(x, y + TITLE_HEIGHT, width, height - TITLE_HEIGHT, layout, 30, PepesComing.Sprite.GREY);
    }

    public override void CalculateLayout() {
        base.CalculateLayout();
        title.CalculateLayout();
        close.CalculateLayout();
        content.CalculateLayout();
    }

    public override bool Update() {
        bool handled = false;
        if (!handled) handled = content.Update();
        return base.Update();
    }

    public override void RenderElement(SpriteBatch spriteBatch) {
        title.RenderElement(spriteBatch);
        close.RenderElement(spriteBatch);
        content.RenderElement(spriteBatch);
    }
}
