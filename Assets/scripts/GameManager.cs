using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private bool readyToPlay = false;

    public bool localOnly;
    public SocketManager socketManager;

    [SerializeField]
    private bool debugMode = false;
    public GameObject debugText;
    private int debugLines = 0;
    private string debugTextString = "";
    private string lastDebugString = "";
    private int debugLogRepeats = 1;

    void Start()
    {
       if ((!localOnly) || (GetComponent<SocketManager>() == null))
            socketManager = GetComponent<SocketManager>();
        
        DebugLog("Beginning Game");
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!readyToPlay)
        {
            if (localOnly) {
                readyToPlay = true;
            }
            
            //DebugLog("readyToPlay", ""+ readyToPlay, gameObject);
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
        //GameManager.instance.DebugLog("LevelManager.stages.Count()", LevelManager.instance.stages.Count +"");
        DebugLog("readytoplay: levelManager.stages[0].Emit()");
        //levelManager.stages[0].Emit();
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
            debugLines++;

            if (text != lastDebugString)
            {
                debugTextString = debugTextString + ((debugLogRepeats>1)?(" *" + debugLogRepeats):"") + "\n" + debugLines + ": " + text;
                debugText.GetComponent<Text>().text = debugTextString;
                debugLogRepeats = 1;
            }
            else
            {
                debugText.GetComponent<Text>().text = debugTextString + " *" + (++debugLogRepeats);
            }

            lastDebugString = text;
        }
    }
    public void DebugLog(string valueText, int value, GameObject go = null)
    {
        DebugLog(valueText, "" + value, go);
    }
    public void DebugLog(string valueText, float value, GameObject go = null)
    {
        DebugLog(valueText, "" + value, go);
    }
    public void DebugLog(string text, GameObject go = null)
    {
        DebugLog("", text, go);
    }



}
