using UnityEngine;
using System.Collections;
using System;

public class GameState
{
    private World world;
    Action<Actor> nextActor;
    public Actor currentActor { get; internal set; }
    public MovementMode movementMode { get; internal set; }
    GameMode currentMode;
    ShootingMode shootingMode;


    public GameState(World world, Actor firstActor)
    {
        this.world = world;
        currentActor = firstActor;
        foreach (Actor actor in world.actors)
        {
            actor.finishedMovingCallback(findNextActor);
        }
        movementMode = new MovementMode(world);
        currentMode = movementMode;

        //setup movement
        nextActorCallback(movementMode.movementRange.newMovementRange);
        movementMode.movementRange.newMovementRange(currentActor);
    }


    internal void click(Tile tile)
    {
        currentMode.click(tile);
    }


    public void findNextActor(Actor actor)
    {
        //pick the next from the list of actors
        int index = world.actors.IndexOf(currentActor);
        index++;
        if (index >= world.actors.Count)
        {
            index = 0;
        }
        currentActor = world.actors[index];

        Debug.Log("current actor is " + currentActor.name);

        //callbacks
        if (nextActor != null)
        {
            Debug.Log("call next actor callback.");
            nextActor(currentActor);
        }
    }

    public void nextActorCallback(Action<Actor> callback)
    {
        nextActor += callback;
    }
}
