
using System.Numerics;
using Debugger.Core.Components;

namespace Debugger.Core.Entities
{
    public class Player : Entity
    {
        public Player(Vector2 startPosition) : base(startPosition)
        {
            Hitbox = new CircleHitbox(10.0f);
            TextureKey = "player";
            Width = 64;
            Height = 64;
        }
    }
}