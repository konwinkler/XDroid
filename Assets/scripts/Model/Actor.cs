using UnityEngine;
using System.Collections;
using System;

public class Actor {
    public Tile currentTile { get; internal set; }
    public string name { get; internal set; }
    public float x {
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

    Action<Actor> callbackMoved;
    private float speed = 5f;
    private float movementPercentage = 0;
    private Tile nextTile;

    public Actor(String name, Tile tile)
    {
        this.name = name;
        this.currentTile = tile;
        this.nextTile = tile;
    }

    public void move(Tile tile)
    {
        //get path

        //for loop through path? would block other behavior?

        nextTile = tile;
    }

    public void registerCallback(Action<Actor> callback)
    {
        callbackMoved += callback;
    }

    internal void update(float deltaTime)
    {
        if(currentTile == nextTile)
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

        if(movementPercentage >= 1)
        {
            movementPercentage = 0f;
            currentTile = nextTile;
        }

        callbackMoved(this);
    }
}
