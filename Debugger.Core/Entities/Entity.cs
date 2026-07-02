
using System.Numerics;

namespace Debugger.Core.Entities
{
    public abstract class Entity
    {
        public Vector2 Position { get; set; }
        public int Width { get; set; } = 32;
        public int Height { get; set; } = 32;
        public float Speed { get; set; } = 100.0f;
        public float Health { get; set; } = 100.0f;

        public string TextureKey { get; protected set; } = "Empty";

        public Entity(Vector2 startPosition)
        {
            Position = startPosition;
        }

        virtual public void Move(Vector2 direction, float dt)
        {
            if (direction == Vector2.Zero) return;

            direction = Vector2.Normalize(direction);

            Position += direction * Speed * dt;
        }

        virtual public void Update(float dt)
        {
        }
    }
}