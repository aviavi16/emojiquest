using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject winUi;

    public int stage;

    public Camera cam;
    public AnimationCurve curve;
    public SocketManager socketManager;
    public MainChar mainChar;

    private Clickable targetClickable = null;

    public List<StageDef> stages;


    private float minX = -10;
    private float maxX = -4.5f;

    void Start()
    {


        if (instance != null)
            throw new System.Exception("singelton not null");
        instance = this;
        stage = 0;
        winUi.SetActive(false);
        socketManager = GetComponent<SocketManager>();
    }

    public void ReadyToPlay()
    {
        //stages[0].Emit();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            bool close = Mathf.Abs(mainChar.transform.position.x - ray.origin.x) < 1f;
            if (FindClickable(ray))
                if (close)
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
        Debug.Log("hit");
        return true;
    }


    public void NotifyClicked(Clickable c)
    {
        if (stage >= stages.Count)
             return;
         StageDef sdef = stages[stage];
         sdef.NotifyClicked(c);
         if (sdef.IsDone())
             WinStage();

        
    }




    private void WinStage()
    {
        StartCoroutine(MoveStage());
    }

    IEnumerator MoveStage()
    {
        yield return new WaitForSeconds(2f);
        winUi.SetActive(true);
        if (stage == 0)
            maxX = 30;

        yield return new WaitForSeconds(5f);
       
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


    public float GetMinX() { return minX; }
    public float GetMaxX() { return maxX; }
    public void SetMaxX(float val) { maxX = val; }
}
