using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour {


    private float targetX;
    private bool moveMode = false;
    public float vel = 0.2f;
    private bool callback = false;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!moveMode)
            return;

        Vector3 p = transform.position;
        float x = p.x;
        float diff = targetX-x;


        
        float tvel = vel * Time.deltaTime;
     //   Debug.Log("x=" + x + " tvel=" + tvel+" targetX="+targetX);
        if (Mathf.Abs(diff) <= tvel)
        {
            x = targetX;
            moveMode = false;
            if (callback)
                GameManager.instance.ReachedTarget();
        } 
        else
            x += tvel * Mathf.Sign(diff);
        //Debug.Log("x=" + x + " tvel=" + tvel + " targetX=" + targetX);

        transform.position = new Vector3(x, p.y, p.z);
        


    }

    public void SetGoTo(float x)
    {

        targetX = Mathf.Clamp(x,GameManager.instance.GetMinX(),GameManager.instance.GetMaxX());
        callback = Mathf.Abs(targetX - x) <= 0.5f;
        moveMode = true;
    }


   

}
