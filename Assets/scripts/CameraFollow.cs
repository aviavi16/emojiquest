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
        float x = follow.position.x;
        x = Mathf.Clamp(x, LevelManager.instance.cameraLeftEdge.transform.position.x, LevelManager.instance.cameraRightEdge.transform.position.x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }}
