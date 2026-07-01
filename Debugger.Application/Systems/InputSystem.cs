using Microsoft.Xna.Framework.Input;

namespace Debugger.Systems
{
    public static class InputSystem
    {
        private static KeyboardState _currentState;
        private static KeyboardState _previousState;

        public static void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public static bool IsPressed(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        public static bool IsPressedOnce(Keys key)
        {
            return _currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);
        }

    }
}