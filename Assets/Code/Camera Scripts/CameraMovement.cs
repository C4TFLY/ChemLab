using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Vector3 moveDir, newPosition, mousePos;
    private float horAxis, verAxis;
    private float yLim, xLim, aspectRatio, wallWidth;
    private float mouseScroll;
    private Camera mainCamera;
    private float newZoom, startTime;
    private float camSize;

    bool zoomChanging = false;



    public Transform topWall, rightWall;
    public float camSpeed = 1;
    public float zoomDuration = 10;

    private void Start()
    {
        //Lock newPosition Z value
        newPosition.z = transform.position.z;

        wallWidth = topWall.gameObject.GetComponent<BoxCollider>().size.y;

        //Setup for camera limitations;
        yLim = topWall.position.y - (wallWidth / 2);
        xLim = rightWall.position.x - (wallWidth / 2);
        mainCamera = Camera.main;
        newZoom = mainCamera.orthographicSize;
    }

    void Update () {

#region Setup

        aspectRatio = Screen.width / (float)Screen.height;
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        mousePos = Input.mousePosition;
        camSize = mainCamera.orthographicSize;

        #endregion

#region Zoom
        
        if (mouseScroll > 0 && mainCamera.orthographicSize > 1)
        {
            //Zoom out from cursor position
            ZoomCamera(mainCamera.ScreenToWorldPoint(mousePos), 1);
        }
        else if (mouseScroll < 0 && mainCamera.orthographicSize < topWall.position.y - (wallWidth / 2))
        {
            //Zoom in to cursor position
            ZoomCamera(mainCamera.ScreenToWorldPoint(mousePos), -1);
        }
        
        mainCamera.orthographicSize = Mathf.Lerp(
            mainCamera.orthographicSize,
            newZoom,
            (Time.time - startTime) / zoomDuration);
        

        zoomChanging = (newZoom == mainCamera.orthographicSize) ? false : true;

        if (zoomChanging)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                newPosition,
                (Time.time - startTime) / zoomDuration);
        }

        #endregion

#region Movement

        newPosition.x += horAxis;
        newPosition.y += verAxis;

        //Lerp to the new position
        //TODO: Test whether camspeed actually has an effect
        if (!zoomChanging)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                newPosition,
                0.25f * camSpeed);
        }

        #endregion

    }

    private void LateUpdate()
    {
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

    private void ZoomCamera(Vector3 zoomTowards, float amount)
    {
        if (!zoomChanging)
            startTime = Time.time;
        newZoom -= amount;
        newZoom = Mathf.Clamp(newZoom, 1, topWall.position.y - (wallWidth / 2));

        float multiplier = (1.0f / newZoom * amount);

        //newPosition = transform.position;
        newPosition += (zoomTowards - transform.position) * multiplier;
        
    }
}
