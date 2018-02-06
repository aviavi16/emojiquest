using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinacularScript : MonoBehaviour {
    public bool hidden = false;

    // Use this for initialization
    void Start () {
        GetComponent<Image>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        if ((!hidden && GameManager.instance.socketManager.SentFirstMessage()) || Input.GetKeyDown(KeyCode.B))
        {
            hidden = true;
            gameObject.SetActive(false);
           // Destroy(this.gameObject);
        }
	}

    public void setActive()
    {
        gameObject.SetActive(true);
    }
}
