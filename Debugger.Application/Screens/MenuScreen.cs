using Debugger.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Debugger.Screens
{
    public class MenuScreen : Screen
    {
        public MenuScreen(Debugger game) : base(game)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Game.GraphicsDevice.Clear(Color.Purple);
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