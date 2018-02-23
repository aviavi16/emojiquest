using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestLevelManager : Singleton<TestLevelManager> {

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;

        gameManager.DebugLog("Beginning Level");
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
    /*
    public void ReachedTarget()
    {
        Debug.Log("callback");
        if (targetClickable != null)
            NotifyClicked(targetClickable);
    }

    private bool FindClickable(Ray ray)
    {
        targetClickable = null;
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider == null)
            return false;

        Clickable c = hit.collider.GetComponent<Clickable>();
        if (c == null)
            return false;


        targetClickable = c;
        Debug.Log("hit " + c.name);
        return true;
    }


    public void NotifyClicked(Clickable c)
    {
        Debug.Log("note " + c.name + " stage=" + stage);
        if (binocular.gameObject.activeSelf)
            return;

        if (stage >= stages.Count)
            return;

        if (c.GetComponent<ShovelClickable>())
        {
            Destroy(c.gameObject);
            shovel.SetActive(true);
            return;
        }

        if (c.GetComponent<DigClickable>())
        {
            if (shovel.activeSelf)
                c.GetComponent<DigClickable>().Dig(mainChar.transform.position.x);
            return;
        }

        if (Time.time - lastClickTime < 0.5f)
            return;
        lastClickTime = Time.time;

        StageDef sdef = stages[stage];
        sdef.NotifyClicked(c);
        if (sdef.IsDone() && wonStages <= stage)
        {
            WinStage();
        }

    }
    

    private void WinStage()
    {
        StartCoroutine(MoveStage());
    }

    IEnumerator MoveStage()
    {
        ++wonStages;
        yield return new WaitForSeconds(2f);
        successUi.SetActive(true);
        if (stage == 0)
            characterRightEdge.transform.position = cameraRightEdge.transform.position + new Vector3(cam.orthographicSize,0,0);

        yield return new WaitForSeconds(2f);
        successUi.SetActive(false);
        ++stage;
        Debug.Log("inc stage " + stage);
        if (stage < stages.Count)
        {
            GameManager.instance.DebugLog("LevelManager.stages.Count()", stages.Count + "");
            GameManager.instance.DebugLog("LevelManager.MoveStage: levelManager.stages[0].Emit()");
            stages[stage].Emit();
        }
        //  SceneManager.LoadScene("title");
        
    }

    internal void NotifyWin()
    {
        StartCoroutine(NotifyWinI());
    }


    IEnumerator NotifyWinI()
    {

        winUi.SetActive(true);


        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("title");
        winUi.SetActive(false);
        float pos = 0;
        float initX = 0 + stage * 20;
        float endX = initX + 20;
        ++stage;
        while (pos < 1)
        {
            Vector3 p = cam.transform.position;
            float w = curve.Evaluate(pos);
            cam.transform.position = new Vector3(Mathf.Lerp(initX, endX, w), p.y, p.z);
            pos += 0.02f;
            yield return null;
        }
        if (stage<stages.Count)
            stages[stage].Emit();
            
    }


    public float GetMinX() { return characterLeftEdge.transform.position.x; }
    public float GetMaxX() { return characterRightEdge.transform.position.x; }
    public void SetMaxX(float val) { characterRightEdge.transform.position = new Vector3( val, characterRightEdge.transform.position.y, characterRightEdge.transform.position.z ); }
    public void SetMinX(float val) { characterLeftEdge.transform.position = new Vector3(val, characterLeftEdge.transform.position.y, characterLeftEdge.transform.position.z); }

    public int getSceneVer() { return sceneVer; }*/
}
