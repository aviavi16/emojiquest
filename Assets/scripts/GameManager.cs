using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public LevelManager levelManager = null;

    private bool readyToPlay = false;

    public SocketManager socketManager;

    [SerializeField]
    private bool debugMode = false;
    [SerializeField]
    private GameObject debugText;
    private int debugLines = 0;
    private string debugTextString = "";

    

    void Start()
    {
        socketManager = GetComponent<SocketManager>();
        
        debugText.GetComponent<Text>().enabled = debugMode;
        DebugLog("Beginning Game");
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!readyToPlay)
        {
            DebugLog("readyToPlay", ""+ readyToPlay, gameObject);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DebugLog("Input.GetKeyDown(KeyCode.Escape)");
            SceneManager.LoadScene(0);
            return;
        }
    }

    public void ReadyToPlay()
    {
        readyToPlay = true;
        levelManager.stages[0].Emit();

    }

    /// <summary>
    /// Omri: Method for onscreen debug, for use in build debugs.
    /// </summary>
    /// <param name="valueText">the value's name or description to be printed</param>
    /// <param name="value">the value</param>
    /// <param name="go">the gameobject closest to the printed value for debugging in the editor (optional)</param>
    public void DebugLog(string valueText, string value, GameObject go = null)
    {
        string text = valueText + ((valueText == "") ? "" : ": ") + value;
        
        Debug.Log(text, go);
        if (debugMode)
        {
            debugTextString = text + "\n" + debugTextString;
            debugText.GetComponent<Text>().text = "lines: " + ++debugLines + "\n" + debugTextString;
        }
    }
    public void DebugLog(string text, GameObject go = null)
    {
        DebugLog("", text, go);
    }


}
