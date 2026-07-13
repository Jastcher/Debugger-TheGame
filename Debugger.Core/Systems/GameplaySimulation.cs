using System.Numerics;
using Debugger.Core.Components;
using Debugger.Core.Entities;

namespace Debugger.Core.Systems
{
    public class GameplaySimulation
    {
        public Player Player { get; set; }

        public List<Entity> Entities { get; set; }

        public RoomManager RoomManager { get; private set; } = new();

        public CollisionGrid? CurrentCollisionGrid { get; set; }

        public GameplaySimulation()
        {

            Player = new(new Vector2(50, 50));

            Entities = new();
            Entities.Add(Player);
        }

        public void GenerateDungeonLayout(int size)
        {
            RoomManager.GenLayout(size);
        }
        
        public void MoveRooms(Vector2 movementInput)
        {
            RoomManager.Move((int)movementInput.X, (int)movementInput.Y);
        }

        public void Update(Vector2 movementInput, float dt)
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

            Player.HandleAnimationState(movementInput);

            Player.Update(dt);
        }
    }
}