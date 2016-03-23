using UnityEngine;
using System.Collections;
using System;

public class Actor {
    public int x { get; internal set; }
    public int y { get; internal set; }
    public string name { get; internal set; }

    public Actor(String name, int x, int y)
    {
        this.name = name;
        this.x = x;
        this.y = y;
    }
}
