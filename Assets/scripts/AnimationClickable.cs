using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClickable : Clickable {

    
    private void Start()
    {
    }

    override public void DoAction()
    {
        GetComponent<Animator>().SetTrigger("Use");
    }
    

    override public void UndoAction()
    {
        //GetComponent<Renderer>().material.color = srcColor;
    }
}
