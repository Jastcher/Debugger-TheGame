using System.Numerics;
using Debugger.Core.Components;

namespace Debugger.Core.Entities;

public class Barrel : Entity
{
    public Barrel()
    {
        
        TextureKey = "tileSheet";
        StaticFrameIndex = 5;

        Hitbox = new CircleHitbox(20.0f, new(16,16));
    }
}