using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    public Transform follow;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Level1Manager.instance != null)
        {
            float x = follow.position.x;
            x = Mathf.Clamp(x, Level1Manager.instance.cameraLeftEdge.transform.position.x + (GetComponent<Camera>().orthographicSize), Level1Manager.instance.cameraRightEdge.transform.position.x - GetComponent<Camera>().orthographicSize);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}
