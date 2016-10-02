using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {

    public GameObject sceneManager;
    public GameObject avatarController;
    public TimerText timerManager;
    public ScoreManager scoreManager;
    public GameObject calibManager;
    public GameObject malpositionManager;
    public GameObject leftCanvas;
    public GameObject rightCanvas;

    public int correctMatches = 0;
    public int currentLevel = 0;
    public int[] levelTarget;
    public GameObject[] cardCollections;

    GameObject instMatching, instSorting;
    public GameObject instructionPanel;
    public GameObject prompt1;
    public static bool canPick = true;
    public GameObject leftCat;
    public GameObject rightCat;
    int timer;

    void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
    }
    void Start()
    {
        if (sceneManager != null)
        {
            StartCoroutine("ShowInstructions");
            //  Debug.Log("focus side " + sceneManager.GetComponent<SceneManager>().focusSide);
            if (SceneManager.focusSide == 0)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                rightCat.SetActive(false);
                leftCat.SetActive(true);
                leftCanvas.SetActive(true);
                rightCanvas.SetActive(false);
                //  focusText.text = "Focus Side: \n Left Arm";
            }
            if (SceneManager.focusSide == 1)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 2;
                leftCanvas.SetActive(false);
                rightCat.SetActive(true);
                leftCat.SetActive(false);
                rightCanvas.SetActive(true);
                // focusText.text = "Focus Side: \n Right Arm";
            }
            else
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                leftCanvas.SetActive(true);
                rightCanvas.SetActive(false);
                //   focusText.text = "Focus Side: \n Left Arm";
            }

            sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>().avatarControllers[0] = avatarController.GetComponent<AvatarController>();
            calibManager.GetComponent<CalibrationManager>().kinectManager = sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>();
        }
        timer = 0;
        prompt1.SetActive(false);

        for (int i = 0; i < cardCollections.Length; i++)
        {
            cardCollections[i].SetActive(false);
        }
        cardCollections[0].SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        //  Debug.Log(CardManager.canPick);
       // Debug.Log("Correct Matches " + correctMatches);
        if (correctMatches >= levelTarget[currentLevel] || Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }

    public void CorrectMatch()
    {
        scoreManager.GetComponent<ScoreManager>().IncrementScore();
        correctMatches++;
    }


    IEnumerator ShowPrompt()
    {
        canPick = false;
        prompt1.SetActive(true);
        yield return new WaitForSeconds(3f);
        prompt1.SetActive(false);
        canPick = true;
        yield return null;
    }
    public void NextLevel()
    {
        // timer++;
        //    Debug.Log(timer);
        /*
        switch(currentLevel)
        {
            case 0:
                Level1Manager.Instance.DestroyAllCards();
                break;
        }
        */
        StartCoroutine("ShowPrompt");
      //  if (timer > 60)
       // {
            Debug.Log("NEXTLEVEL");
            currentLevel++;
            cardCollections[currentLevel - 1].SetActive(false);

        if (currentLevel > 3)
        {
            scoreManager.GetComponent<ScoreManager>().ShowGrading();
        }
        else
        {
             
            float currentTimer = scoreManager.GetComponent<ScoreManager>().GetCurrentLevelTime();
            int currentScore = scoreManager.GetComponent<ScoreManager>().RetrieveScore();
            scoreManager.GetComponent<ScoreManager>().IncreaseLevel();
            if (!AlternateController.noKinect)
            {
            sceneManager.GetComponent<SceneManager>().UpdateLevelScore(0, currentLevel, currentScore);
            sceneManager.GetComponent<SceneManager>().UpdateCurrentLevelTime(0, currentLevel, currentTimer);
            sceneManager.GetComponent<SceneManager>().AddToTotalTime(currentTimer);
            
            }
        }
            cardCollections[currentLevel].SetActive(true);

            correctMatches = 0;

            timer = 0;
           // prompt1.SetActive(false);
        /*
            if (currentLevel == 3)
            {
                instMatching.SetActive(false);
                instSorting.SetActive(true);
            }
            else
            {
                instMatching.SetActive(true);
                instSorting.SetActive(false);
            }
            */
    //     }

    }

    IEnumerator ShowInstructions()
    {
        instructionPanel.SetActive(true);
        canPick = false;
        yield return new WaitForSeconds(10f);
        instructionPanel.SetActive(false);
        canPick = true;
       yield return null;
    }
}
