using System;
using System.Collections.Generic;
using Debugger.Application.Systems;
using Debugger.Core.Entities;
using Debugger.Core.Systems;
using Debugger.Systems;
using LDtk;
using LDtk.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Debugger.Screens
{
    public class GameScreen : Screen
    {
        EntityRenderer _renderer = new();

        GameplaySimulation _simulation = new();

        LDtkWorld world;
        ExampleRenderer tilerenderer;
        public GameScreen(Debugger game) : base(game)
        {
        }

        public override void Initialize()
        {
            LDtkFile file = LDtkFile.FromFile("Content/world.ldtk");
            world = file.LoadWorld(file.Worlds[0].Iid);
            foreach (LDtkLevel room in world.Levels)
            {
                Console.WriteLine($"Found room named: {room.Identifier} at grid position X:{room.WorldX} Y:{room.WorldY}");

            }

            tilerenderer = new ExampleRenderer(Game.SpriteBatch);

        }

        public override void LoadContent()
        {

            foreach (LDtkLevel level in world.Levels)
            {
                tilerenderer.PrerenderLevel(level);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (LDtkLevel level in world.Levels)
            {
                tilerenderer.RenderPrerenderedLevel(level);
            }

            _renderer.Draw(spriteBatch, _simulation.Entities);

        }

        public override void Update(GameTime gameTime)
        {
            if (InputSystem.IsPressedOnce(Keys.Escape))
            {
                Game.ScreenManager.PushScreen(new MenuScreen(Game));
            }

            CoreVector2 movementInput = ProcessMovementInput();

            _simulation.Update(movementInput, (float)gameTime.ElapsedGameTime.TotalSeconds);

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