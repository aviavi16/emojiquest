using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigClickable : Clickable {

    public GameObject anim;
    
    public GameObject chest;


    private void Start()
    {
        chest.SetActive(false);
    }
    public void Dig(float x)
    {
        Debug.Log("dig");
        anim.SetActive(false);
        Vector3 p = anim.transform.position;
        anim.transform.position = new Vector3(x, p.y, p.z);
        anim.SetActive(true);
        if (Mathf.Abs(chest.transform.position.x -1- x) < 1)
        {
            chest.SetActive(true);
            GameManager.instance.NotifyWin();
        }
    }
}
