﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickStart2()
    {
        SceneManager.LoadScene(3);
    }
}
