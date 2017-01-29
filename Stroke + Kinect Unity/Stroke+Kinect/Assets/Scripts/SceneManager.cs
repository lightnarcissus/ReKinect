using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public bool mainMenu = false;
    public static float totalTimePlayed;

    //SINGLETON
    private static SceneManager _instance;

    public static SceneManager Instance
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

    }
    // Use this for initialization
    void Start () {
        if (!created)
        {
            /*
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("DrawingChallenge", LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("CardMatching", LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("ConductorMusic", LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("HumanTuningFork", LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            */
            Debug.Log("created new kinect");
            mainMenu = true;
            //kinectManager = Instantiate(kinectPrefab, transform.position, Quaternion.identity) as GameObject;
            kinectManager = GameObject.Find("KinectManager");
            if (kinectManager != null)
                DontDestroyOnLoad(kinectManager);
            created = true;
        }
        else
        {
            ResetConnections();
        }
        DontDestroyOnLoad(this.gameObject);
       // DontDestroyOnLoad(avatarController);
      //  DontDestroyOnLoad(Camera.main.gameObject);
    }

    public void ResetConnections()
    {
        /*
        Debug.Log("resetting connections");
        GameObject canvas = GameObject.Find("Canvas");
        if(canvas!=null)
        {
            Debug.Log("about to reset");
            Debug.Log(canvas.transform.GetChild(3).gameObject.name);
            Button drawingChallenge = canvas.transform.GetChild(3).GetChild(0).GetComponent<Button>();
            Button cardMatching= canvas.transform.GetChild(3).GetChild(1).GetComponent<Button>(); 
            Button musicPage= canvas.transform.GetChild(3).GetChild(2).GetComponent<Button>();
            Button conductorMusic= canvas.transform.GetChild(3).GetChild(0).GetComponent<Button>();
            Button tuningFork= canvas.transform.GetChild(3).GetChild(0).GetComponent<Button>();
            drawingChallenge.onClick.RemoveAllListeners();


        }
        */
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
            ActivateApp(5);
            currentApp = 3;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateApp(4);
            currentApp = 4; 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            mainMenu = true;
            ActivateApp(0);
            currentApp = 0;
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
        Debug.Log("activating some app");
        switch (appNumber)
        {

            case 1:
                GameManager.activeApp = 1;
                mainMenu = false;
                Application.LoadLevel("DrawingChallenge");
                break;
            case 2:
                GameManager.activeApp = 2;
                mainMenu = false;
                Application.LoadLevel("CardMatching");
                break;
		case 3:
			GameManager.Instance.gamePage.SetActive (false);
			GameManager.Instance.musicPage.SetActive (true);
                break;
		case 4:
			GameManager.activeApp = 4;
                mainMenu = false;
                Debug.Log ("game manager is: " + GameManager.activeApp);
                Application.LoadLevel("HumanTuningFork_2");
                break;
		case 5:
			GameManager.activeApp = 3;
                mainMenu = false;
                Application.LoadLevel ("ConductorMusic");
			break;
		case 6:
			GameManager.Instance.musicPage.SetActive (false);
			GameManager.Instance.gamePage.SetActive (true);
			break;
        case 0:
                Debug.Log("Go back to main menu");
                GameManager.activeApp = 0;
                KinectManager.Instance.gameObject.SetActive(false);
            Application.LoadLevel("MainMenu");
                ResetConnections();
                break;

			
        }
    }


}
