using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SceneManager : MonoBehaviour {

    public GameObject avatarController;
    public GameObject kinectPrefab;
    public GameObject kinectManager;
    public static bool created = false;
    public static int currentApp = 0;
   
    public static string patientName = "";
    public static string age;
    public static int focusSide = 0; //1 for left and 2 for right

    public static float[] levelTime=new float[7];
    public static int[] levelScore = new int[7];
    public static int[] levelMalposition = new int[37];
    public static float totalTimePlayed;
    
	// Use this for initialization
	void Start () {
        if (!created)
        {
            kinectManager = Instantiate(kinectPrefab, transform.position, Quaternion.identity) as GameObject;
            created = true;
        }
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(kinectManager);
       // DontDestroyOnLoad(avatarController);
      //  DontDestroyOnLoad(Camera.main.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //   Destroy(kinectManager);
            ActivateApp(1);
         //   GameManager.activeApp = 1;
            currentApp = 1;
            //Application.LoadLevel("MainMenu");
          //  Destroy(gameObject);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateApp(2);
            currentApp = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateApp(3);
            currentApp = 3;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateApp(4);
            currentApp = 4; 
        }
	}

    public void UpdateLevelMalpositions(int app, int level, int poorBalance, int flexionSynergy, int shoulderShrug, int innerRotation, int wristDrop, int extensorSynergy)
    {
        levelMalposition[app * 4 + level * 6] = poorBalance;
        levelMalposition[app * 4 + level * 6 + 1] = flexionSynergy;
        levelMalposition[app * 4 + level * 6 + 2] = shoulderShrug;
        levelMalposition[app * 4 + level * 6 + 3] = innerRotation;
        levelMalposition[app * 4 + level * 6 + 4] = wristDrop;
        levelMalposition[app * 4 + level * 6 + 5] = extensorSynergy;

    }
    public void UpdateLevelScore(int app, int level, int score)
    {
        levelScore[app * 4 + level] = score;
    }
    public void UpdateCurrentLevelTime(int app, int level, float time)
    {
        levelTime[app*4+level] += time;
    }
    public void AddToTotalTime(float timePlayed)
    {
        Debug.Log("Patient Name is " + patientName);
        totalTimePlayed += timePlayed;
        GetComponent<CSVReader>().CSVRewrite(patientName, age, focusSide);
    }

    public void ActivateApp(int appNumber)
    {
        switch (appNumber)
        {

            case 1:
                GameManager.activeApp = 1;
                Application.LoadLevel("DrawingChallenge");
                break;
            case 2:
                GameManager.activeApp = 2;
                Application.LoadLevel("CardMatching");
                break;
            case 3:
                GameManager.activeApp = 3;
                Application.LoadLevel("ConductorMusic");
                break;
		case 4:
			GameManager.activeApp = 4;
			Debug.Log ("game manager is: " + GameManager.activeApp);
                Application.LoadLevel("HumanTuningFork");
                break;
        }
    }


}
