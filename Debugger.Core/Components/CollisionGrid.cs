namespace Debugger.Core.Components;

public class CollisionGrid
{
    private readonly int[,] _intGrid;


    public int Width => _intGrid.GetLength(0);
    public int Height => _intGrid.GetLength(1);
    
    public int TileSize {get;set;}

    public CollisionGrid(int width, int height, int tileSize)
    {
        _intGrid = new int[width, height];
        
        TileSize = tileSize;
    }

    public void SetSolid(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return;

        _intGrid[x, y] = 1;
    }

    public bool IsSolid(int x, int y)
    {

        if (x < 0 || x >= Width || y < 0 || y >= Height) return true;

        return _intGrid[x, y] == 1;
    }

    public void Print()
    {
        for (int i = 0; i < Width; i++)
        {

            for (int j = 0; j < Height; j++)
            {

                Console.Write($"{_intGrid[j,i]}");
            }
            Console.WriteLine();
        }
    }

}