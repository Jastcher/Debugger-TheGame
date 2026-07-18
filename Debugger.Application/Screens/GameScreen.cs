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
                _simulation.CurrentEntities ??= _worldManager.GetEntitiesForRoom(newRoomIndex);
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

            _renderer.Draw(_spriteBatch, _simulation.Player);

            _renderer.Draw(_spriteBatch, _simulation.CurrentEntities);

            _renderer.DrawHitbox(_spriteBatch, (Core.Components.CircleHitbox)_simulation.Player.Hitbox, _simulation.Player.Position);

            foreach (var entity in _simulation.CurrentEntities)
            {

                if (entity.Hitbox == null) continue;
                _renderer.DrawHitbox(_spriteBatch, (Core.Components.CircleHitbox)entity.Hitbox, entity.Position);
            }

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
            if (InputSystem.IsPressedOnce(Keys.NumPad8)) _simulation.MoveRooms(new(0, -1));
            else if (InputSystem.IsPressedOnce(Keys.NumPad2)) _simulation.MoveRooms(new(0, 1));
            if (InputSystem.IsPressedOnce(Keys.NumPad4)) _simulation.MoveRooms(new(-1, 0));
            else if (InputSystem.IsPressedOnce(Keys.NumPad6)) _simulation.MoveRooms(new(1, 0));
            if (InputSystem.IsPressedOnce(Keys.RightShift)) _simulation.RoomManager.PrintMap();


            CoreVector2 movementInput = ProcessMovementInput();
            CoreVector2 facingInput = ProcessFacingInput();

            _simulation.Update(movementInput, facingInput, dt);

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
        private CoreVector2 ProcessFacingInput()
        {
            var state = Keyboard.GetState();
            var direction = CoreVector2.Zero;

            if (state.IsKeyDown(Keys.Up)) direction.Y -= 1f;
            else if (state.IsKeyDown(Keys.Down)) direction.Y += 1f;
            if (state.IsKeyDown(Keys.Left)) direction.X -= 1f;
            else if (state.IsKeyDown(Keys.Right)) direction.X += 1f;

            return direction;
        }
    }
}