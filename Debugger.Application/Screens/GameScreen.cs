using System;
using System.Collections.Generic;
using Debugger.Application.Systems;
using Debugger.Core.Entities;
using Debugger.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Debugger.Screens
{
    public class GameScreen : Screen
    {
        EntityRenderer renderer = new();
        List<Entity> entities = new();

        private Player player;
        public GameScreen(Debugger game) : base(game)
        {
        }

        public override void Initialize()
        {
            player = new(new CoreVector2(50, 50));
            entities.Add(player);
        }

        public override void LoadContent()
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            renderer.Draw(spriteBatch, entities);
        }


        public override void Update(GameTime gameTime)
        {
            if (InputSystem.IsPressedOnce(Keys.Escape))
            {
                Game.ScreenManager.PushScreen(new MenuScreen(Game));
            }

            CoreVector2 movementInput = ProcessMovementInput();

            if (movementInput != Vector2.Zero)
                player.Move(movementInput, (float)gameTime.ElapsedGameTime.TotalSeconds);

        }

        private CoreVector2 ProcessMovementInput()
        {
            var state = Keyboard.GetState();
            var direction = CoreVector2.Zero;

            if (state.IsKeyDown(Keys.W)) direction.Y -= 1f;
            else if (state.IsKeyDown(Keys.S)) direction.Y += 1f;
            if (state.IsKeyDown(Keys.A)) direction.X -= 1f;
            else if (state.IsKeyDown(Keys.D)) direction.X += 1f;

            return direction;
        }
    }
}