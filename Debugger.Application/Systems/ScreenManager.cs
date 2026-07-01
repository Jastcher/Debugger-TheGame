using System.Collections;
using System.Collections.Generic;
using Debugger.Screens;

namespace Debugger.Systems
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
    }
}