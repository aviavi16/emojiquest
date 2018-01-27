using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinacularScript : MonoBehaviour {
    public SocketManager socketManager;
    bool hidden = false;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if ((!hidden && socketManager.SentFirstMessage()) || Input.GetKeyDown(KeyCode.B))
        {
            hidden = true;
            gameObject.SetActive(false);
           // Destroy(this.gameObject);
        }
	}
}
