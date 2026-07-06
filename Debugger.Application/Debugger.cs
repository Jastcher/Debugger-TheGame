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
            ScreenManager = new ScreenManager();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ScreenManager.ChangeScreen(new GameScreen(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            AssetManager.LoadAll(Content);

            ScreenManager.ActiveScreen.LoadContent();
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


            ScreenManager.ActiveScreen.Draw();


            base.Draw(gameTime);
        }
    }

}