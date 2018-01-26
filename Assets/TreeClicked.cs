using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeClicked : MonoBehaviour {

    public Animator animator;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        

    }

    private void OnMouseUp()
    {
       
        Debug.Log(" clicked");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("tree hit");

            if (hit.transform.name == "Tree")
            {
                Debug.Log("tree ");
                animator.SetTrigger("Shake");

            }
        }
    }

}
