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

    public void SetMinX(float val)
    {
        GameManager.instance.levelManager.SetMinX(val);
    }

    public void SetMaxX(float val)
    {
        GameManager.instance.levelManager.SetMaxX(val);
    }
}
