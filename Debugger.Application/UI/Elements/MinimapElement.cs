using Debugger.Application.Systems;
using Debugger.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Graphics;
using MLEM.Ui;

namespace Debugger.Application.UI.Elements;

public class MinimapElement : MLEM.Ui.Elements.Element
{
    private RoomManager _roomManager;
    public MinimapElement(Anchor anchor, XnaVector2 size, RoomManager roomManager) : base(anchor, size)
    {
        _roomManager = roomManager;
    }

    public override void Draw(GameTime time, SpriteBatch batch, float alpha, SpriteBatchContext context)
    {
        var area = this.DisplayArea;

        float roomWidth = area.Width / _roomManager.Size;
        float roomHeight = area.Height / _roomManager.Size;

        float spacing = 2f;

        for (int y = 0; y < _roomManager.Size; y++)
        {
            for (int x = 0; x < _roomManager.Size; x++)
            {
                if (_roomManager.GetRoom(x, y) == null) continue;

                float screenX = area.X + (x * roomWidth);
                float screenY = area.Y + (y * roomHeight);

                Color roomColor = (x == _roomManager.CurrentPosX && y == _roomManager.CurrentPosY)
                    ? Color.Red
                    : Color.Blue;

                batch.Draw(
                    AssetManager.WhitePixel,
                    new Vector2(screenX, screenY),
                    null,
                    roomColor * alpha, 
                    0f,
                    Vector2.Zero,
                    new Vector2(roomWidth - spacing, roomHeight - spacing), 
                    SpriteEffects.None,
                    0f
                );
            }
        }

        base.Draw(time, batch, alpha, context);
    }
}