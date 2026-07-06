using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Debugger.Application.Screens
{
    public abstract class Screen
    {
        
        protected Debugger Game;
        
        protected SpriteBatch _spriteBatch;
        
        public Screen(Debugger game)
        {
            Game = game;
            
            _spriteBatch = new(Game.GraphicsDevice);
        }
        
        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
        
        public virtual void UnloadContent() { }
    }
    
}