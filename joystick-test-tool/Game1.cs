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
        private SpriteFont titleFont, dataFont;
        private Texture2D tempBackground;
        private List<HatSwitch> hats = new List<HatSwitch>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            tempBackground = Content.Load<Texture2D>("textures/sections");

            hats.Add(new HatSwitch("HAT-1", hatAtlas, titleFont, dataFont));
            hats.Add(new HatSwitch("HAT-2", hatAtlas, titleFont, dataFont));
            hats.Add(new HatSwitch("HAT-3", hatAtlas, titleFont, dataFont));
            hats.Add(new HatSwitch("HAT-4", hatAtlas, titleFont, dataFont, enabled: false));
        }

        protected override void Update(GameTime gameTime)
        {
            for (int h = 0; h < hats.Count; h++)
            {
                hats[h].Update(Joystick.GetState(0).Hats[h].ToString());
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            int x, y, ox, oy;  // local scratch variables for drawing

            GraphicsDevice.Clear(Color.Black);

            // First render target is our "native" full size of 1600x900
            GraphicsDevice.SetRenderTarget(_fullsizeRenderTarget);

            // --- Rendering on the full size target starts here ---

            _spriteBatch.Begin();
            
            // this is temporary - just to help align individual components
            _spriteBatch.Draw(tempBackground, new Vector2(0, 0), Color.White);

            // HEADER ---------------------------------------------------------
            _spriteBatch.Draw(headerTexture, new Vector2(0, 0), Color.White);

            x = 1098; y = 72; ox = 252; oy = 414;
            hats[0].Draw(_spriteBatch, new Vector2(x,y));
            hats[1].Draw(_spriteBatch, new Vector2(x + ox, y));
            hats[2].Draw(_spriteBatch, new Vector2(x, y + oy));
            hats[3].Draw(_spriteBatch, new Vector2(x + ox, y + oy));

            _spriteBatch.DrawString(dataFont, Joystick.GetState(0).Axes[0].ToString(), new Vector2(100, 100), Color.Yellow);

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
