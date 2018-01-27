using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour {
    public int number;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {


    }

   // void OnMouseDown()
 //   {
       // GameManager.instance.NotifyClicked(this);
//    }


    virtual public void DoAction() { }
    virtual public void UndoAction() { }
   

}
