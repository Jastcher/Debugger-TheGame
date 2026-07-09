using System;
using Debugger.Application.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Font;
using MLEM.Maths;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Debugger.Application.Screens
{
    public class PauseScreen : Screen
    {
        UiSystem _uiSystem;

        public PauseScreen(Debugger game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            _uiSystem.Draw(gameTime, _spriteBatch);
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            var menuItems = new (string Text, Action Action)[]
            {
                ("Resume",    () => Game.ScreenManager.PopScreen()),
                ("Options",   () => {}),
                ("Main Menu", () => {}),
                ("Quit Game", () => Game.Exit())
            };


            _uiSystem = new UiSystem(Game, AssetManager.DefaultUiStyle);
            Panel panel = new Panel(Anchor.Center, size: new Vector2(200, 0), positionOffset: Vector2.Zero)
            {
                SetHeightBasedOnChildren = true,
            };
            _uiSystem.Add("PauseMenu", panel);

            Group buttonGroup = new Group(Anchor.Center, size: new Vector2(1, 0))
            {
                SetHeightBasedOnChildren = true
            };
            panel.AddChild(buttonGroup);

            bool isFirst = true;
            foreach (var item in menuItems)
            {
                var button = new Button(Anchor.AutoCenter, size: new Vector2(1, 40), text: item.Text)
                {
                    PositionOffset = isFirst ? Vector2.Zero : new Vector2(0, 5),
                    OnPressed = (element) => item.Action()
                };

                buttonGroup.AddChild(button);
                isFirst = false;
            }
        }

        public override void Update(GameTime gameTime)
        {

            if (InputSystem.IsPressedOnce(Keys.Escape))
            {
                Game.ScreenManager.PopScreen();
            }

            _uiSystem.Update(gameTime);
        }
    }
}