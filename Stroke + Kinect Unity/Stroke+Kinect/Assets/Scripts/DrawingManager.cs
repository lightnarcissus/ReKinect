﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class DrawingManager : MonoBehaviour {

    public List<GameObject> targets;
    public List<GameObject> indicatorTargets;
    public int currentLevel;
    public int directionID = 0; // 0 is clockwise, 1 is anti-clockwise
    public int lastTarget = 0;
    public List<int> levelLimit; //total targets -1
	private int currentLevelLimit=0;
    public int nextTarget = 0;
    private List<int> scoreTarget;
    public TimerText timerManager;
    public GameObject scoreManager;
    public GameObject malpositionManager;
    public int drawingDir = 0; // 1 is counter-clockwise for left  and 2 is clockwise for right
    //indestructible common object
    public GameObject sceneManager;

    //important object references to be given to avatar controller
    public GameObject drawnLines; // kinectAvatar and avatarController variables in DrawMouse
    public GameObject calibrationManager;
    public GameObject avatarController;
    public GameObject leftCanvas;
    public GameObject rightCanvas;
    public bool correct = false;

    public int totalAttempts = 0;

    public GameObject leftTrace;
    public GameObject rightTrace;

    public GameObject gradePanel;

    public GameObject leftInstructionPanel;
    public GameObject rightInstructionPanel;

	// Use this for initialization
    void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
    }
	void Start () { 
        if(sceneManager!=null || AlternateController.noKinect)
        {
            StartCoroutine("ShowInstructions");
            leftTrace.SetActive(false);
            rightTrace.SetActive(false);
            //Debug.Log("focus side " + sceneManager.GetComponent<SceneManager>().focusSide);
            if (!AlternateController.noKinect)
            {
                if (SceneManager.focusSide == 0)
                {
                    leftCanvas.SetActive(true);
                    rightCanvas.SetActive(false);
                    avatarController.GetComponent<AvatarController>().activeJoint = 1;
                    sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>().avatarControllers[0] = avatarController.GetComponent<AvatarController>();
                    drawingDir = 1; // left hand should move counter-clockwise
                                    // focusText.text = "Focus Side: \n Left Arm";
                                    //    scoreBox.GetComponent<RectTransform>().position = new Vector3(-397f, -227f, 0f);

                }
                if (SceneManager.focusSide == 1)
                {
                    leftCanvas.SetActive(false);
                    rightCanvas.SetActive(true);
                    avatarController.GetComponent<AvatarController>().activeJoint = 2;
                    sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>().avatarControllers[0] = avatarController.GetComponent<AvatarController>();
                    drawingDir = 2; //right hand should move clockwise
                                    //  focusText.text = "Focus Side: \n Right Arm";
                                    //  scoreBox.GetComponent<RectTransform>().position = new Vector3(269f, -227f, 0f);
                }
            }
            else
            {
                //no Kinect
                leftCanvas.SetActive(true);
                rightCanvas.SetActive(false);
                SceneManager.focusSide = 0;
                drawingDir = 1;
                // avatarController.GetComponent<AvatarController>().activeJoint = 1;
                // focusText.text = "Focus Side: \n Left Arm";
                //   scoreBox.GetComponent<RectTransform>().position = new Vector3(-397f, -227f, 0f);
            }

               
        }
        for(int i=0;i<targets.Count;i++)
        {
            if (i == currentLevel)
                targets[i].SetActive(true);
            else
                targets[i].SetActive(false);
        }
		currentLevelLimit = levelLimit [currentLevel];
       scoreTarget=scoreManager.GetComponent<ScoreManager>().scoreTarget;
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateLevel();
        }
        if (sceneManager != null)
        {
            //Debug.Log("focus side " + sceneManager.GetComponent<SceneManager>().focusSide);
            if (SceneManager.focusSide == 0)
            {
                if (currentLevel == 3)
                {
                    leftTrace.SetActive(true);
                    rightTrace.SetActive(false);
                }
                leftCanvas.SetActive(true);
                rightCanvas.SetActive(false);
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                drawingDir = 1; // left hand should move counter-clockwise
                                // focusText.text = "Focus Side: \n Left Arm";
                                //    scoreBox.GetComponent<RectTransform>().position = new Vector3(-397f, -227f, 0f);

            }
            if (SceneManager.focusSide == 1)
            {
                if (currentLevel == 3)
                {
                    leftTrace.SetActive(false);
                    rightTrace.SetActive(true);
                }
                leftCanvas.SetActive(false);
                rightCanvas.SetActive(true);
                avatarController.GetComponent<AvatarController>().activeJoint = 2;
                drawingDir = 2; //right hand should move clockwise
                                //  focusText.text = "Focus Side: \n Right Arm";
                                //  scoreBox.GetComponent<RectTransform>().position = new Vector3(269f, -227f, 0f);
            }
            else
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                // focusText.text = "Focus Side: \n Left Arm";
                //   scoreBox.GetComponent<RectTransform>().position = new Vector3(-397f, -227f, 0f);
            }

         //   sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>().avatarControllers[0] = avatarController.GetComponent<AvatarController>();
        }
    }

    public void AssignNextTarget(int hitTarget)
    {
        if(drawingDir==1)
        {
            if (lastTarget != hitTarget)
                CheckTargetIsCorrect(hitTarget);
            lastTarget = hitTarget;
            if (hitTarget != 0)
                hitTarget--;
            else
                hitTarget = currentLevelLimit;
        }
        else if (drawingDir == 2)
        {
            if (lastTarget != hitTarget)
                CheckTargetIsCorrect(hitTarget);
            lastTarget = hitTarget;
            if (hitTarget != currentLevelLimit)
                hitTarget++;
            else
                hitTarget = 0;
        }

        nextTarget = hitTarget;
       // Debug.Log("next target is: " + hitTarget);
    }

    void CheckTargetIsCorrect(int hitTarget)
    {
        int tempInt = scoreManager.GetComponent<ScoreManager>().RetrieveScore();
        totalAttempts++;
        if (hitTarget!=nextTarget)
        {
            if (tempInt>0)
                scoreManager.GetComponent<ScoreManager>().DecrementScore();
            correct = false;
           // StartCoroutine("FalseTarget")

        }
        else
        {
            correct = true;
            scoreManager.GetComponent<ScoreManager>().IncrementScore();
         //   StartCoroutine("CorrectTarget");
        }

        if(tempInt>scoreTarget[currentLevel])
        {
            UpdateLevel();
        }
      
    }


    void SetLevel(int activatedLevel)
    {
        targets[currentLevel].SetActive(false);
     //   indicatorTargets[currentLevel].SetActive(false);
        //increment and then enable
        currentLevel=activatedLevel;
        targets[activatedLevel].SetActive(true);
        currentLevelLimit = levelLimit[currentLevel];
    }


    void UpdateLevel()
    {

        //first disable the current targets

        //record information

        //time and score
        if (!AlternateController.noKinect)
        {
            float currentTimer = scoreManager.GetComponent<ScoreManager>().GetCurrentLevelTime();
            int currentScore = scoreManager.GetComponent<ScoreManager>().RetrieveScore();
            sceneManager.GetComponent<SceneManager>().UpdateLevelScore(0, currentLevel, currentScore);
            sceneManager.GetComponent<SceneManager>().UpdateCurrentLevelTime(0, currentLevel, currentTimer);
            sceneManager.GetComponent<SceneManager>().AddToTotalTime(currentTimer);

            //update malpositions
            int poorBalanceCount = malpositionManager.GetComponent<MalpositionManager>().RetrievePoorBalanceCount();
            int shoulderShrugCount = malpositionManager.GetComponent<MalpositionManager>().RetrieveShoulderShrugCount();
            int wristDropCount = malpositionManager.GetComponent<MalpositionManager>().RetrieveWristDropCount();
            int innerRotationCount = malpositionManager.GetComponent<MalpositionManager>().RetrieveInnerRotationCount();
            int extensorSynergyCount = malpositionManager.GetComponent<MalpositionManager>().RetrieveExtensorSynergyCount();
            int flexionSynergyCount = malpositionManager.GetComponent<MalpositionManager>().RetrieveFlexionSynergyCount();

            sceneManager.GetComponent<SceneManager>().UpdateLevelMalpositions(0, currentLevel, poorBalanceCount, flexionSynergyCount, shoulderShrugCount, innerRotationCount, wristDropCount, extensorSynergyCount);
        }
         //then finally increase the level
         
            targets[currentLevel].SetActive(false);

            //increment and then enable
            currentLevel++;
        if (currentLevel <= 3)
        {
            scoreManager.GetComponent<ScoreManager>().IncreaseLevel();
            Debug.Log("NOW ON LEVEL " + currentLevel);
            targets[currentLevel].SetActive(true);

            currentLevelLimit = levelLimit[currentLevel];
        }

       

        scoreManager.GetComponent<ScoreManager>().ResetScore();
        if(currentLevel>=4)
        {
            Debug.Log("should be showing grading"); 
            scoreManager.GetComponent<ScoreManager>().ShowGrading();
            scoreManager.GetComponent<ScoreManager>().accuracyVal = (float) 68f/totalAttempts;
            currentLevel = 0;
        }
    }

    IEnumerator ShowInstructions()
    {
        if(SceneManager.focusSide==0)
        {
            leftInstructionPanel.SetActive(true);
            rightInstructionPanel.SetActive(false);
            yield return new WaitForSeconds(5f);
            leftInstructionPanel.SetActive(false);
        }
        else
        {
            rightInstructionPanel.SetActive(true);
            leftInstructionPanel.SetActive(false);
            yield return new WaitForSeconds(5f);
            rightInstructionPanel.SetActive(false);
        }
        
        yield return null;
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void RestartLevel()
    {
        SetLevel(0);
        totalAttempts = 0;
        timerManager.ResetTimer();
        scoreManager.GetComponent<ScoreManager>().ResetScore();
    }

    
    
}
