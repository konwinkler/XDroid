using UnityEngine;
using System.Collections;
using System;

public class GameState
{
    private World world;
    Action<Actor> nextActor;
    public Actor currentActor { get; internal set; }
    public MovementMode movementMode { get; internal set; }
	public GameMode currentMode { get; internal set; }
    private Action<Actor> gameEnds;
	private AI ai;

    public GameState(World world, Actor firstActor)
    {
        this.world = world;
        currentActor = firstActor;
        movementMode = new MovementMode(world);
        movementMode.registerFinishedAction(nextTurn);
        currentMode = movementMode;
		ai = new AI (world);

        //setup movement for first actor
        movementMode.movementRange.newMovementRange(currentActor);
    }

    public void nextTurn()
    {
		if (currentActor.team == 1 ) {
			foreach (Actor other in world.actors) {
				if (other.team == 0 && currentActor.currentTile == other.currentTile) {
					gameEnds (currentActor);
				}
			}
		}


		if (currentActor.team == 0 && currentActor.currentTile == world.goal) {
			Debug.Log("new world");
			world.destroy ();
			world = new World (true);
		}

//
        //next turn
        findNextActor(currentActor);
        currentMode = movementMode;
        movementMode.movementRange.newMovementRange(currentActor);


		if (currentActor.team == 1) {
			//AI has to move it
			ai.act(currentActor, this);
		}
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

    public void setModeToMovement()
    {
        currentMode.end();
        currentMode = movementMode;
        currentMode.start();
    }

	public void destroy ()
	{
		movementMode.unregisterFinishedAction(nextTurn);	
		movementMode.destroy ();
	}
}
