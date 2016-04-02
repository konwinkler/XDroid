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
        // FIXME: this needs to be called from the game mode
        world.gameState.movementMode.movementRange.newMovementCallback(drawMovementRange);
        drawMovementRange(world.gameState.movementMode.movementRange);
    }

    public void drawMovementRange(MovementRange movementRange)
    {
        Debug.Log("Draw movement.");

        //delete old ones
        foreach (GameObject gameObject in drawnMovement)
        {
            Destroy(gameObject);
        }

        //draw new ones
        foreach (Tile tile in movementRange.validMovement)
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
