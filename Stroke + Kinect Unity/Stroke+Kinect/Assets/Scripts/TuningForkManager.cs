using UnityEngine;
using System.Collections;

public class TuningForkManager : MonoBehaviour {

	public static float malPosFactor=0f;
	public GameObject avatarController;
    public static int tuningForkTask = 0;
	public GameObject sceneManager;
	// Use this for initialization
	void Awake()
	{
		sceneManager = GameObject.Find("SceneManager");
	}
	void Start () {
		StartCoroutine (MalpositionManager.Instance.CheckMalPosValue ());
        StartCoroutine("TuningForkTasks");
	}

    IEnumerator TuningForkTasks()
    {
        while(tuningForkTask<=5)
        {
            MalpositionManager.Instance.SetTuningTask(tuningForkTask);
            yield return new WaitForSeconds(30f);
            tuningForkTask++;
            yield return 0;
        }
        yield return null;
    }

	// Update is called once per frame
	void Update () {
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
