using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace joystick_test_tool
{
    public static class TextTools{
        public static Vector2 CenterVector(Vector2 size, Rectangle bounds)
        {            
            return new Vector2(
                bounds.X + (bounds.Width - (int)size.X) / 2,
                bounds.Y + (bounds.Height - (int)size.Y) / 2
            );
        }
    }
}