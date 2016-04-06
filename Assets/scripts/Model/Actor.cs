using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Actor
{
    public Tile currentTile { get; internal set; }
    public string name { get; internal set; }
    public float x
    {
        get
        {
            return Mathf.Lerp(currentTile.x, nextTile.x, movementPercentage);
        }
    }
    public float y
    {
        get
        {
            return Mathf.Lerp(currentTile.y, nextTile.y, movementPercentage);
        }
    }

    public int movementRange { get; internal set; }

    Action<Actor> moving;
    Action<Actor> finishedMoving;

    private float speed = 5f;
    private float movementPercentage = 0;

    internal void unregisterFinishedMoving(Action<Actor> action)
    {
        finishedMoving -= action;
    }

    private Tile nextTile;
    private World world;
    private Stack<Tile> path;
    public int team { get; internal set; }
    public int maxShootingRange { get; internal set; }
    public Boolean alive { get; internal set; }

    public Actor(String name, Tile tile, World world, int movementRange, int team)
    {
        this.name = name;
        this.currentTile = tile;
        this.nextTile = tile;
        this.world = world;
        this.movementRange = movementRange;
        this.team = team;
        this.maxShootingRange = 3;
        this.alive = true;
    }

    public void kill()
    {
        this.alive = false;
    }

    public void move(Tile tile)
    {
        //get path
        Pathfinder finder = new Pathfinder(world);
        this.path = finder.calculatePath(currentTile, tile);

        if (path != null && path.Count > 0)
        {
            nextTile = path.Pop();
        }
    }

    public void movingCallback(Action<Actor> callback)
    {
        moving += callback;
    }

    public void registerFinishedMoving(Action<Actor> callback)
    {
        finishedMoving += callback;
    }

    internal void update(float deltaTime)
    {
        if (currentTile == nextTile)
        {
            return;
        }
        //take care of incremental movement

        float distanceThisFrame = deltaTime * speed;

        float distanceToNextTile = Mathf.Sqrt(
            Mathf.Pow(currentTile.x - nextTile.x, 2) +
            Mathf.Pow(currentTile.y - nextTile.y, 2)
            );

        float percentageThisFrame = distanceThisFrame / distanceToNextTile;

        movementPercentage += percentageThisFrame;

        if (movementPercentage >= 1)
        {
            movementPercentage = 0f;
            if (path != null && path.Count > 0)
            {
                currentTile = nextTile;
                nextTile = path.Pop();
                // Debug.Log("next tile is " + nextTile);
                // Debug.Log("stack size is " + path.Count);
            }
            else
            {
                currentTile = nextTile;
                if (finishedMoving != null)
                {
                    finishedMoving(this);
                }
            }
        }

        if (moving != null)
        {
            moving(this);
        }
    }
}
