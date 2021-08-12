using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace joystick_test_tool
{
    public class Buttons
    {
        public int NumButtons { get; set; }
        private Texture2D _atlas;
        private SpriteFont _titleFont, _dataFont;
        private Rectangle _background, _border, _inactive, _active, _disabled;
        private bool[] _states = new bool[128];

        public Buttons(int num_buttons, Texture2D atlas, SpriteFont titleFont, SpriteFont dataFont)
        {
            NumButtons = num_buttons;
            _atlas = atlas;
            _titleFont = titleFont;
            _dataFont = dataFont;
            _background = new Rectangle(0, 0, 558, 810);
            _border = new Rectangle(558, 0, 558, 810);
            _inactive = new Rectangle(1116, 0, 56, 36);
            _active = new Rectangle(1116, 36, 56, 36);
            _disabled = new Rectangle(0, 0, _inactive.Width, _inactive.Height);
        }

        public void Update(ButtonState[] states)
        {
            for (int b = 0; b < 128; b++)
            {
                if (b == NumButtons)
                    break;
                _states[b] = (states[b] == ButtonState.Pressed);
            }
        }
    
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            // spriteBatch.Begin() should have already been called in the main draw routine

            const int bodyOffsetY = 54;

            // panel background and border
            Rectangle panel = new Rectangle(
                (int)location.X,
                (int)location.Y,
                _background.Width,
                _background.Height
            );
            if (_states.Length > 0)
            {
                spriteBatch.Draw(_atlas, panel, _background, Theme.Button["background"]);
                spriteBatch.Draw(_atlas, panel, _border, Theme.Button["border"]);
            }
            else
            {
                spriteBatch.Draw(_atlas, panel, _background, Theme.Button["disabled"]);
                return;
            }

            // the rest only gets drawn if there are buttons defined
            
            // title text
            Rectangle titleBounds = new Rectangle(
                (int)location.X,
                (int)location.Y,
                _background.Width,
                bodyOffsetY
            );
            spriteBatch.DrawString(
                _titleFont,
                "BUTTONS",
                TextTools.CenterVector(_titleFont.MeasureString("BUTTONS"), titleBounds),
                Theme.Button["title"]
            );

            const int x_step = 65;
            const int y_step = 45;
            int x_start = (int)location.X + 24;
            int y_start = (int)location.Y + 77;
            int x = x_start;
            int y = y_start;

            for (int b = 0; b < 128; b++)
            {
                Rectangle button_dest = new Rectangle(x, y, _inactive.Width, _inactive.Height);
                if (b < NumButtons)
                {
                    Color textColor;
                    
                    if (_states[b])
                    {                        
                        spriteBatch.Draw(_atlas, button_dest, _active, Theme.Button["button_active"]);
                        textColor = Theme.Button["number_active"];
                    }
                    else
                    {
                        spriteBatch.Draw(_atlas, button_dest, _inactive, Theme.Button["button_inactive"]);
                        textColor = Theme.Button["number_inactive"];
                    }
                    spriteBatch.DrawString(
                        _dataFont,
                        (b + 1).ToString(),
                        TextTools.CenterVector(_dataFont.MeasureString((b + 1).ToString()), button_dest),
                        textColor
                    );                    
                }
                else
                    spriteBatch.Draw(_atlas, button_dest, _disabled, Theme.Button["button_disabled"]);

                if ((b + 1) % 8 == 0)
                {
                    x = x_start;
                    y += y_step;
                }
                else
                    x += x_step;
            }

            // spriteBatch.End() will be called in the main draw routine
        }
    }
}