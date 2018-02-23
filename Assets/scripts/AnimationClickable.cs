using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClickable : Clickable {

    public Animator animator;
    
    private void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
    }

    override public void DoAction()
    {
        Debug.Log("Use!!!!!");
        //animator.SetInteger("SceneVer",GameManager.instance.levelManager.getSceneVer());
        animator.SetTrigger("Use");
    }
    

    override public void UndoAction()
    {
        //GetComponent<Renderer>().material.color = srcColor;
    }

    public void SetMinXtoCameraEdge()
    {
        Level1Manager.instance.SetMinX(Level1Manager.instance.cameraLeftEdge.transform.position.x);
    }


    public void SetMaxXtoCameraEdge()
    {
        Level1Manager.instance.SetMaxX(Level1Manager.instance.cameraRightEdge.transform.position.x);
    }
}
