using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TuningForkManager : MonoBehaviour {

	public static float malPosFactor=0f;
	public GameObject avatarController;
    public static int tuningForkTask = 0;
	public GameObject sceneManager;
    public GameObject[] instructionList;
    public float multFactor = 10f;
    public float clipFactor = 10f;
    private AudioSource aud;
    int currentClip = 0;
    bool shouldPlay = false;
    public AudioSource noiseAud;
   public AudioClip[] saxMinorAClips;
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
        aud = GetComponent<AudioSource>();
        noiseAud.volume = 0f;
        aud.volume = 0f;
    }
	void Start () {
        for(int i=0;i<instructionList.Length;i++)
        {
            instructionList[i].SetActive(false);
        }
		StartCoroutine (MalpositionManager.Instance.CheckMalPosValue ());
        StartCoroutine("TuningForkTasks");
        aud.volume = 0f;
       // PlayClips();
	}

    IEnumerator TuningForkTasks()
    {
        while(tuningForkTask<=5)
        {
            instructionList[tuningForkTask].transform.parent.gameObject.SetActive(true);
            instructionList[tuningForkTask].SetActive(true);
            aud.volume = 0f;
            shouldPlay = false;
            yield return StartCoroutine("WaitForShortTime", 5f);
            instructionList[tuningForkTask].SetActive(false);
            instructionList[tuningForkTask].transform.parent.gameObject.SetActive(false);
            MalpositionManager.Instance.SetTuningTask(tuningForkTask);
            aud.volume=1f;
            shouldPlay = true;
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
        Debug.Log("Malpos: " + malPosFactor);
     /*   if(shouldPlay)
        {
            // noise
            noiseAud.volume = malPosFactor * multFactor;
            aud.volume = Mathf.Clamp(1f - noiseAud.volume, 0f, 1f);
        }
        */
		if((KinectManager.userActive && malPosFactor > 0.1f) || Input.GetKey(KeyCode.F))
        {
                  GetComponent<Sinus>().IncrementFrequency(malPosFactor);
            }
       else
        {
          //  Debug.Log("NOPPPE");
            GetComponent<Sinus>().DecrementFrequency(malPosFactor);
        }
    }

    public void PlayClips()
    {
       // if (KinectManager.userActive && malPosFactor > 0.1f)
        //{

           int malPosClip = Mathf.Clamp(Mathf.FloorToInt(malPosFactor * multFactor),0,7);
            Debug.Log("THE CLIP: " + malPosClip);
        if (malPosClip == 0)
            malPosClip = 8;
        
        //Debug.Log(aud.isPlaying);
  /*
            currentClip++;
        if (currentClip == malPosClip)
            currentClip = 0;
        else
            currentClip = malPosClip;
        currentClip = Mathf.Clamp(currentClip, 0, 7);
    */
        aud.clip = saxMinorAClips[malPosClip];
            aud.Play();
            Invoke("PlayClips", saxMinorAClips[malPosClip].length);
        /*
        else
        {

            aud.clip = saxMinorAClips[malPosClip];
            aud.Play();
            Invoke("PlayClips", saxMinorAClips[malPosClip].length);
        }
        */
       /* }
        else
        {
            Invoke("PlayClips", 0.3f);
        }*/
    }
}
