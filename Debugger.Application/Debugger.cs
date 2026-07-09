using Debugger.Application.Screens;
using Debugger.Application.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Debugger.Application
{

    public class Debugger : Game
    {
        private GraphicsDeviceManager _graphics;
        public ScreenManager ScreenManager;

        public Debugger()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            ScreenManager = new ScreenManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            AssetManager.LoadAll(Content, GraphicsDevice);

            ScreenManager.ChangeScreen(new GameScreen(this));
        }

        protected override void Update(GameTime gameTime)
        {

            InputSystem.Update();

            ScreenManager.ActiveScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            ScreenManager.DrawAll(gameTime);


            base.Draw(gameTime);
        }
    }

}