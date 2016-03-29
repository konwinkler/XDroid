using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementController : MonoBehaviour
{
    public Sprite movementSprite;
    List<GameObject> drawnMovement = new List<GameObject>();

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start()
    {
        foreach (Actor actor in world.actors)
        {
            actor.finishedMovingCallback(drawMovementRange);
            drawMovementRange(actor);
        }
    }

    public void drawMovementRange(Actor actor)
    {
        Debug.Log("Draw movement.");

        //delete old ones
        foreach (GameObject gameObject in drawnMovement)
        {
            Destroy(gameObject);
        }

        //draw new ones
        var finder = new Pathfinder(world);
        List<Tile> reachableTiles = finder.calculaterange(actor.currentTile, actor.movementRange);
        foreach (Tile tile in reachableTiles)
        {
            GameObject movementGameObject = new GameObject();

            movementGameObject.name = "Movement_" + tile.x + "_" + tile.y;
            movementGameObject.transform.position = new Vector3(tile.x, tile.y, 0);
            movementGameObject.transform.SetParent(this.transform, true);

            SpriteRenderer sr = movementGameObject.AddComponent<SpriteRenderer>();
            sr.sprite = movementSprite;
            sr.sortingLayerName = "Movement";

            drawnMovement.Add(movementGameObject);
        }

    }
}
