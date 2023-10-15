using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Tower;
using Tower.Objs;
using Tower.Particles;
using Tower.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        public static Game1 self;

        public Game1()
        {
            self = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("font");
            Utils.get().batch = _spriteBatch;
            Utils.get().font = _font;
            Utils.get().content = Content;
            Utils.get()._graphics = _graphics;
            // TODO: use this.Content to load your game content here

            Utils.get().currentGame = new MenuScreen(false);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed )
                Exit();

            Utils.get().currentGame.update();

            // TODO: Add your update logic here
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            Utils.get().deltaTime = (float)delta;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);

            Utils.get().batch.Begin();

            Utils.get().currentGame.draw();

            Utils.get().batch.End();
        }
    }
}