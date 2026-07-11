using System;
using System.Collections.Generic;
using Debugger.Core.Components;
using Debugger.Core.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Debugger.Application.Systems
{
    public class EntityRenderer
    {

        public void Draw(SpriteBatch spriteBatch, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var texture = AssetManager.Get(entity.TextureKey);
                if (texture != null)
                {
                    spriteBatch.Draw(texture, (Vector2)entity.Position, Color.White);
                }
            }
        }

        public void DrawHitbox(SpriteBatch spriteBatch, CircleHitbox hitbox, Vector2 position)
        {
            
            DrawCircleOutline(spriteBatch, AssetManager.WhitePixel, position + hitbox.Offset, hitbox.Radius, 16, Color.Red);

        }

        private void DrawCircleOutline(SpriteBatch spriteBatch, Texture2D pixelTexture, Vector2 center, float radius, int segments, Color color, int thickness = 1)
        {
            Vector2[] points = new Vector2[segments + 1];

            // Calculate positions along the edge
            for (int i = 0; i <= segments; i++)
            {
                float theta = (i / (float)segments) * MathHelper.TwoPi;
                points[i] = center + new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
            }

            // Draw line segments connecting the points
            for (int i = 0; i < segments; i++)
            {
                Vector2 start = points[i];
                Vector2 end = points[i + 1];

                Vector2 edge = end - start;
                float angle = (float)Math.Atan2(edge.Y, edge.X);

                spriteBatch.Draw(
                    pixelTexture,
                    start,
                    null,
                    color,
                    angle,
                    Vector2.Zero,
                    new Vector2(edge.Length(), thickness), // Scale the 1x1 pixel texture into a line
                    SpriteEffects.None,
                    0
                );
            }
        }
    }
}