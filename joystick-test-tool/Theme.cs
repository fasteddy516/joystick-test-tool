using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace joystick_test_tool
{
    public static class Theme
    {
        public static Dictionary<string, Color> Main = new Dictionary<string, Color>()
        {
            ["background"] = new Color(35, 35, 35),
            ["header"] = Color.Orange
        };

        public static Dictionary<string, Color> Axis = new Dictionary<string, Color>()
        {
            ["disabled"] = Color.Orange * 0.1f,
            ["background"] = Color.Orange * 0.1f,
            ["border"] = Color.Orange,
            ["title"] = Color.Black,
            ["track"] = Color.Orange * 0.2f,
            ["markers"] = Color.Orange * 0.5f,
            ["center"] = Color.Orange,
            ["indicator_inactive"] = Color.Orange,
            ["indicator_active"] = Color.DodgerBlue,
            ["data"] = Color.Black
        };

        public static Dictionary<string, Color> Button = new Dictionary<string, Color>()
        {
            ["disabled"] = Color.Orange * 0.1f,
            ["background"] = Color.Orange * 0.1f,
            ["border"] = Color.Orange,
            ["title"] = Color.Black,
            ["button_disabled"] = Color.Orange * 0.1f,
            ["button_inactive"] = Color.Orange,
            ["button_active"] = Color.DodgerBlue,
            ["number_inactive"] = Color.Orange,
            ["number_active"] = Color.White,
        };

        public static Dictionary<string, Color> Hat = new Dictionary<string, Color>()
        {
            ["disabled"] = Color.Orange * 0.1f,
            ["background"] = Color.Orange * 0.1f,
            ["border"] = Color.Orange,
            ["title"] = Color.Black,
            ["fill"] = Color.Orange * 0.2f,
            ["outline"] = Color.Orange,
            ["markers"] = Color.Orange * 0.5f,
            ["center"] = Color.Orange * 0.5f,
            ["indicator_inactive"] = Color.Orange,
            ["indicator_active"] = Color.DodgerBlue,
            ["data"] = Color.Black
        };
    }
}