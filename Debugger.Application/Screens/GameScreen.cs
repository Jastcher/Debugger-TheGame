using System;
using Debugger.Application.Components;
using Debugger.Application.Systems;
using Debugger.Core.Systems;
using LDtk;
using LDtk.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Debugger.Application.Screens
{
    public class GameScreen : Screen
    {
        EntityRenderer _renderer = new();
        
        Camera _camera = new();

        GameplaySimulation _simulation = new();

        LDtkWorld world;
        ExampleRenderer tilerenderer;
        public GameScreen(Debugger game) : base(game)
        {
        }

        public override void Initialize()
        {
            // TODO: fix relative path
            LDtkFile file = LDtkFile.FromFile("Content/world.ldtk");
            world = file.LoadWorld(file.Worlds[0].Iid);
            foreach (LDtkLevel room in world.Levels)
            {
                Console.WriteLine($"Found room named: {room.Identifier} at grid position X:{room.WorldX} Y:{room.WorldY}");

            }

            tilerenderer = new ExampleRenderer(_spriteBatch);

        }

        public override void LoadContent()
        {

            foreach (LDtkLevel level in world.Levels)
            {
                tilerenderer.PrerenderLevel(level);
            }
        }

        public override void Draw()
        {
            _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: _camera.GetTransformationMatrix(Game.GraphicsDevice)
        );
            foreach (LDtkLevel level in world.Levels)
            {
                tilerenderer.RenderPrerenderedLevel(level);
            }

            _renderer.Draw(_spriteBatch, _simulation.Entities);

            _spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (InputSystem.IsPressedOnce(Keys.Escape))
            {
                Game.ScreenManager.PushScreen(new MenuScreen(Game));
            }

            CoreVector2 movementInput = ProcessMovementInput();

            _simulation.Update(movementInput, dt);
            
            _camera.Follow(_simulation.Player.Position, 2.0f, dt);
            
            //Console.WriteLine($"player pos: {_simulation.Player.Position}");
            Console.WriteLine($"camera pos: {_camera.Position}");

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