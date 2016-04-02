using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MovementMode : GameMode
{
    private World world;
    public MovementRange movementRange { get; internal set; }

    public MovementMode(World world)
    {
        this.world = world;
        movementRange = new MovementRange(world);
    }

    public void click(Tile tile)
    {
        if (movementRange.validMovement.Contains(tile))
        {
            world.gameState.currentActor.move(tile);
        }
    }
}
