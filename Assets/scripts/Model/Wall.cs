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

}