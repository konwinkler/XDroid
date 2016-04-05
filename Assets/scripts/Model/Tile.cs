using UnityEngine;

public class Tile {

    public enum TileType
    {
        Floor
    }

    public TileType type { get; internal set; }
    public int x { get; internal set; }
    public int y { get; internal set; }

    public Tile(int x, int y)
    {
        type = TileType.Floor;
        this.x = x;
        this.y = y;
    }

    public Vector2 getPostition()
    {
        return new Vector2(x, y);
    }

    public override string ToString()
    {
        return "Tile: (" + x + ", " + y + ") " + type;
    }
}
