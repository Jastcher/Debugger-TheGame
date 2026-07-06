using System.Numerics;
using Debugger.Core.Entities;

namespace Debugger.Core.Systems
{
    public class GameplaySimulation
    {
        public Player Player {get;set;}
        
        public List<Entity> Entities {get;set;}
        public GameplaySimulation()
        {
            Player = new(new Vector2(50,50));
            
            Entities = new();
            Entities.Add(Player);
        }
        
        public void Update(Vector2 movementInput, float dt)
        {
            Player.Move(movementInput, dt);
        }
    }
}