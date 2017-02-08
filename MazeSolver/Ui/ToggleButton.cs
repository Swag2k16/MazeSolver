using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepesComing.Ui {
    class ToggleButton : Button {

        private bool isDown;
        private string upString;
        private string downString;

        public ToggleButton(string upString, string downString, int x = 0, int y = 0, int width = 0, int height = 0, Color textColor = Color.WHITE, Sprite buttonUp = Sprite.RED, Sprite buttonDown = Sprite.DARK_RED)
            :base(upString, x, y, width, height, textColor, buttonUp, buttonDown) {
            isDown = false;
            this.upString = upString;
            this.downString = downString;
        }

        protected override void MouseDown() {
            
        }

        protected override void MouseUp() {
            base.MouseUp();

            isDown = !isDown;
            if (isDown) {
                panel.Sprite = buttonDown;
                text.text = downString;
            } else {
                panel.Sprite = buttonUp;
                text.text = upString;
            }

          
        }

    }
}
