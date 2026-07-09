using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Debugger.Application.Screens;
using Microsoft.Xna.Framework;

namespace Debugger.Application.Systems
{
    public class ScreenManager
    {

        Stack<Screen> _screenStack = new();

        public Screen ActiveScreen => _screenStack.Count > 0 ? _screenStack.Peek() : null;

        public void PushScreen(Screen screen)
        {
            _screenStack.Push(screen);
            screen.Initialize();
            screen.LoadContent();
        }


        public void PopScreen()
        {
            Screen discardedScreen = _screenStack.Pop();
            discardedScreen.UnloadContent();
        }
        
        public void ChangeScreen(Screen screen)
        {
            
            while (_screenStack.Count > 0)
            {
                _screenStack.Pop().UnloadContent();
            }
            
            PushScreen(screen);
        }
        
        public void DrawActive(GameTime gameTime)
        {
            ActiveScreen.Draw(gameTime);
        }
        
        public void DrawAll(GameTime gameTime)
        {
            foreach (var screen in _screenStack.AsEnumerable().Reverse())
            {
                screen.Draw(gameTime);
            }
        }
    }
}