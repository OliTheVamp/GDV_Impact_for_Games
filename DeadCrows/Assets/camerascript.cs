using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{

    public Camera usedcamera; public GameObject player1; public GameObject player2;
    Vector3 cameralocation;
    // Start is called before the first frame update
    void Start()
    {
        usedcamera = this.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        FixedCameraFollowSmooth(usedcamera, player1.transform, player2.transform);
    }


    public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
      
        float zoomFactor = 1.0f;
        float followTimeDelta = 0.8f;


        Vector3 midpoint = (t1.position + t2.position) / 2f;

       
        float distance = (t1.position - t2.position).magnitude;

     
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

       
       // if (cam.orthographic)
      //  {
           
     //       cam.orthographicSize = distance;
     //   }

        //  cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);
        cameralocation = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);
        cam.transform.position = new Vector3(0.0f, cameralocation.y + 6.5f, cameralocation.z);
        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
        {
            cam.transform.position = cameraDestination;
        }


       

    }



}
