﻿public class Tile {

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
}
