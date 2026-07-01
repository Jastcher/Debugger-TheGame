using System;
using System.Collections.Generic;
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
    }
}