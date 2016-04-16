using UnityEngine;
using System;

public class Tile {
	private Action<Tile> notifyDestroy;

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

	public void destroy ()
	{
		notifyDestroy (this);
		notifyDestroy = null;
	}

	public void registerDestroy(Action<Tile> callback)
	{
		notifyDestroy += callback;
	}

	public void unregisterDestroy(Action<Tile> callback)
	{
		notifyDestroy -= callback;
	}
}
