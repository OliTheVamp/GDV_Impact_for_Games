// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player1;
    public GameObject player2;
    
    public float camoffset = 5.0f;

    public bool firstPrintDone = false;
    
    Vector3 cameralocation;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = this.gameObject.GetComponent<Camera>();

        if (mainCamera == null) print("null camera..");
    }

    // Update is called once per frame
    void Update()
    {
        FixedCameraFollowSmooth(mainCamera, player1.transform, player2.transform);
    }

    public void FixedCameraFollowSmooth(Camera thisCamera, Transform firstPlayer, Transform secondPlayer)
    {
        // Adjust to avoid the camera showing too much below the level
        float adjustedP1YPos = firstPlayer.position.y;
        float adjustedP2YPos = secondPlayer.position.y;

        float cameraMinHeight = (adjustedP1YPos < adjustedP2YPos) ? adjustedP1YPos - 1 : adjustedP2YPos - 1;
        float cameraMaxHeight = (adjustedP1YPos > adjustedP2YPos) ? adjustedP1YPos + 1 : adjustedP2YPos + 1;

        // Calculate midpoint between both players
        float midpoint = (cameraMinHeight + cameraMaxHeight) / 2;

        //print("midpoint:" + midpoint + ", cameraMinHeight:" + cameraMinHeight + " vs adjustedP2YPos:" + adjustedP2YPos);

        Vector3 currentCameraPos = thisCamera.transform.position;

        if (midpoint > cameraMinHeight 
         && midpoint < cameraMaxHeight) thisCamera.transform.position = new Vector3(currentCameraPos.x, midpoint, currentCameraPos.z);

        firstPrintDone = true;
    }
}