using System.Numerics;

namespace Debugger.Core.Entities
{
    public class Bullet : Entity
    {
        public Bullet(Vector2 startPosition) : base(startPosition)
        {
            Width = 16;
            Height = 16;
        }

    }
}