using UnityEngine;
using System.Collections;
using System;

public class MouseController : MonoBehaviour {
    // The world-position of the mouse last frame.
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        movePlayer();
        updateCameraMovement();

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    private void movePlayer()
    {

        if (Input.GetMouseButtonDown(0))
        {
            int x = Mathf.FloorToInt(currFramePosition.x + 0.5f);
            int y = Mathf.FloorToInt(currFramePosition.y + 0.5f);

            world.getCurrentActor().move(x, y);
        }
    }

    void updateCameraMovement()
    {
        // Handle screen panning
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {   // Right or Middle Mouse Button

            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);

        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 15f);
    }
}
