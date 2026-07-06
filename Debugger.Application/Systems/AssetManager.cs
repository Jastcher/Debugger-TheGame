using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Debugger.Application.Systems
{


    public static class AssetManager
    {
        private static readonly Dictionary<string, Texture2D> _textures = new();

        public static void LoadAll(ContentManager content)
        {
            _textures["player"] = content.Load<Texture2D>("Sprites/arrow");
        }

        public static Texture2D Get(string key)
        {
            return _textures.TryGetValue(key, out var tex) ? tex : null;
        }
    }
}