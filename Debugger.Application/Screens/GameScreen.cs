using System;
using Debugger.Application.Components;
using Debugger.Application.Systems;
using Debugger.Application.UI;
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
        ExampleRenderer _tileRenderer;

        Camera _camera = new();

        GameplaySimulation _simulation = new();

        RoomManager _roomManager = new();
        
        HudUI _hud;

        LDtkWorld world;
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

            _tileRenderer = new ExampleRenderer(_spriteBatch);

            _roomManager.GenLayout(5);
            
            _hud = new(Game, AssetManager.DefaultUiStyle, _roomManager);
        }

        public override void LoadContent()
        {
            foreach (LDtkLevel level in world.Levels)
            {
                _tileRenderer.PrerenderLevel(level);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: _camera.GetTransformationMatrix(Game.GraphicsDevice)
            );

            _tileRenderer.RenderPrerenderedLevel(world.Levels[_roomManager.CurrentRoom.RoomIndex]);

            _renderer.Draw(_spriteBatch, _simulation.Entities);

            // DEBUG
            _spriteBatch.DrawString(
                AssetManager.Font,
                $"{_roomManager.CurrentPosX} {_roomManager.CurrentPosY}",
                new Vector2(20, 20),
                Color.White
            );
            

            _spriteBatch.End();

            _hud.Draw(_spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (InputSystem.IsPressedOnce(Keys.Escape))
            {
                Game.ScreenManager.PushScreen(new PauseScreen(Game));
            }

            // DEBUG
            if (InputSystem.IsPressedOnce(Keys.Up)) _roomManager.Move(0, -1);
            else if (InputSystem.IsPressedOnce(Keys.Down)) _roomManager.Move(0, 1);
            if (InputSystem.IsPressedOnce(Keys.Left)) _roomManager.Move(-1, 0);
            else if (InputSystem.IsPressedOnce(Keys.Right)) _roomManager.Move(1, 0);
            if (InputSystem.IsPressedOnce(Keys.RightShift)) _roomManager.PrintMap();


            CoreVector2 movementInput = ProcessMovementInput();

            _simulation.Update(movementInput, dt);

            _camera.Follow(_simulation.Player.Position, 1.0f, dt);

            //Console.WriteLine($"player pos: {_simulation.Player.Position}");
            //Console.WriteLine($"camera pos: {_camera.Position}");
            
            _hud.Update(gameTime);

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