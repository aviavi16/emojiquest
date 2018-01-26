using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public GameObject winUi;

    public int stage;
    public int expectedNumber;
    public int[] maxNum={3,3};

    private List<Clickable> goodClicked = new List<Clickable>();
    // Use this for initialization

    void Start () {
        

        if (instance != null)
            throw new System.Exception("singelton not null");
        instance = this;
        expectedNumber = 0;
        stage = 0;
        winUi.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NotifyClicked(Clickable c)
    {
        if (goodClicked.Contains(c))
            return;
        if (c.number == expectedNumber)
        {
            c.DoAction();
            ++expectedNumber;
            goodClicked.Add(c);
            if (expectedNumber == maxNum[stage])
                WinStage();
        } else
        {
            foreach (Clickable gc in goodClicked)
            {
                gc.UndoAction();
            }
            goodClicked.Clear();
            expectedNumber = 0;
        }


    }

    
    

    private void WinStage()
    {
        if (stage == maxNum.Length)
            ;
        else
            StartCoroutine(MoveStage());
    }

    IEnumerator MoveStage()
    {
        winUi.SetActive(true);
        return null; 
    }

}
