
using System.Numerics;
using Debugger.Core.Components;
using Debugger.Core.Systems;

namespace Debugger.Core.Entities
{
    public abstract class Entity
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public float Scale {get;set;} = 1.0f;
        public int Width { get; set; } = 32;
        public int Height { get; set; } = 32;
        
        public Vector2 Origin {get; set;} = Vector2.Zero;

        public float Speed { get; set; } = 100.0f;
        public float Health { get; set; } = 100.0f;

        public Hitbox? Hitbox {get;set;}
        
        public string TextureKey { get; protected set; } = "Empty";
        public AnimationController? Animator {get;set;} 
        public int StaticFrameIndex {get;set;} = 0;

        public Entity()
        {
            //Origin = new Vector2(Width/2, Height/2);
        }

        virtual public void Move(Vector2 direction, float dt)
        {
            if (direction == Vector2.Zero) return;

            direction = Vector2.Normalize(direction);

            Position += direction * Speed * dt;
        }

        virtual public void Update(float dt)
        {
            Animator?.Update(dt);
        }
    }
}