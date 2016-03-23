using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {
    public Sprite ActorSprite;

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start () {
        foreach (Actor actor in world.actors)
        {
            GameObject actorGameObject = new GameObject();


            actorGameObject.name = "actor_" + actor.name;
            actorGameObject.transform.position = new Vector3(actor.x, actor.y, 0);
            actorGameObject.transform.SetParent(this.transform, true);

            SpriteRenderer sr = actorGameObject.AddComponent<SpriteRenderer>();
            sr.sprite = ActorSprite;
            sr.sortingLayerName = "Walls";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
