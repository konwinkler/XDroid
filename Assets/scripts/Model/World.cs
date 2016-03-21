public class World {
    private int height;
    private Tile[,] tiles;
    private int width;

    public World(int width = 5, int height = 10)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                tiles[x, y] = new Tile();
            }
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Width
    {
        get
        {
            return width;
        }
    }

    public Tile getTileAt(int x, int y)
    {
        return tiles[x, y];
    }
}
