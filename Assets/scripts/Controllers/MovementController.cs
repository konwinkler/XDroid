using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
        // FIXME: this needs to be called from the game mode
        world.gameState.movementMode.movementRange.newMovementCallback(drawMovementRange);
        world.gameState.movementMode.movementRange.clearMovementCallback(destroyAll);
        drawMovementRange(world.gameState.movementMode.movementRange);
    }

    public void drawMovementRange(MovementRange movementRange)
    {
        Debug.Log("Draw movement.");

        //delete old ones
        destroyAll(movementRange);

        //draw new ones
        foreach (Tile tile in movementRange.validMovementTiles)
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

    private void destroyAll(MovementRange movementRange)
    {
        foreach (GameObject gameObject in drawnMovement)
        {
            Destroy(gameObject);
        }
    }
}
