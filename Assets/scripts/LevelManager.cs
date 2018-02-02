using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public GameObject winUi;
    public GameObject successUi;

    public GameObject cameraLeftEdge;
    public GameObject cameraRightEdge;
    public GameObject characterLeftEdge;
    public GameObject characterRightEdge;

    public int stage;
    public BinacularScript binocular;

    public Camera cam;
    public AnimationCurve curve;
    public MainChar mainChar;
    public GameObject shovel;

    private Clickable targetClickable = null;
    private float lastClickTime = -1;
    private int wonStages = 0;

    public List<StageDef> stages;

  

    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        GameManager.instance.levelManager = this;

        stage = 0;
        winUi.SetActive(false);
        successUi.SetActive(false);
        shovel.SetActive(false);
        GameManager.instance.DebugLog("Beginning Level");
    }
    

    // Update is called once per frame
    void Update()
    {
        /**if (!readyToPlay)
        {
            DebugLog("readyToPlay", ""+ readyToPlay, gameObject);
            return;
        }**/

        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.DebugLog("Input.GetMouseButtonDown(0)", "" + Input.GetMouseButtonDown(0), gameObject);
            if (binocular.gameObject.activeSelf)
                return;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            bool close = Mathf.Abs(mainChar.transform.position.x - ray.origin.x) < 1f;
            if (FindClickable(ray))
                if (close && targetClickable.GetComponent<DigClickable>() == null)
                {
                    ReachedTarget();
                }
                else
                {
                    mainChar.SetGoTo(ray.origin.x);
                }
            else
            {
                //GameManager.instance.DebugLog("character, ray: ", mainChar.transform.position.x + ", " + ray.origin.x, gameObject);
                mainChar.SetGoTo(ray.origin.x);
            }
        }
    }
    
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
            stages[stage].Emit();
        //  SceneManager.LoadScene("title");
        /*winUi.SetActive(false);
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
            */
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
}
