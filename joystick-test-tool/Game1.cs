using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace joystick_test_tool
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _fullsizeRenderTarget;
        private Texture2D axisAtlas, buttonAtlas, hatAtlas, headerTexture;
        private SpriteFont titleFont, dataFont, buttonFont;
        private List<HatSwitch> hats = new List<HatSwitch>();
        private List<Axis> axes = new List<Axis>();
        private Buttons buttons;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //Window.IsBorderless = true;
        }

        protected override void Initialize()
        {
            // Do all background rendering at 1600x900
            _fullsizeRenderTarget = new RenderTarget2D(GraphicsDevice, 1600, 900);

            // Render at 1600x900 for 4K+ displays, 800x450 for everything else
            if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height > 1200)
            {
                _graphics.PreferredBackBufferWidth = 1600;
                _graphics.PreferredBackBufferHeight = 900;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = 800;
                _graphics.PreferredBackBufferHeight = 450;
            }
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            axisAtlas = Content.Load<Texture2D>("textures/axis");
            buttonAtlas = Content.Load<Texture2D>("textures/button");
            hatAtlas = Content.Load<Texture2D>("textures/hat");
            headerTexture = Content.Load<Texture2D>("textures/section-title");

            titleFont = Content.Load<SpriteFont>("fonts/title");
            dataFont = Content.Load<SpriteFont>("fonts/data");
            buttonFont = Content.Load<SpriteFont>("fonts/button");

            axes.Add(new Axis("X", axisAtlas, titleFont, dataFont));
            axes.Add(new Axis("Y", axisAtlas, titleFont, dataFont));
            axes.Add(new Axis("Z", axisAtlas, titleFont, dataFont));
            axes.Add(new Axis("RX", axisAtlas, titleFont, dataFont));
            axes.Add(new Axis("RY", axisAtlas, titleFont, dataFont));
            axes.Add(new Axis("RZ", axisAtlas, titleFont, dataFont));
            axes.Add(new Axis("S0", axisAtlas, titleFont, dataFont));
            axes.Add(new Axis("S1", axisAtlas, titleFont, dataFont));

            buttons = new Buttons(Joystick.GetCapabilities(0).ButtonCount, buttonAtlas, titleFont, buttonFont);

            hats.Add(new HatSwitch("HAT-1", hatAtlas, titleFont, dataFont));
            hats.Add(new HatSwitch("HAT-2", hatAtlas, titleFont, dataFont));
            hats.Add(new HatSwitch("HAT-3", hatAtlas, titleFont, dataFont));
            hats.Add(new HatSwitch("HAT-4", hatAtlas, titleFont, dataFont));
        }

        protected override void Update(GameTime gameTime)
        {
            for (int a = 0; a < axes.Count; a++)
            {
                axes[a].Update(Joystick.GetState(0).Axes[a]);
            }

            buttons.Update(Joystick.GetState(0).Buttons);

            for (int h = 0; h < hats.Count; h++)
            {
                hats[h].Update(Joystick.GetState(0).Hats[h].ToString());
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            int x, y, ox, oy;  // local scratch variables for drawing

            // First render target is our "native" full size of 1600x900
            GraphicsDevice.SetRenderTarget(_fullsizeRenderTarget);
            GraphicsDevice.Clear(Theme.Main["background"]);

            // --- Rendering on the full size target starts here ---

            _spriteBatch.Begin();
            
            // HEADER ---------------------------------------------------------
            _spriteBatch.Draw(headerTexture, new Vector2(0, 0), Theme.Main["header"]);

            x = 18; y = 72; ox = 126; oy = 414;
            axes[0].Draw(_spriteBatch, new Vector2(x, y));
            axes[1].Draw(_spriteBatch, new Vector2(x + ox, y));
            axes[2].Draw(_spriteBatch, new Vector2(x + 2 * ox, y));
            axes[3].Draw(_spriteBatch, new Vector2(x + 3 * ox, y));
            axes[4].Draw(_spriteBatch, new Vector2(x, y + oy));
            axes[5].Draw(_spriteBatch, new Vector2(x + ox, y + oy));
            axes[6].Draw(_spriteBatch, new Vector2(x + 2 * ox, y + oy));
            axes[7].Draw(_spriteBatch, new Vector2(x + 3 * ox, y + oy));           

            buttons.Draw(_spriteBatch, new Vector2(522, 72));

            x = 1098; y = 72; ox = 252; oy = 414;
            hats[0].Draw(_spriteBatch, new Vector2(x,y));
            hats[1].Draw(_spriteBatch, new Vector2(x + ox, y));
            hats[2].Draw(_spriteBatch, new Vector2(x, y + oy));
            hats[3].Draw(_spriteBatch, new Vector2(x + ox, y + oy));

            _spriteBatch.End();

            // --- Rendering on the full size target stops here ---

            // Scale and output rendered full-size target to actual size
            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_fullsizeRenderTarget, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
