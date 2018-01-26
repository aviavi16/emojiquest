using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public GameObject winUi;

    public int stage;

    public Transform cam;
    public AnimationCurve curve;



    public List<StageDef> stages;

    void Start () {
        

        if (instance != null)
            throw new System.Exception("singelton not null");
        instance = this;
        stage = 0;
        winUi.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
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
        winUi.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        winUi.SetActive(false);
        float pos = 0;
        float initX = 0 + stage * 20;
        float endX = initX + 20;
        ++stage; 
        while (pos<1)
        {
            Vector3 p = cam.transform.position;
            float w = curve.Evaluate(pos);
            cam.transform.position = new Vector3(Mathf.Lerp(initX, endX, w), p.y, p.z);
            pos += 0.02f;
            yield return null;
        }
    }

}
