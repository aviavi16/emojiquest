using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //TODO  change the mission text

    public static GameManager instance = null;

    public GameObject winUi;
    public GameObject successUi;

    public float cameraLeftBorder=0;
    public float cameraRightBorder = 100;
    public int stage;
    public BinacularScript binocular;

    public Camera cam;
    public AnimationCurve curve;
    public SocketManager socketManager;
    public MainChar mainChar;
    public GameObject shovel;


    private Clickable targetClickable = null;
    private bool readyToPlay = false;
    private float lastClickTime = -1;
    private int wonStages = 0;

    [SerializeField]
    private int sceneVer;



    public List<StageDef> stages;


    public float minX = -10;
    public float maxX = -4.5f;

    void Awake()
    {
        if (instance != null)
            throw new System.Exception("singelton not null");
        instance = this;
        
    }

    void Start()
    {
        binocular.setActive();
        stage = 0;
        winUi.SetActive(false);
        successUi.SetActive(false);
        socketManager = GetComponent<SocketManager>();
        shovel.SetActive(false);

    }

    public void ReadyToPlay()
    {
        readyToPlay = true;
        stages[0].Emit();

    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToPlay)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetMouseButtonDown(0))
        {
           
            if (binocular.gameObject.activeSelf)
                return;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            bool close = Mathf.Abs(mainChar.transform.position.x - ray.origin.x) < 1f;
            if (FindClickable(ray))
                if (close && targetClickable.GetComponent<DigClickable>()==null)
                    ReachedTarget();
                else
                    mainChar.SetGoTo(ray.origin.x);
            else
                mainChar.SetGoTo(ray.origin.x);
            
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
        Debug.Log("hit "+c.name);
        return true;
    }


    public void NotifyClicked(Clickable c)
    {
        Debug.Log("note " + c.name + " stage="+stage);
        if (binocular.gameObject.activeSelf)
            return;

        if (stage >= stages.Count)
             return;

        if (c.GetComponent<ShovelClickable>())
        {
            Destroy(c.gameObject);
            shovel.SetActive(true);
            if(sceneVer == 2)
            {
                GameObject blockingMindFuck = GameObject.Find("BlockingMindFuck");
                Destroy(blockingMindFuck);
            }
            return;
        }

        if (c.GetComponent<DigClickable>() )
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
            if (sdef.IsDone() && wonStages<=stage)
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
            maxX = 30;
        
        yield return new WaitForSeconds(2f);
        successUi.SetActive(false);
        ++stage;
        Debug.Log("inc stage " + stage);
        if (stage<stages.Count)
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
      
        //the name of the scene in which the roles change
        SceneManager.LoadScene("title- SceneVersion2");
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


    public float GetMinX() { return minX; }
    public float GetMaxX() { return maxX; }
    public void SetMaxX(float val) { maxX = val; }

    public int getSceneVer()
    {
        return sceneVer; 
    }
}
