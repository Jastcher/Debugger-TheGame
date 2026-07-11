using Debugger.Core.Components;
using LDtk;
using LDtk.Renderer;
using Microsoft.Xna.Framework.Graphics;

namespace Debugger.Application.Systems;

public class WorldManager
{
    private readonly SpriteBatch _spriteBatch;
    private ExampleRenderer _tileRenderer;
    private LDtkWorld _world;

    public WorldManager(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
    }

    public void Initialize(string ldtkPath)
    {
        LDtkFile file = LDtkFile.FromFile(ldtkPath);
        _world = file.LoadWorld(file.Worlds[0].Iid);
    }

    public void LoadContent()
    {
        _tileRenderer = new ExampleRenderer(_spriteBatch);

        foreach (var level in _world.Levels)
        {
            _tileRenderer.PrerenderLevel(level);
        }
    }

    public void RenderRoom(int roomIndex)
    {
        _tileRenderer.RenderPrerenderedLevel(_world.Levels[roomIndex]);
    }

    public CollisionGrid GetCollisionGridForRoom(int roomIndex)
    {
        LDtkLevel level = _world.Levels[roomIndex];

        LDtkIntGrid collisions = level.GetIntGrid("Collisions");

        var coreGrid = new CollisionGrid(collisions.GridSize.X, collisions.GridSize.Y, collisions.TileSize);

        for (int x = 0; x < collisions.GridSize.X; x++)
        {
            for (int y = 0; y < collisions.GridSize.Y; y++)
            {
                long intGridValue = collisions.GetValueAt(x, y);
                if (intGridValue == 1)
                {
                    coreGrid.SetSolid(x, y);
                }
            }
        }

        return coreGrid;
    }

}