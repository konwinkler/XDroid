using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovementRange {
    private World world;
    Action<MovementRange> newMovement;

    public List<Tile> validMovement { get; internal set; }

    /// <summary>
    /// depends on world.gamestate
    /// </summary>
    /// <param name="world"></param>
    public MovementRange(World world)
    {
        this.world = world;
        newMovementRange(world.gameState.currentActor);
        world.gameState.nextActorCallback(newMovementRange);
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
