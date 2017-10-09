using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Vector3 moveDir;
    private float horAxis;
    private float verAxis;
    private float yLim, xLim;
    private Vector3 newPosition;

    public Transform topWall, rightWall;
    public float camSpeed = 1;

    private void Start()
    {
        //Lock newPosition Z value
        newPosition.z = transform.position.z;

        //Setup for camera limitations;
        yLim = topWall.position.y - (topWall.localScale.x / 2);
        xLim = rightWall.position.x - (topWall.localScale.x / 2);
    }

    void Update () {

#region Movement

        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");

        newPosition.x += horAxis;
        newPosition.y += verAxis;

        //Lerp to the new position
        //TODO: Test whether camspeed actually has an effect
        transform.position = Vector3.Lerp(
            transform.position, 
            newPosition,
            0.25f * camSpeed);

        #endregion

        
    }

    private void LateUpdate()
    {
        //Get the camera's orthographic size and aspect ratio
        float camSize = Camera.main.orthographicSize;
        float aspectRatio = Screen.width / (float)Screen.height;

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
}
