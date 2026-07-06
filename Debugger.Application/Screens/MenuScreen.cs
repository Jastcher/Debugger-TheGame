using Debugger.Application.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Debugger.Application.Screens
{
    public class MenuScreen : Screen
    {
        public MenuScreen(Debugger game) : base(game)
        {
        }

        public override void Draw()
        {
            _spriteBatch.Begin();
            Game.GraphicsDevice.Clear(Color.Purple);
            _spriteBatch.End();
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {

            if (InputSystem.IsPressedOnce(Keys.Escape))
            {
                Game.ScreenManager.PopScreen();
            }
        }
    }
}