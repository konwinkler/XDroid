using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    // The world-position of the mouse last frame.
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    World world
    {
        get { return WorldController.Instance.World; }
    }

    // Use this for initialization
    void Start()
    {

    }

    public void endButton()
    {
        Debug.Log("Quit.");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        int x = Mathf.FloorToInt(currFramePosition.x + 0.5f);
        int y = Mathf.FloorToInt(currFramePosition.y + 0.5f);
        Tile tile = world.getTileAt(x, y);

        interfaceInput(tile);
        updatePosition(tile);
        updateCameraMovement();

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    private void updatePosition(Tile tile)
    {
        if (tile != null)
        {
            world.gameState.updateMousePosition(tile);
        }
    }

    private void interfaceInput(Tile tile)
    {
        // If we're over a UI element, then bail out from this.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (tile != null)
            {
                world.gameState.click(tile);
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            world.gameState.setModeToMovement();
        }
    }

    void updateCameraMovement()
    {
        // Handle screen panning
        if (Input.GetMouseButton(2))
        {   // Right or Middle Mouse Button

            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);

        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 15f);
    }
}
