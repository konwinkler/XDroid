using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovementRange {
    private World world;
    Action<MovementRange> newMovement;

    public List<Tile> validMovement { get; internal set; }

    public MovementRange(World world)
    {
        this.world = world;
    }

    public void newMovementRange(Actor actor)
    {
        var finder = new Pathfinder(world);
        validMovement = finder.calculaterange(actor.currentTile, actor.movementRange);
        if(newMovement != null)
        {
            newMovement(this);
        }
    }

    public void newMovementCallback(Action<MovementRange> callback)
    {
        newMovement += callback;
    }

}
