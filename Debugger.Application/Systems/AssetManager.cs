using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Font;
using MLEM.Ui.Style;

namespace Debugger.Application.Systems
{
    public static class AssetManager
    {
        private static readonly Dictionary<string, Texture2D> _textures = new();

        public static SpriteFont Font { get; private set; }

        public static Texture2D WhitePixel { get; private set; }

        public static UiStyle DefaultUiStyle { get; private set; }
        public static void LoadAll(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _textures["player_walk"] = content.Load<Texture2D>("walk");
            _textures["player_idle"] = content.Load<Texture2D>("idle");

            Font = content.Load<SpriteFont>("Font");

            WhitePixel = new Texture2D(graphicsDevice, 1, 1);
            Color[] colorData = [Color.White];
            WhitePixel.SetData(colorData);

            DefaultUiStyle = new UntexturedStyle(new SpriteBatch(graphicsDevice))
            {
                Font = new GenericSpriteFont(Font),
                //TextColor = Color.Red
            };

        }

        public static Texture2D Get(string key)
        {
            return _textures.TryGetValue(key, out var tex) ? tex : null;
        }
    }
}