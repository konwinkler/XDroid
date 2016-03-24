using System;
using System.Collections.Generic;

public class World {
    public int Height { get; private set; }
    public int Width { get; private set; }
    private Tile[,] tiles;
    public List<Actor> actors { get; private set; }
    public LinkedList<Wall> walls { get; private set; }

    public World(int width = 5, int height = 10)
    {
        this.Width = width;
        this.Height = height;

        createTiles();
        createWalls();
        createActors();

    }

    private void createActors()
    {
        actors = new List<Actor>();
        actors.Add(new Actor("Player1", 2, 5));
    }

    private void createWalls()
    {
        walls = new LinkedList<Wall>();
        walls.AddFirst(new Wall(0, 0, Wall.Direction.Up, Wall.WallType.Full));
        walls.AddFirst(new Wall(1, 1, Wall.Direction.Down, Wall.WallType.Full));
        walls.AddFirst(new Wall(1, 4, Wall.Direction.Down, Wall.WallType.Half));
        walls.AddFirst(new Wall(1, 4, Wall.Direction.Right, Wall.WallType.Half));
        walls.AddFirst(new Wall(1, 4, Wall.Direction.Left, Wall.WallType.Half));
    }

    private void createTiles()
    {
        tiles = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                tiles[x, y] = new Tile();
            }
        }
    }

    public Tile getTileAt(int x, int y)
    {
        return tiles[x, y];
    }

    public Actor getCurrentActor()
    {
        return actors[0];
    }
}
