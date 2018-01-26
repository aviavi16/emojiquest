using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateClickable : Clickable {

    private Color srcColor;
    public Color color;

    private void Start()
    {
        srcColor = GetComponent<Renderer>().material.color;
    }

    override public void DoAction()
    {

        GetComponent<Renderer>().material.color = color;
    }
    

    override public void UndoAction()
    {
        GetComponent<Renderer>().material.color = srcColor;
    }
}
