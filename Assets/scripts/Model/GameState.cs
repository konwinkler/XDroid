using UnityEngine;
using System.Collections;
using System;

public class GameState {
    private World world;
    Action<Actor> nextActor;

    public GameState(World world)
    {
        this.world = world;
        world.currentActor.finishedMovingCallback(findNextActor);
    }

    public void findNextActor(Actor actor)
    {
        world.currentActor = world.actors[0];
        if(nextActor != null)
        {
            nextActor(world.currentActor);
        }
    }

    public void nextActorCallback(Action<Actor> callback)
    {
        nextActor = callback;
    }
}
