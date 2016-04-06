using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorController : MonoBehaviour
{
    public Sprite ActorSpriteTeam1;
    public Sprite ActorSpriteTeam2;
    Dictionary<Actor, GameObject> gameObjectCache;

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start()
    {
        gameObjectCache = new Dictionary<Actor, GameObject>();
        foreach (Actor actor in world.actors)
        {
            GameObject actorGameObject = new GameObject();

            actorGameObject.name = "actor_" + actor.name;
            actorGameObject.transform.position = new Vector3(actor.currentTile.x, actor.currentTile.y, 0);
            actorGameObject.transform.SetParent(this.transform, true);

            SpriteRenderer sr = actorGameObject.AddComponent<SpriteRenderer>();
            switch (actor.team)
            {
                case 0:
                    sr.sprite = ActorSpriteTeam1;
                    break;
                case 1:
                    sr.sprite = ActorSpriteTeam2;
                    break;
                default:
                    throw new System.Exception("no sprite defined for an actor of team " + actor.team);
            }
            sr.sortingLayerName = "Actor";

            actor.movingCallback(moveActor);
            gameObjectCache.Add(actor, actorGameObject);
        }
    }

    public void moveActor(Actor actor)
    {
        var actorGameObject = gameObjectCache[actor];

        actorGameObject.transform.position = new Vector3(actor.x, actor.y, 0);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
