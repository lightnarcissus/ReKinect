using UnityEngine;
using System.Collections;

public class TuningForkManager : MonoBehaviour {

	public static float malPosFactor=0f;
	public GameObject avatarController;
    public static int tuningForkTask = 0;
	public GameObject sceneManager;
    public GameObject[] instructionList;
    //SINGLETON
    private static TuningForkManager _instance;

    public static TuningForkManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {

        if (_instance != null)
        {
            Debug.Log("Instance already exists!");
            Destroy(this);
            return;
        }
        _instance = this;
        sceneManager = GameObject.Find("SceneManager");
    }
	void Start () {
        for(int i=0;i<instructionList.Length;i++)
        {
            instructionList[i].SetActive(false);
        }
		StartCoroutine (MalpositionManager.Instance.CheckMalPosValue ());
        StartCoroutine("TuningForkTasks");
	}

    IEnumerator TuningForkTasks()
    {
        while(tuningForkTask<=5)
        {
            instructionList[tuningForkTask].transform.parent.gameObject.SetActive(true);
            instructionList[tuningForkTask].SetActive(true);
            yield return StartCoroutine("WaitForShortTime", 5f);
            instructionList[tuningForkTask].SetActive(false);
            instructionList[tuningForkTask].transform.parent.gameObject.SetActive(false);
            MalpositionManager.Instance.SetTuningTask(tuningForkTask);
           
            yield return StartCoroutine("WaitForShortTime",10f);
            tuningForkTask++;
            
            yield return 0;
        }
        yield return null;
    }

    IEnumerator WaitForShortTime(float waitTime)
    {

        float timer = 0f;
        while(timer<waitTime)
        {
            if (Input.GetKeyDown(KeyCode.N))
                timer = waitTime;
            timer += Time.deltaTime;
            yield return 0;
        }
        yield return null;
    }

	// Update is called once per frame
	void Update () {
        Debug.Log(malPosFactor);
		if((KinectManager.userActive && malPosFactor > 0.1f) || Input.GetKey(KeyCode.F))
        {
           // Debug.Log("innit");
            GetComponent<Sinus>().IncrementFrequency(malPosFactor);
        }
       else
        {
          //  Debug.Log("NOPPPE");
            GetComponent<Sinus>().DecrementFrequency(malPosFactor);
        }
    }
}
