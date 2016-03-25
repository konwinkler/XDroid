using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private World world;

    public Pathfinder(World world)
    {
        this.world = world;
    }

    public Stack<Tile> calculatePath(Tile start, Tile end)
    {
        Debug.Log("Try to find path from " + start + " to " + end);
        Stack<Tile> path = new Stack<Tile>();
        if(start == end)
        {
            return path;
        }

        var history = new Dictionary<Tile, Tile>();
        var visited = new List<Tile>();
        var toVisit = new Queue<Tile>();
        toVisit.Enqueue(start);

        while(toVisit.Count > 0)
        {
            Tile current = toVisit.Dequeue();
            visited.Add(current);
            //Debug.Log("looking at tile: " + current);

            if(current == null)
            {
                throw new Exception("Something went wrong in pathfinding.");
                Debug.Log("Could not find path.");
                return path;
            }

            if(current == end)
            {
                reconstruct(history, end, path, start);
                Debug.Log("finished pathfinding, path is "+path.Count+" tiles long.");
                return path;
            }


            List<Tile> neighbours = world.getNeighbours(current);
            foreach(var neighbour in neighbours)
            {
                if (!visited.Contains(neighbour) && !toVisit.Contains(neighbour))
                {
                    //Debug.Log("Add to history " + neighbour + "->" + current);
                    history.Add(neighbour, current);
                    toVisit.Enqueue(neighbour);
                }
            }
        }
        Debug.Log("Could not find valid path.");
        return path;
    }

    private void reconstruct(Dictionary<Tile, Tile> history, Tile currentTile, Stack<Tile> path, Tile start)
    {
        //Debug.Log("reconstruct path for " + currentTile);
        if (!history.ContainsKey(currentTile))
        {
            throw new Exception("Pathfinding Error - tile of reconstruction could not be found in history.");
        }
        var tileBefore = history[currentTile];
        path.Push(currentTile);
        if (tileBefore == start)
        {
            return;
        }
        else
        {
            reconstruct(history, tileBefore, path, start);
        }
    }
}