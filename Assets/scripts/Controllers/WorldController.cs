using UnityEngine;
using System.Collections;
using System;

public class WorldController : MonoBehaviour
{


    public static WorldController Instance { get; protected set; }

    public World World
    {
        get; protected set;
    }

    // Use this for initialization
    void OnEnable()
    {
        if (Instance != null)
        {
            Debug.LogError("There should never be two world controllers.");
        }
        Instance = this;

        this.World = new World(true);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        World.update(Time.deltaTime);
    }
}
