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
        Camera _camera = new();
        GameplaySimulation _simulation = new();
        WorldManager _worldManager;
        HudUI _hud;

        public GameScreen(Debugger game) : base(game)
        {
        }

        public override void Initialize()
        {
            _worldManager = new(_spriteBatch);
            _worldManager.Initialize("Content/world.ldtk");

            _simulation.RoomManager.OnRoomChanged += (newRoomIndex) =>
            {
                _simulation.CurrentCollisionGrid = _worldManager.GetCollisionGridForRoom(newRoomIndex);
            };

            _simulation.GenerateDungeonLayout(5);

            _hud = new(Game, AssetManager.DefaultUiStyle, _simulation.RoomManager);
        }

        public override void LoadContent()
        {
            _worldManager.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: _camera.GetTransformationMatrix(Game.GraphicsDevice)
            );

            _worldManager.RenderRoom(_simulation.RoomManager.CurrentRoom.RoomIndex);

            _renderer.Draw(_spriteBatch, _simulation.Entities);

            _renderer.DrawHitbox(_spriteBatch, (Core.Components.CircleHitbox)_simulation.Player.Hitbox, _simulation.Player.Position);

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
            if (InputSystem.IsPressedOnce(Keys.Up)) _simulation.MoveRooms(new(0, -1));
            else if (InputSystem.IsPressedOnce(Keys.Down)) _simulation.MoveRooms(new(0, 1));
            if (InputSystem.IsPressedOnce(Keys.Left)) _simulation.MoveRooms(new(-1, 0));
            else if (InputSystem.IsPressedOnce(Keys.Right)) _simulation.MoveRooms(new(1, 0));
            if (InputSystem.IsPressedOnce(Keys.RightShift)) _simulation.RoomManager.PrintMap();


            CoreVector2 movementInput = ProcessMovementInput();

            _simulation.Update(movementInput, dt);

            _camera.Follow(_simulation.Player.Position, 1.0f, dt);

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