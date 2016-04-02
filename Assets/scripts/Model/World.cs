using System;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public int Height { get; internal set; }
    public int Width { get; internal set; }
    public Tile[,] tiles { get; internal set; }
    public List<Actor> actors { get; internal set; }
    public LinkedList<Wall> walls { get; internal set; }
    public GameState gameState { get; internal set; }

    public World(int width = 5, int height = 10)
    {
        this.Width = width;
        this.Height = height;

        createTiles();
        createWalls();
        createActors();
        gameState = new GameState(this, actors[0]);
    }

    private void createActors()
    {
        actors = new List<Actor>();
        actors.Add(new Actor("Player1", getTileAt(2, 0), this, 2));
        actors.Add(new Actor("Player2", getTileAt(2, 9), this, 2));
    }

    public List<Tile> getNeighbours(Tile root)
    {
        var neighbours = new List<Tile>();

        if (root.x > 0 && !blocked(root, Wall.Direction.Left))
        {
            neighbours.Add(getTileAt(root.x - 1, root.y));
        }
        if (root.x < (Width - 1) && !blocked(root, Wall.Direction.Right))
        {
            neighbours.Add(getTileAt(root.x + 1, root.y));
        }
        if (root.y > 0 && !blocked(root, Wall.Direction.Down))
        {
            neighbours.Add(getTileAt(root.x, root.y - 1));
        }
        if (root.y < (Height - 1) && !blocked(root, Wall.Direction.Up))
        {
            neighbours.Add(getTileAt(root.x, root.y + 1));
        }

        return neighbours;
    }

    private bool blocked(Tile root, Wall.Direction direction)
    {
        foreach (var wall in walls)
        {
            if (wall.blocks(root, direction))
            {
                return true;
            }
        }
        return false;
    }

    private void createWalls()
    {
        walls = new LinkedList<Wall>();
        //lower half
        walls.AddFirst(new Wall(1, 1, Wall.Direction.Up, Wall.WallType.Half));
        walls.AddFirst(new Wall(0, 2, Wall.Direction.Right, Wall.WallType.Full));
        walls.AddFirst(new Wall(0, 3, Wall.Direction.Right, Wall.WallType.Half));

        walls.AddFirst(new Wall(4, 1, Wall.Direction.Up, Wall.WallType.Half));

        walls.AddFirst(new Wall(2, 3, Wall.Direction.Up, Wall.WallType.Half));
        walls.AddFirst(new Wall(2, 3, Wall.Direction.Down, Wall.WallType.Half));
        walls.AddFirst(new Wall(2, 3, Wall.Direction.Left, Wall.WallType.Half));
        walls.AddFirst(new Wall(2, 3, Wall.Direction.Right, Wall.WallType.Half));

        walls.AddFirst(new Wall(3, 3, Wall.Direction.Right, Wall.WallType.Full));
        walls.AddFirst(new Wall(3, 4, Wall.Direction.Right, Wall.WallType.Half));
        walls.AddFirst(new Wall(3, 4, Wall.Direction.Up, Wall.WallType.Full));

        //upper half
        walls.AddFirst(new Wall(3, 8, Wall.Direction.Down, Wall.WallType.Half));
        walls.AddFirst(new Wall(4, 7, Wall.Direction.Left, Wall.WallType.Full));
        walls.AddFirst(new Wall(4, 6, Wall.Direction.Left, Wall.WallType.Half));

        walls.AddFirst(new Wall(0, 8, Wall.Direction.Down, Wall.WallType.Half));

        walls.AddFirst(new Wall(2, 6, Wall.Direction.Down, Wall.WallType.Half));
        walls.AddFirst(new Wall(2, 6, Wall.Direction.Up, Wall.WallType.Half));
        walls.AddFirst(new Wall(2, 6, Wall.Direction.Right, Wall.WallType.Half));
        walls.AddFirst(new Wall(2, 6, Wall.Direction.Left, Wall.WallType.Half));

        walls.AddFirst(new Wall(1, 6, Wall.Direction.Left, Wall.WallType.Full));
        walls.AddFirst(new Wall(1, 5, Wall.Direction.Left, Wall.WallType.Half));
        walls.AddFirst(new Wall(1, 5, Wall.Direction.Down, Wall.WallType.Full));
    }

    internal void update(float deltaTime)
    {
        foreach (Actor actor in actors)
        {
            actor.update(deltaTime);
        }
    }

    private void createTiles()
    {
        tiles = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                tiles[x, y] = new Tile(x, y);
            }
        }
    }

    public Tile getTileAt(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return null;
        }

        if (x >= Width || y >= Height)
        {
            return null;
        }

        return tiles[x, y];
    }
}
