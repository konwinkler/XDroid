using UnityEngine;
using System.Collections;
using System;

public class ShootingMode : GameMode
{
    private Action notifyStart;
    private Action notifyEnd;
    private World world;
    public Tile currentPosition { get; internal set; }
    private Action<ShootingMode> notifyNewPosition;

    public ShootingMode(World world)
    {
        this.world = world;
    }

    public void click(Tile tile)
    {
        //TODO:
    }

    public void start()
    {
        if (notifyStart != null)
        {
            notifyStart();
        }
    }

    public void updateMousePosition(Tile tile)
    {
        if (currentPosition == null || currentPosition != tile)
        {
            currentPosition = tile;
            if (notifyNewPosition != null)
            {
                notifyNewPosition(this);
            }
        }

    }

    public void newPositionCallback(Action<ShootingMode> callback)
    {
        notifyNewPosition += callback;
    }

    public void startCallback(Action callback)
    {
        notifyStart += callback;
    }

    public void endCallback(Action callback)
    {
        notifyEnd += callback;
    }

    public void end()
    {
        if(notifyEnd != null)
        {
            notifyEnd();
        }
    }
}
