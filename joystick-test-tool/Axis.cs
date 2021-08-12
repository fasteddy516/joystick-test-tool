using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace joystick_test_tool
{
    public class Axis
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        private Texture2D _atlas;
        private SpriteFont _titleFont, _dataFont;
        private int _position;
        private Vector2 _i_offset = new Vector2(0, 0);
        private Rectangle _background, _border, _track, _markers, _center, _indicator;

        public Axis(string name, Texture2D atlas, SpriteFont titleFont, SpriteFont dataFont, bool enabled=true)
        {
            Name = name;
            Enabled = enabled;
            _atlas = atlas;
            _titleFont = titleFont;
            _dataFont = dataFont;
            _background = new Rectangle(0, 0, 108, 396);
            _border = new Rectangle(108, 0, 108, 396);
            _track = new Rectangle(216, 0, 108, 396);
            _markers = new Rectangle(324, 0, 108, 396); 
            _center = new Rectangle(432, 0, 108, 396);
            _indicator = new Rectangle(540, 0, 72, 21);
        }

        public void Update(int position)
        {
            const float increment = 276f / 65535f;

            _position = position;
            _i_offset = new Vector2(0, _position * increment);
        }
    
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            // spriteBatch.Begin() should have already been called in the main draw routine

            const int bodyWidth = 102;
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
                spriteBatch.Draw(_atlas, panel, _background, Theme.Axis["background"]);
                spriteBatch.Draw(_atlas, panel, _border, Theme.Axis["border"]);
            }
            else
            {
                spriteBatch.Draw(_atlas, panel, _background, Theme.Axis["disabled"]);
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
                Theme.Axis["title"]
            );

            // body
            spriteBatch.Draw(_atlas, panel, _track, Theme.Axis["track"]);
            spriteBatch.Draw(_atlas, panel, _markers, Theme.Axis["markers"]);

            // center marker
            Color _indicator_color = Theme.Axis["indicator_active"];
            
            if (_position == 0)
            {
                spriteBatch.Draw(_atlas, panel, _center, Theme.Axis["center"]);
                _indicator_color = Theme.Axis["indicator_inactive"];
            }

            // indicator
            Rectangle indicator = new Rectangle(
                (int)location.X + bodyOffsetX + (bodyWidth - _indicator.Width) / 2 - (int)_i_offset.X, 
                (int)location.Y + bodyOffsetY + (bodyHeight - _indicator.Height) / 2 - (int)_i_offset.Y, 
                _indicator.Width,
                _indicator.Height
            );
            spriteBatch.Draw(_atlas, indicator, _indicator, _indicator_color);

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
                _position.ToString(),
                TextTools.CenterVector(_dataFont.MeasureString(_position.ToString()), dataBounds),
                Theme.Axis["data"]
            );

            // spriteBatch.End() will be called in the main draw routine
        }
    }
}