using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start()
    {
        this.gameObject.SetActive(false);
        world.gameState.registerGameEnds(showEndScreen);
    }

    private void showEndScreen(Actor actor)
    {
        this.gameObject.SetActive(true);
        Text announcer = this.GetComponentInChildren<Text>();

        Debug.Log(announcer);


        announcer.text = "You lost!";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
