using UnityEngine;
using System.Collections;

public class TuningForkManager : MonoBehaviour {

	public static float malPosFactor=0f;
	public GameObject avatarController;

	public GameObject sceneManager;
	// Use this for initialization
	void Awake()
	{
		sceneManager = GameObject.Find("SceneManager");
	}
	void Start () {
		StartCoroutine (MalpositionManager.Instance.CheckMalPosValue ());
	}

	// Update is called once per frame
	void Update () {
		if(malPosFactor > 0.5f || Input.GetKey(KeyCode.F))
        {
            GetComponent<Sinus>().IncrementFrequency();
        }
       else
        {
            GetComponent<Sinus>().DecrementFrequency();
        }
    }
}
