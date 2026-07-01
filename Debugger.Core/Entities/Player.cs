
using System.Numerics;

namespace Debugger.Core.Entities
{
    public class Player : Entity
    {
        public Player(Vector2 startPosition) : base(startPosition)
        {
            TextureKey = "player";
            Width = 64;
            Height = 64;
        }
    }
}