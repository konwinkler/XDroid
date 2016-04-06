using UnityEngine;
using System.Collections;
using System;

public class ShootingMode : GameMode
{
    private Action notifyStart;
    private Action notifyEnd;
    private World world;
    public Tile currentPosition { get; internal set; }
    private Action<ShootingMode> notifyNewPosition;
    private Action finishedAction;

    public Boolean inRange { get; internal set; }

    public ShootingMode(World world)
    {
        this.world = world;
    }

    public void click(Tile tile)
    {
        if(inRange)
        {
            foreach(Actor actor in world.actors)
            {
                if(actor.currentTile == tile)
                {
                    actor.kill();
                    notifyEnd();
                    finishedAction();
                }
            }
        }
    }

    public void start()
    {
        if (notifyStart != null)
        {
            notifyStart();
            inRange = false;
            currentPosition = null;
        }
    }

    public void updateMousePosition(Tile tile)
    {
        if (currentPosition == null || currentPosition != tile)
        {
            currentPosition = tile;
            inRange = calculateInRange(world.gameState.currentActor, currentPosition);
            if (notifyNewPosition != null)
            {
                notifyNewPosition(this);
            }
        }

    }

    private bool calculateInRange(Actor actor, Tile target)
    {
        Tile origin = actor.currentTile;

        if(Vector2.Distance(origin.getPostition(), target.getPostition()) > actor.maxShootingRange)
        {
            //target is too far away
            return false;
        } 

        //check if target is blocked by a wall
        foreach (Wall wall in world.walls)
        {
            if(wall.Type == Wall.WallType.Half)
            {
                //half walls do not block shots
                continue;
            }
            if (intersect(origin.x, origin.y, target.x, target.y, wall.getStartPoint().x, wall.getStartPoint().y, wall.getEndPoint().x, wall.getEndPoint().y))
            {
                return false;
            }
        }
        return true;
    }

    private Boolean intersect(float p0_x, float p0_y, float p1_x, float p1_y,
        float p2_x, float p2_y, float p3_x, float p3_y)
    {
        Vector3 start1 = new Vector3(p0_x, p0_y, -9);
        Vector3 end1 = new Vector3(p1_x, p1_y, -9);
        Debug.DrawLine(start1, end1, Color.cyan, 5f);

        Vector3 start2 = new Vector3(p2_x, p2_y, -9);
        Vector3 end2 = new Vector3(p3_x, p3_y, -9);
        Debug.DrawLine(start2, end2, Color.red, 5f);

        float s1_x = p1_x - p0_x;
        float s1_y = p1_y - p0_y;
        float s2_x = p3_x - p2_x;
        float s2_y = p3_y - p2_y;

        float s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
        float t = (s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            // Collision detected
            return true;
        }

        return false; // No collision
    }

    public void newPositionCallback(Action<ShootingMode> callback)
    {
        notifyNewPosition += callback;
    }

    public void startCallback(Action callback)
    {
        notifyStart += callback;
    }

    public void endCallback(Action callback)
    {
        notifyEnd += callback;
    }

    public void end()
    {
        if (notifyEnd != null)
        {
            notifyEnd();
        }
    }

    public void registerFinishedAction(Action callback)
    {
        finishedAction += callback;
    }
}
