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

        public List<Entity>? CurrentEntities { get; set; }
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
            foreach (var entity in CurrentEntities)
            {
                
                if (entity.Hitbox == null) continue;
                
                Vector2 pushback;
                if (CollisionSystem.CheckCircleVsCircle(Player.Position, Player.Hitbox as CircleHitbox,entity.Position, entity.Hitbox as CircleHitbox,  out pushback))
                {
                    
                    Player.Position += pushback;
                    Console.WriteLine("COLLIDED");
                }

            }

            if (facingInput == Vector2.Zero)
                Player.HandleAnimationState(movementInput);
            else
                Player.HandleAnimationState(facingInput);

            Player.Update(dt);
        }
    }
}