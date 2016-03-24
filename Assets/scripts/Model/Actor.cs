using UnityEngine;
using System.Collections;
using System;

public class Actor {
    public Tile currentTile { get; internal set; }
    public string name { get; internal set; }

    Action<Actor> callbackMoved;
    private float speed = 5f;

    public Actor(String name, Tile tile)
    {
        this.name = name;
        this.currentTile = tile;
    }

    public void move(Tile tile)
    {
        //get path

        //for loop through path? would block other behavior?


        this.currentTile = tile;
        callbackMoved(this);
    }

    public void registerCallback(Action<Actor> callback)
    {
        callbackMoved += callback;
    }

    internal void update(float deltaTime)
    {
        //take care of incremental movement

        float distanceThisFrame = deltaTime * speed;
    }
}
