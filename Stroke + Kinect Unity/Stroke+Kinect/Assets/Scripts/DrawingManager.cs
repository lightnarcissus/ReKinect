using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class DrawingManager : MonoBehaviour {

    public List<GameObject> targets;
    public int currentLevel;
    public int directionID = 0; // 0 is clockwise, 1 is anti-clockwise
    public int lastTarget = 0;
    public List<int> levelLimit; //total targets -1
	private int currentLevelLimit=0;
    public int nextTarget = 0;
    public int scoreTarget = 15;
    public TimerText timerManager;
    public GameObject scoreManager;
    public Text scoreText;

    //indestructible common object
    public GameObject sceneManager;

    //important object references to be given to avatar controller
    public GameObject drawnLines; // kinectAvatar and avatarController variables in DrawMouse
    public GameObject calibrationManager;

    public GameObject avatarController;

    public Text focusText;

	// Use this for initialization
    void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
    }
	void Start () { 
        if(sceneManager!=null)
        {
            Debug.Log("focus side " + sceneManager.GetComponent<SceneManager>().focusSide);
            if (sceneManager.GetComponent<SceneManager>().focusSide == 1)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                focusText.text = "Focus Side: \n Left Arm";
            }
            if (sceneManager.GetComponent<SceneManager>().focusSide == 2)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 2;
                focusText.text = "Focus Side: \n Right Arm";
            }
            else
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                focusText.text = "Focus Side: \n Left Arm";
            }

                sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>().avatarControllers[0] = avatarController.GetComponent<AvatarController>();
        }
        for(int i=0;i<targets.Count;i++)
        {
            if (i == currentLevel)
                targets[i].SetActive(true);
            else
                targets[i].SetActive(false);
        }
		currentLevelLimit = levelLimit [currentLevel];

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AssignNextTarget(int hitTarget)
    {
        
        if (lastTarget != hitTarget) 
            CheckTargetIsCorrect(hitTarget);
        lastTarget = hitTarget;
        if (hitTarget != currentLevelLimit)
            hitTarget++;
        else
            hitTarget = 0;

        nextTarget = hitTarget;
       // Debug.Log("next target is: " + hitTarget);
    }

    void CheckTargetIsCorrect(int hitTarget)
    {
        int tempInt = scoreManager.GetComponent<ScoreManager>().RetrieveScore();
        if (hitTarget!=nextTarget)
        {
            

            if (tempInt>0)
                scoreManager.GetComponent<ScoreManager>().DecrementScore() ;
        }
        else
        {
            scoreManager.GetComponent<ScoreManager>().IncrementScore();
        }

        if(tempInt>scoreTarget)
        {
            UpdateLevel();
        }
      
    }


    void SetLevel(int activatedLevel)
    {
        targets[currentLevel].SetActive(false);
        //increment and then enable
        currentLevel=activatedLevel;
        targets[activatedLevel].SetActive(true);
        currentLevelLimit = levelLimit[currentLevel];
    }


    void UpdateLevel()
    {

        //first disable the current targets

        targets[currentLevel].SetActive(false);
        //increment and then enable
        currentLevel++;
        targets[currentLevel].SetActive(true);

		currentLevelLimit=levelLimit [currentLevel];
        scoreManager.GetComponent<ScoreManager>().ResetScore();
        if(currentLevel>=4)
        {
            currentLevel = 0;
        }
    }

    public void RestartLevel()
    {
        SetLevel(0);
        timerManager.ResetTimer();
        scoreManager.GetComponent<ScoreManager>().ResetScore();
    }
    
}
