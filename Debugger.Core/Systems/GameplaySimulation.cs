using System.Numerics;
using System.Runtime.InteropServices;
using Debugger.Core.Components;
using Debugger.Core.Entities;
using Microsoft.VisualBasic;

namespace Debugger.Core.Systems
{
    public class GameplaySimulation
    {
        public Player Player { get; set; }


        public RoomManager RoomManager { get; private set; } = new();

        public List<Entity>? CurrentEntities
        {
            get => RoomManager.CurrentRoom.Entities;
            set => RoomManager.CurrentRoom.Entities = value;
        }
        public CollisionGrid? CurrentCollisionGrid { get; set; }

        public GameplaySimulation()
        {

            Player = new();

        }

        public void GenerateDungeonLayout(int size)
        {
            RoomManager.GenLayout(size);
        }

        public void MoveRooms(Vector2 movementInput)
        {
            RoomManager.Move((int)movementInput.X, (int)movementInput.Y);
        }

        public void Update(Vector2 movementInput, Vector2 facingInput, float dt)
        {

            Player.Move(movementInput, dt);

            if (CurrentCollisionGrid != null)
            {
                Vector2 pushback;
                if (CollisionSystem.CheckCircleVsGrid(Player.Position, Player.Hitbox as CircleHitbox, CurrentCollisionGrid, out pushback))
                {
                    Player.Position += pushback;
                }
            }

            if (CurrentEntities != null)
                for (int i = CurrentEntities.Count - 1; i >= 0; i--)
                {
                    var entity = CurrentEntities[i];

                    if (entity.Hitbox == null) continue;

                    Vector2 pushback;
                    if (CollisionSystem.CheckCircleVsCircle(Player.Position, Player.Hitbox as CircleHitbox, entity.Position, entity.Hitbox as CircleHitbox, out pushback))
                    {

                        Player.Position += pushback;

                        //CurrentEntities.RemoveAt(i);
                        Console.WriteLine("COLLIDED");
                    }

                }

            Player.MovementVector = movementInput;
            Player.FacingVector = facingInput;

            Player.Update(dt);
        }
    }
}