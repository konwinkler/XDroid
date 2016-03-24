using UnityEngine;
using System.Collections;
using System;

public class Actor {
    public int x { get; internal set; }
    public int y { get; internal set; }
    public string name { get; internal set; }

    Action<Actor> callbackMoved;

    public Actor(String name, int x, int y)
    {
        this.name = name;
        this.x = x;
        this.y = y;
    }

    public void move(int x, int y)
    {
        this.x = x;
        this.y = y;

        callbackMoved(this);
    }

    public void registerCallback(Action<Actor> callback)
    {
        callbackMoved += callback;
    }
}
