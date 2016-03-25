using System;

public class Wall
{
    public enum WallType
    {
        Half, Full
            //if there is no wall the object is null
    }

    public enum Direction
    {
        Up, Right, Down, Left
    }

    public WallType Type { get; protected set; }
    public Direction direction { get; protected set; }
    public int x { get; protected set; }
    public int y { get; protected set; }

    public Wall(int x, int y, Direction dir, WallType type)
    {
        this.x = x;
        this.y = y;
        this.direction = dir;
        this.Type = type;
    }

    internal bool blocks(Tile root, Direction direction)
    {
        //i want to go the same direction the wall is at
        if (direction == this.direction && root.x == x && root.y == y)
        {
            return true;
        } else {
            switch (direction)
            {
                //I want to go up and the wall points down
                case Direction.Up:
                    if (this.direction == Direction.Down && root.x == this.x && (root.y + 1) == this.y)
                    {
                        return true;
                    }
                    break;
                case Direction.Down:
                    if (this.direction == Direction.Up && root.x == this.x && (root.y - 1) == this.y)
                    {
                        return true;
                    }
                    break;
                case Direction.Left:
                    if (this.direction == Direction.Right && (root.x - 1) == this.x && root.y == this.y)
                    {
                        return true;
                    }
                    break;
                case Direction.Right:
                    if (this.direction == Direction.Left && (root.x + 1) == this.x && root.y == this.y)
                    {
                        return true;
                    }
                    break;
            }
        }
        return false;
    }
}