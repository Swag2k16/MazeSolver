// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// namespace PepesComing.Ui
// {
//     class Window : Element {

//         private Panel titlePanel;
//         private Button close;

//         public bool Show { get; set; }

//         private Rectangle position;
//         public override Rectangle Position {
//             get {
//                 return position;
//             }

//             set {
//                 position = value;
//             }
//         }

//         public Window(Rectangle position, Sprites sprites) : base(sprites) {
//             this.position = position;

//             Rectangle titleRect = new Rectangle(position.X, position.Y, Position.Width, 50);
//             Text titleText = new Ui.Text("New Maze", titleRect, Color.White, sprites);
//             close = new Button(new Rectangle(0, 0, 50, 50), Color.Red, Color.White, "X", sprites);
//             HorizontalLayout layout = new HorizontalLayout(titleRect, false, 10, sprites);
//             layout.AddElement(titleText);
//             layout.AddElement(close);

//             VerticalLayout vlayout = new VerticalLayout(position, false, 10, sprites);
//             vlayout.AddElement(layout);

//             titlePanel = new Panel(vlayout, 0, position, sprites.Grey, sprites);
//         }

//         public override void RenderElement(SpriteBatch spriteBatch, Sprites sprites) {
//             if (Show) titlePanel.RenderElement(spriteBatch, sprites);
//         }

//         public override bool Update(Controller controller) {
//             bool handled = false;

//             if (close.Clicked) {
//                 Show = false;
//             }

//             if (Show) handled = titlePanel.Update(controller);
//             return handled;
//         }
//     }
// }
