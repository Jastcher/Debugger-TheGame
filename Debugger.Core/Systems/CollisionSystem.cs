using System.Numerics;
using System.Runtime.Intrinsics.X86;
using Debugger.Core.Components;

namespace Debugger.Core.Systems;

public static class CollisionSystem
{
    public static bool CheckCircleVsCircle(Vector2 entity1Pos, CircleHitbox? hitbox1, Vector2 entity2Pos, CircleHitbox? hitbox2, out Vector2 pushBack)
    {
        pushBack = Vector2.Zero;

        if (hitbox1 == null || hitbox2 == null) return false;

        Vector2 entity1center = entity1Pos + hitbox1.Offset;
        Vector2 entity2center = entity2Pos + hitbox2.Offset;

        var distX = entity1center.X - entity2center.X;
        var distY = entity1center.Y - entity2center.Y;

        float distanceSquared = (distX * distX) + (distY * distY);

        float R = hitbox1.Radius + hitbox2.Radius;

        if (distanceSquared < (R * R))
        {

            float distance = (float)Math.Sqrt(distanceSquared);

            // fallback if distance is 0
            Vector2 normal = (distance > 0)
                ? new Vector2(distX / distance, distY / distance)
                : new Vector2(0, -1);

            float overlapDepth = R - distance;
            
            pushBack = normal * overlapDepth;
            
            return true;
        }
        return false;
    }
    public static bool CheckCircleVsGrid(Vector2 entityPos, CircleHitbox? hitbox, CollisionGrid grid, out Vector2 pushBack)
    {

        pushBack = Vector2.Zero;
        bool didCollide = false;

        // if no hitbox, nothing to check
        if (hitbox == null) return false;

        Vector2 center = entityPos + hitbox.Offset;
        float radius = hitbox.Radius;

        // Gather tiles around the circle to test
        int startX = (int)((center.X - radius) / grid.TileSize);
        int startY = (int)((center.Y - radius) / grid.TileSize);
        int endX = (int)((center.X + radius) / grid.TileSize);
        int endY = (int)((center.Y + radius) / grid.TileSize);

        //Console.WriteLine($"start {startX}:{startY} \n end {endX}:{endY}\n");
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                if (!grid.IsSolid(x, y)) continue;

                // Find the closest point on this tile to the circle's center
                float tileMinX = x * grid.TileSize;
                float tileMinY = y * grid.TileSize;
                float tileMaxX = tileMinX + grid.TileSize;
                float tileMaxY = tileMinY + grid.TileSize;

                float closestX = Math.Clamp(center.X, tileMinX, tileMaxX);
                float closestY = Math.Clamp(center.Y, tileMinY, tileMaxY);

                // Calculate distance from closest point to circle center
                float distX = center.X - closestX;
                float distY = center.Y - closestY;
                float distanceSquared = (distX * distX) + (distY * distY);


                if (distanceSquared < (radius * radius))
                {
                    didCollide = true;
                    float distance = (float)Math.Sqrt(distanceSquared);

                    // fallback if distance is 0
                    Vector2 normal = (distance > 0)
                        ? new Vector2(distX / distance, distY / distance)
                        : new Vector2(0, -1);

                    float overlapDepth = radius - distance;

                    pushBack += normal * overlapDepth;
                }
            }
        }
        return didCollide;
    }

}