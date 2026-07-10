using System.Dynamic;
using System.Numerics;

namespace Debugger.Core.Components;

public abstract class Hitbox
{
    public Vector2 Offset {get; set;} = Vector2.Zero;
}

public class CircleHitbox : Hitbox
{
    public float Radius {get;set;}
    public CircleHitbox(float radius, Vector2 offset = default)
    {
        Radius = radius;
        Offset = offset;
        
    }
}

public class SquareHitbox : Hitbox
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public SquareHitbox(float width, float height, Vector2 offset = default)
        {
            Width = width;
            Height = height;
            Offset = offset;
        }
    }