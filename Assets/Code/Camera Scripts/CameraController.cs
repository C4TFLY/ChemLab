using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 moveDir, newPosition, mousePos;
    private Camera mainCamera;
    private float horAxis, verAxis, mouseScroll;
    private float yLim, xLim, aspectRatio, wallWidth;
    private float newZoom;
    private float camSize, previousCamSize;
    private bool zoomChanging = false;
    private bool zoomInButton, zoomInButtonNP, zoomOutButton, zoomOutButtonNP;

    public Transform topWall, rightWall;
    [Range(0, 4)] public float camSpeed = 1;
    [Range(1, 20)] public float maxZoom = 1;
    public float zoomDuration = 10;

    private void Start()
    {

#region First-time assignments

        //Lock newPosition Z value
        newPosition.z = transform.position.z;

        wallWidth = topWall.gameObject.GetComponent<BoxCollider>().size.y;

        //Setup for camera limitations
        yLim = topWall.position.y - (wallWidth / 2);
        xLim = rightWall.position.x - (wallWidth / 2);
        mainCamera = Camera.main;
        newZoom = mainCamera.orthographicSize;

        #endregion
         
    }

    void Update ()
    {

#region Setup

        aspectRatio = Screen.width / (float)Screen.height;
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        mousePos = Input.mousePosition;
        camSize = mainCamera.orthographicSize;
        zoomInButtonNP = Input.GetKey(KeyCode.KeypadPlus);
        zoomInButton = Input.GetKey(KeyCode.Equals);
        zoomOutButtonNP = Input.GetKey(KeyCode.KeypadMinus);
        zoomOutButton = Input.GetKey(KeyCode.Minus);

        #endregion

#region Zoom

        if (camSize > maxZoom)
        {
            if (mouseScroll > 0)
            {
                //Zoom in to cursor position
                ZoomCamera(mainCamera.ScreenToWorldPoint(mousePos), 1);
            }
            else if (zoomInButton
                    || zoomInButtonNP)
            {
                //Zoom in to cursor position
                ZoomCamera(mainCamera.ScreenToWorldPoint(mousePos), .1f);
            }
        }
        if (camSize < topWall.position.y - (wallWidth / 2) - 0.05f)
        {
            if (mouseScroll < 0)
            {
                //Zoom out from cursor position
                ZoomCamera(mainCamera.ScreenToWorldPoint(mousePos), -1);
            }
            else if (zoomOutButton
                    || zoomOutButtonNP)
            {
                //Zoom out from cursor position
                ZoomCamera(mainCamera.ScreenToWorldPoint(mousePos), -.1f);
            }
        }

        float zoomVelocity = 0f;
        mainCamera.orthographicSize = Mathf.SmoothDamp(
            camSize,
            newZoom,
            ref zoomVelocity,
            Time.deltaTime * Mathf.Pow(zoomDuration, 2));
        
        zoomChanging = (previousCamSize == camSize) ? false : true;

        #endregion

#region Movement
        //Set the new position
        float speedMultiplier = Time.deltaTime * camSpeed * (camSize * 10);
        newPosition.x += horAxis * speedMultiplier;
        newPosition.y += verAxis * speedMultiplier;

        //Lerp to the new position deoending on whether the zoom is active or not
        if (zoomChanging)
        {
            Vector3 moveVel = new Vector3(0, 0, 0);
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref moveVel, Time.deltaTime * Mathf.Pow(zoomDuration, 2));
        }
        else if (!zoomChanging)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                newPosition,
                0.25f);
        }

        #endregion
        
    }

    private void LateUpdate()
    {
        //Assign the limit of the camera's movement
        float maxY = yLim - camSize;
        float maxX = xLim - (camSize * aspectRatio);
        
        transform.position = ClampVectorXY(transform.position, maxX, maxY);
        newPosition = ClampVectorXY(newPosition, maxX, maxY);
        previousCamSize = camSize;

        if (transform.position.y < 0.005f
            && transform.position.y > -0.005f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        //Fix for camera size being of values using more than 2 decimals
        if ((camSize > newZoom && (camSize - newZoom) < 0.01)
            || (camSize < newZoom && (newZoom - camSize) < 0.01))
        {
            mainCamera.orthographicSize = newZoom;
        }
    }

    /// <summary>
    /// Method for zooming the camera towards the mouse position
    /// </summary>
    /// <param name="zoomTowards">The mouse position</param>
    /// <param name="amount">How much the camera size should change per scroll</param>
    private void ZoomCamera(Vector3 zoomTowards, float amount)
    {
        newZoom -= amount;
        newZoom = Mathf.Clamp(newZoom,
            maxZoom,
            topWall.position.y - (wallWidth / 2));

        float multiplier = (1.0f / newZoom * amount);

        newPosition += (zoomTowards - transform.position) * multiplier;
        
    }

    /// <summary>
    /// Clamp a vector along the X-Y axis to within an area. Center is considered to be 0,0,0.
    /// </summary>
    /// <param name="vector">The vector to clamp.</param>
    /// <param name="maxX">The maximum X value.</param>
    /// <param name="maxY">The minimum Y value.</param>
    /// <returns>The clamped vector.</returns>
    private Vector3 ClampVectorXY(Vector3 vector, float maxX, float maxY)
    {
        float minX = maxX * -1;
        float minY = maxY * -1;

        Vector3 clampVector = vector;
        clampVector.x = Mathf.Clamp(clampVector.x, minX, maxX);
        clampVector.y = Mathf.Clamp(clampVector.y, minY, maxY);
        return clampVector;
    }
}