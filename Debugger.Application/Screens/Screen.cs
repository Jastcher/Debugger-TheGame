using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Debugger.Screens
{
    public abstract class Screen
    {
        
        protected Debugger Game;
        
        public Screen(Debugger game)
        {
            Game = game;
        }
        
        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        
        public virtual void UnloadContent() { }
    }
    
}