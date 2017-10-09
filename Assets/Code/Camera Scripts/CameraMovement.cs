using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Vector3 moveDir;
    private float horAxis;
    private float verAxis;
    private float yLim, xLim;
   
    Vector3 newPosition;

    public Transform topWall, rightWall;


    private void Start()
    {
        newPosition.z = transform.position.z;

        //Setup for camera limitations;
        yLim = topWall.position.y;
        xLim = rightWall.position.x;
        Debug.Log(yLim);
        Debug.Log(xLim);
    }

    void Update () {

#region Movement

        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");

        newPosition.x += horAxis;
        newPosition.y += verAxis;

        transform.position = Vector3.Lerp(
            transform.position, 
            newPosition,
            0.25f);

        #endregion

        
    }

    private void LateUpdate()
    {
        float camSize = Camera.main.orthographicSize;
        float aspectRatio = Screen.width / Screen.height;

        float maxY = xLim - camSize;
        float minY = maxY * -1;

        float maxX = yLim - (camSize * aspectRatio);
        float minX = maxX * -1;

        Vector3 v3 = newPosition;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);

        newPosition = v3;
    }
}
