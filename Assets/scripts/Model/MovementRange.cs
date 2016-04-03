using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovementRange
{
    private World world;
    internal Action<MovementRange> notifyNewMovement;
    internal Action<MovementRange> notifyClearMovement;

    public List<Tile> validMovementTiles { get; internal set; }

    public MovementRange(World world)
    {
        this.world = world;
    }

    public void newMovementRange(Actor actor)
    {
        var finder = new Pathfinder(world);
        validMovementTiles = finder.calculaterange(actor.currentTile, actor.movementRange);
        if (notifyNewMovement != null)
        {
            notifyNewMovement(this);
        }
    }

    public void newMovementCallback(Action<MovementRange> callback)
    {
        notifyNewMovement += callback;
    }

    public void clearMovementCallback(Action<MovementRange> callback)
    {
        notifyClearMovement += callback;
    }

    public void clear()
    {
        validMovementTiles.Clear();
        notifyClearMovement(this);
    }

}
