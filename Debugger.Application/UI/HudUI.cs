using Debugger.Application.UI.Elements;
using Debugger.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Maths;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Debugger.Application.UI;

public class HudUI
{

    private Game _game;
    private UiSystem _uiSystem;

    public HudUI(Game game, UiStyle style, RoomManager roomManager)
    {
        _game = game;

        _uiSystem = new UiSystem(_game, style);
        
        Panel hudContainer = new Panel(Anchor.TopLeft, size: Vector2.One, positionOffset: Vector2.Zero)
        {
            Texture = null, 
        };

        _uiSystem.Add("GameHUD", hudContainer);

        var _healthLabel = new Paragraph(Anchor.TopLeft, 1.0f, "HP: 100/100") { TextColor = Color.Red };
        hudContainer.AddChild(_healthLabel);

        var minimap = new MinimapElement(Anchor.TopRight, new Vector2(200, 200), roomManager);
        hudContainer.AddChild(minimap);
    }
    
    public void Update(GameTime gameTime)
    {
        _uiSystem.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        _uiSystem.Draw(gameTime, spriteBatch);
    }
}