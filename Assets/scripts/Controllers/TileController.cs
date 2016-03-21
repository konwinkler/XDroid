using UnityEngine;
using System.Collections;


using System;
using System.Linq;
using System.Collections.Generic;

public class TileController : MonoBehaviour {

    public Sprite FloorSprite;

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start () {
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                GameObject tileGameObject = new GameObject();


                tileGameObject.name = "Tile_" + x + "_" + y;
                tileGameObject.transform.position = new Vector3(x, y, 0);

                SpriteRenderer sr = tileGameObject.AddComponent<SpriteRenderer>();
                sr.sprite = FloorSprite;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
