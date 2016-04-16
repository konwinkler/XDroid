using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallController : MonoBehaviour {

    public Sprite fullSprite;
    public Sprite halfSprite;
	Dictionary<Wall, GameObject> gameObjectCache = new Dictionary<Wall, GameObject>();

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start()
    {
        foreach(Wall wall in world.walls)
        { 
            GameObject wallGameObject = new GameObject();

            wallGameObject.name = "Wall_" + wall.x + "_" + wall.y + "_" + wall.direction;

            float x = wall.x;
            float y = wall.y;
            switch (wall.direction)
            {
                case Wall.Direction.Up:
                    y += 0.5f;
                    break;
                case Wall.Direction.Right:
                    x += 0.5f;
                    wallGameObject.transform.Rotate(new Vector3(0,0,90));
                    break;
                case Wall.Direction.Down:
                    y -= 0.5f;
                    break;
                case Wall.Direction.Left:
                    x -= 0.5f;
                    wallGameObject.transform.Rotate(new Vector3(0, 0, 90));
                    break;
            }
            wallGameObject.transform.position = new Vector3(x, y, 0);
            wallGameObject.transform.SetParent(this.transform, true);

            SpriteRenderer sr = wallGameObject.AddComponent<SpriteRenderer>();

            switch (wall.Type)
            {
                case Wall.WallType.Full:
                    sr.sprite = fullSprite;
                    break;
                case Wall.WallType.Half:
                    sr.sprite = halfSprite;
                    break;
            }
            sr.sortingLayerName = "Walls";

			gameObjectCache.Add (wall, wallGameObject);
			wall.registerDestroy (destroy);
        }
    }

	void destroy (Wall wall)
	{
		GameObject go = gameObjectCache [wall];
		Destroy (go);
		gameObjectCache.Remove (wall);
	}

    // Update is called once per frame
    void Update () {
	
	}
}