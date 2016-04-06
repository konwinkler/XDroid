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
    private Action<Actor> gameEnds;

    public GameState(World world, Actor firstActor)
    {
        this.world = world;
        currentActor = firstActor;
        movementMode = new MovementMode(world);
        movementMode.registerFinishedAction(nextTurn);
        shootingMode = new ShootingMode(world);
        shootingMode.registerFinishedAction(nextTurn);
        currentMode = movementMode;

        //setup movement for first actor
        movementMode.movementRange.newMovementRange(currentActor);
    }

    private void nextTurn()
    {
        //find out if more than one actor is alive
        int countAlive = 0;
        Actor lastAlive = null;
        foreach(Actor actor in world.actors)
        {
            if(actor.alive)
            {
                lastAlive = actor;
                countAlive++;
            }
        }
        if(countAlive == 1)
        {
            //game ends
            if (gameEnds != null)
            {
                gameEnds(lastAlive);
            }
            return;
        }

        //next turn
        findNextActor(currentActor);
        currentMode = movementMode;
        movementMode.movementRange.newMovementRange(currentActor);
    }

    public void registerGameEnds(Action<Actor> callback)
    {
        gameEnds += callback;
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
