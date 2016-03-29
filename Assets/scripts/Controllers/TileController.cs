using UnityEngine;
using System.Collections;


using System;
using System.Linq;
using System.Collections.Generic;

public class TileController : MonoBehaviour
{

    public Sprite FloorSprite;

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start()
    {
        foreach (Tile tile in world.tiles)
        {
            GameObject tileGameObject = new GameObject();

            tileGameObject.name = "Tile_" + tile.x + "_" + tile.y;
            tileGameObject.transform.position = new Vector3(tile.x, tile.y, 0);
            tileGameObject.transform.SetParent(this.transform, true);

            SpriteRenderer sr = tileGameObject.AddComponent<SpriteRenderer>();
            switch (tile.type)
            {
                case Tile.TileType.Floor:
                    sr.sprite = FloorSprite;
                    break;
                default:
                    throw new Exception("Tile type not recognized by tile controller.");
            }
            sr.sortingLayerName = "Tiles";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
