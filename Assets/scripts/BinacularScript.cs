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
        if (!hidden && socketManager.SentFirstMessage())
        {
            hidden = true;
            Destroy(this.gameObject);
        }
	}
}
