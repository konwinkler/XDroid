using UnityEngine;
using System.Collections;
using System;

public class CrossHairController : MonoBehaviour
{
    GameObject crossHair;
    public Sprite crossHairSpriteGood;
    public Sprite crossHairSpriteBad;

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start()
    {
        world.gameState.shootingMode.startCallback(enableCrossHair);
        world.gameState.shootingMode.endCallback(disableCrossHair);
        world.gameState.shootingMode.newPositionCallback(moveCrossHair);
        createGameObject();
    }

    private void disableCrossHair()
    {
        crossHair.SetActive(false);
    }

    private void moveCrossHair(ShootingMode mode)
    {
        crossHair.transform.position = new Vector3(mode.currentPosition.x, mode.currentPosition.y, 0);
        if(mode.inRange)
        {
            crossHair.GetComponent<SpriteRenderer>().sprite = crossHairSpriteGood;
        }
        else
        {
            crossHair.GetComponent<SpriteRenderer>().sprite = crossHairSpriteBad;
        }
    }

    private void createGameObject()
    {
        crossHair = new GameObject();

        crossHair.name = "Crosshair";
        crossHair.transform.position = new Vector3(0, 0, 0);
        crossHair.transform.SetParent(this.transform, true);

        SpriteRenderer sr = crossHair.AddComponent<SpriteRenderer>();
        sr.sprite = crossHairSpriteBad;
        sr.sortingLayerName = "CrossHair";

        crossHair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enableCrossHair()
    {
        crossHair.SetActive(true);
    }
}
