using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorController : MonoBehaviour {
    public Sprite ActorSprite;
    Dictionary<Actor, GameObject> gameObjectCache;

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start () {
        gameObjectCache = new Dictionary<Actor, GameObject>();
        foreach (Actor actor in world.actors)
        {
            GameObject actorGameObject = new GameObject();


            actorGameObject.name = "actor_" + actor.name;
            actorGameObject.transform.position = new Vector3(actor.currentTile.x, actor.currentTile.y, 0);
            actorGameObject.transform.SetParent(this.transform, true);

            SpriteRenderer sr = actorGameObject.AddComponent<SpriteRenderer>();
            sr.sprite = ActorSprite;
            sr.sortingLayerName = "Walls";

            actor.registerCallback(moveActor);
            gameObjectCache.Add(actor, actorGameObject);
        }
	}

    public void moveActor(Actor actor)
    {
        Debug.Log("move actor: " + actor.name);
        var actorGameObject = gameObjectCache[actor];

        actorGameObject.transform.position = new Vector3(actor.x, actor.y, 0);
    }


	
	// Update is called once per frame
	void Update () {
	
	}
}
