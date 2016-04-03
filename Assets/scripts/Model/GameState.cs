using UnityEngine;
using System.Collections;
using System;

public class GameState
{
    private World world;
    Action<Actor> nextActor;
    public Actor currentActor { get; internal set; }
    public MovementMode movementMode { get; internal set; }
    public ShootingMode shootingMode { get; internal set; }
    GameMode currentMode;


    public GameState(World world, Actor firstActor)
    {
        this.world = world;
        currentActor = firstActor;
        foreach (Actor actor in world.actors)
        {
            actor.finishedMovingCallback(findNextActor);
        }
        movementMode = new MovementMode(world);
        shootingMode = new ShootingMode(world);
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

        Debug.Log("new current actor is " + currentActor.name);

        //callbacks
        if (nextActor != null)
        {
            nextActor(currentActor);
        }
    }

    public void updateMousePosition(Tile tile)
    {
        currentMode.updateMousePosition(tile);
    }

    public void nextActorCallback(Action<Actor> callback)
    {
        nextActor += callback;
    }

    public void setModeToShooting()
    {
        currentMode.end();
        currentMode = shootingMode;
        currentMode.start();

    }

    public void setModeToMovement()
    {
        currentMode.end();
        currentMode = movementMode;
        currentMode.start();
    }
}
