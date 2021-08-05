using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace joystick_test_tool
{
    public class HatSwitch
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        private Texture2D _atlas;
        private SpriteFont _titleFont, _dataFont;
        private string _position;
        private Vector2 _i_offset = new Vector2(0, 0);
        private Rectangle _background, _border, _outline, _fill, _markers, _center, _indicator;

        public HatSwitch(string name, Texture2D atlas, SpriteFont titleFont, SpriteFont dataFont, bool enabled=true)
        {
            Name = name;
            Enabled = enabled;
            _atlas = atlas;
            _titleFont = titleFont;
            _dataFont = dataFont;
            _background = new Rectangle(0, 0, 234, 396);
            _border = new Rectangle(234, 0, 234, 396);
            _outline = new Rectangle(468, 0, 214, 215);
            _fill = new Rectangle(468, 215, 214, 215);
            _markers = new Rectangle(682, 0, 214, 215); 
            _center = new Rectangle(682, 215, 214, 215);
            _indicator = new Rectangle(896, 0, 74, 73);
        }

        public void Update(string position)
        {
            const int mainOffset = 60;
            const int diagOffset = 43;

            Vector2 offset = new Vector2(0, 0);

            // string contains binary representation of "LURD"
            switch(position)
            {
                case "0001":
                    _position = "DOWN";
                    offset.Y -= mainOffset;
                    break;
                case "0010":
                    _position = "RIGHT";
                    offset.X -= mainOffset;
                    break;
                case "0011":
                    _position = "DOWN + RIGHT";
                    offset.X -= diagOffset;
                    offset.Y -= diagOffset;
                    break;
                case "0100":
                    _position = "UP";
                    offset.Y += mainOffset;
                    break;
                case "0110":
                    _position = "UP + RIGHT";
                    offset.X -= diagOffset;
                    offset.Y += diagOffset;
                    break;
                case "1000":
                    _position = "LEFT";
                    offset.X += mainOffset;
                    break;
                case "1001":
                    _position = "DOWN + LEFT";
                    offset.X += diagOffset;
                    offset.Y -= diagOffset;
                    break;
                case "1100":
                    _position = "UP + LEFT";
                    offset.X += diagOffset;
                    offset.Y += diagOffset;
                    break;
                default:
                    _position = "IDLE";
                    //_ind_color = IdleColor;
                    break;
            }
            _i_offset = offset;
        }
    
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            // spriteBatch.Begin() should have already been called in the main draw routine

            const int bodyWidth = 228;
            const int bodyHeight = 306;
            const int bodyOffsetX = 3;
            const int bodyOffsetY = 54;

            // panel background and border
            Rectangle panel = new Rectangle(
                (int)location.X,
                (int)location.Y,
                _background.Width,
                _background.Height
            );
            if (Enabled)
            {
                spriteBatch.Draw(_atlas, panel, _background, Color.White * 0.5f);
                spriteBatch.Draw(_atlas, panel, _border, Color.White);
            }
            else
            {
                spriteBatch.Draw(_atlas, panel, _background, Color.White * 0.1f);
                return;
            }

            // the rest only gets drawn if Enabled=true
            
            // title text
            Rectangle titleBounds = new Rectangle(
                (int)location.X,
                (int)location.Y,
                _background.Width,
                bodyOffsetY
            );
            spriteBatch.DrawString(
                _titleFont,
                Name,
                TextTools.CenterVector(_titleFont.MeasureString(Name), titleBounds),
                Color.Black
            );

            // body
            Rectangle body = new Rectangle(
                (int)location.X + bodyOffsetX + (bodyWidth - _outline.Width) / 2, 
                (int)location.Y + bodyOffsetY + (bodyHeight - _outline.Height) / 2,
                _outline.Width,
                _outline.Height
            );
            spriteBatch.Draw(_atlas, body, _fill, Color.White * 0.5f);
            spriteBatch.Draw(_atlas, body, _outline, Color.White);
            spriteBatch.Draw(_atlas, body, _markers, Color.White);
            spriteBatch.Draw(_atlas, body, _center, Color.White);

            // indicator
            Rectangle indicator = new Rectangle(
                (int)location.X + bodyOffsetX + (bodyWidth - _indicator.Width) / 2 - (int)_i_offset.X, 
                (int)location.Y + bodyOffsetY + (bodyHeight - _indicator.Height) / 2 - (int)_i_offset.Y, 
                _indicator.Width,
                _indicator.Height
            );
            spriteBatch.Draw(_atlas, indicator, _indicator, Color.White);

            // data text
            int dataOffsetY = (int)location.Y + bodyOffsetY + bodyHeight;
            Rectangle dataBounds = new Rectangle(
                (int)location.X,
                (int)location.Y + bodyOffsetY + bodyHeight,
                _background.Width,
                _background.Height - bodyOffsetY - bodyHeight
            );
            spriteBatch.DrawString(
                _dataFont,
                _position,
                TextTools.CenterVector(_dataFont.MeasureString(_position), dataBounds),
                Color.Black
            );

            // spriteBatch.End() will be called in the main draw routine
        }
    }
}