using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Vector3 moveDir, newPosition;
    private float horAxis, verAxis;
    private float yLim, xLim, aspectRatio, wallWidth;
    private float mouseScroll;
    private Camera mainCamera;
    private float newZoom;

    public Transform topWall, rightWall;
    public float camSpeed = 1;

    private void Start()
    {
        //Lock newPosition Z value
        newPosition.z = transform.position.z;

        wallWidth = topWall.gameObject.GetComponent<BoxCollider>().size.y;

        //Setup for camera limitations;
        yLim = topWall.position.y - (wallWidth / 2);
        xLim = rightWall.position.x - (wallWidth / 2);
        mainCamera = Camera.main;
    }

    void Update () {
        aspectRatio = Screen.width / (float)Screen.height;

        #region Setup

        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        #endregion

        #region Movement


        // Scroll forward
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        }

        // Scoll back
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), -1);
        }

        newPosition.x += horAxis;
        newPosition.y += verAxis;

        //Lerp to the new position
        //TODO: Test whether camspeed actually has an effect
        transform.position = Vector3.Lerp(
            transform.position,
            newPosition,
            0.25f * camSpeed);

        #endregion

        #region Zoom


        //newZoom = mainCamera.orthographicSize + (mouseScroll * -10);
        //mainCamera.orthographicSize = newZoom;
        //mainCamera.orthographicSize = Mathf.Lerp(
        //    mainCamera.orthographicSize,
        //    newZoom,
        //    0.1f);

        //mainCamera.orthographicSize = Mathf.Clamp(
        //    mainCamera.orthographicSize,
        //    1f,
        //    topWall.position.y - (wallWidth / 2));


        //if (mainCamera.transform.position.y < 0.001
        //    && mainCamera.transform.position.y > -0.001
        //    && mainCamera.transform.position.y != 0)
        //{
        //    mainCamera.transform.position = new Vector3(
        //        mainCamera.transform.position.x,
        //        0,
        //        mainCamera.transform.position.z);
        //}

        #endregion

        


    }

    private void LateUpdate()
    {
        //Get the camera's orthographic size and aspect ratio
        float camSize = mainCamera.orthographicSize;

        //Assign the limit of the camera's movement
        float maxY = yLim - camSize;
        float minY = maxY * -1;
        float maxX = xLim - (camSize * aspectRatio);
        float minX = maxX * -1;

        //Clamp the newPosition vector to the limits
        Vector3 clampVector = newPosition;
        clampVector.x = Mathf.Clamp(clampVector.x, minX, maxX);
        clampVector.y = Mathf.Clamp(clampVector.y, minY, maxY);

        newPosition = clampVector;
    }

    void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
    {
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / mainCamera.orthographicSize * amount);

        // Move camera
        transform.position += (zoomTowards - transform.position) * multiplier;

        // Zoom camera
        mainCamera.orthographicSize -= amount;

        // Limit zoom
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 1, topWall.position.y - (wallWidth / 2));
        newPosition = transform.position;
    }
}
