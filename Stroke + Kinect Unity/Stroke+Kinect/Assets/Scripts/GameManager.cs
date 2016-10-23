using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public InteractionManager intManager;
	public GameObject gamePage;
    public GameObject levelPage;
    public GameObject focusSelectionPage;
	public GameObject titlePage;
	public GameObject musicPage;
    public AvatarController avatarCont;
	public AlternateController altControl;
    public SceneManager sceneManager;
    public bool focusSelected = false;
    public GUIText debugText;
    public Text confirmText;
    public Text chooseText;
	public GameObject cubeMan;
    private bool levelSelected = false;
	//SETTING THIS TO TRUE FOR NOW
	public static bool tuningFork=true;
    public static int activeApp = 0; // 1 is drawing and so on
    private bool firstTime = true;
	//SINGLETON
	private static GameManager _instance;

	public static GameManager Instance
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
			return;
		}
		_instance = this;

	}

	// Use this for initialization
	void Start () {
        if (sceneManager == null)
            sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        
		musicPage.SetActive (false);
		cubeMan.SetActive (false);
        levelPage.SetActive(false);
        focusSelectionPage.SetActive(false);
        gamePage.SetActive(false);
        titlePage.SetActive(true);
        StartCoroutine("CheckController");
	//	InvokeRepeating ("CheckController", 0.1f, 0.1f);


     
	}
	
	// Update is called once per frame
	void Update () {
        MouseControl.MouseMove(avatarCont.elbowPos,debugText);
       // Input.mousePosition = avatarCont.elbowPos;
       //        Debug.Log(intManager.userActive);


    }

	IEnumerator CheckController()
	{
      //  Debug.Log(AlternateController.noKinect);
		if (!AlternateController.noKinect) {
            UnityEngine.Debug.Log(intManager.userActive);
            while (!intManager.userActive && !focusSelected)
            {
                yield return 0;
            }
            if(firstTime)
            { 
				titlePage.SetActive (false);
                //do level selection
                Debug.Log("first time");
                yield return StartCoroutine("SelectLevel");

                //then bring up focus selection
             
                focusSelectionPage.SetActive(true);
                avatarCont.outOfBalance = true;
                yield return StartCoroutine("ActivateFocusSide");
                firstTime = false;
            }
            else
            {
                Debug.Log("NOT");
                focusSelected = true;
                titlePage.SetActive(false);
                focusSelectionPage.SetActive(false);
                gamePage.SetActive(true);
            }
            if (focusSelected) {
				focusSelectionPage.SetActive (false);
				gamePage.SetActive (true);
			}
		} else {
			titlePage.SetActive (false);
			gamePage.SetActive (true);
		}
        yield return null;
	}

    IEnumerator SelectLevel()
    {
        Debug.Log("selecting level");
        levelPage.SetActive(true);
        while(!levelSelected)
        {
            yield return 0;
        }
        levelPage.SetActive(false);
        yield return null;
    }
    void CheckStatus()
    {
       // Debug.Log("left" + intManager.leftHandPos.y);
        
      //  Debug.Log("right" + intManager.rightHandPos.y);
      if(SceneManager.focusSide==0)
        //if (intManager.leftHandPos.y > intManager.rightHandPos.y && Mathf.Abs(intManager.leftHandPos.y) > 0.5f)
        {
            avatarCont.activeJoint = 1;
            Debug.Log("left");
            focusSelectionPage.transform.GetChild(0).gameObject.SetActive(false);
            focusSelectionPage.transform.GetChild(1).gameObject.SetActive(true);
            focusSelectionPage.transform.GetChild(3).gameObject.SetActive(true);
            confirmText.gameObject.SetActive(true);
            confirmText.text = "CONFIRM IF LEFT ARM IS \n THE FOCUS SIDE";
            chooseText.gameObject.SetActive(false);
            StartCoroutine("ActivateFocusSide");

        }
        else if (SceneManager.focusSide==1)
      //  else if(intManager.rightHandPos.y > intManager.leftHandPos.y && Mathf.Abs(intManager.rightHandPos.y) > 0.5f)
        {
            avatarCont.activeJoint=2;
            Debug.Log("right");
            focusSelectionPage.transform.GetChild(0).gameObject.SetActive(false);
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(true);
            focusSelectionPage.transform.GetChild(3).gameObject.SetActive(true);
            confirmText.gameObject.SetActive(true);
            confirmText.text = "CONFIRM IF RIGHT ARM IS \n THE FOCUS SIDE";
            chooseText.gameObject.SetActive(false);
            StartCoroutine("ActivateFocusSide");
        }
        else
        {
            chooseText.gameObject.SetActive(true);
        }
    }

    IEnumerator ActivateFocusSide()
    {
        Debug.Log("activating left or right");
            float currentTime = 0f;
        while (currentTime < 5f)
        {

            if ((intManager.leftHandPos.y > intManager.rightHandPos.y && Config.difficultyLevel != 2) || ((intManager.leftHandPos.z > intManager.rightHandPos.z) && Config.difficultyLevel == 2))
            {
                // Debug.Log("activated left");
                //  focusSelected = true;
                SceneManager.focusSide = 0;
                focusSelectionPage.transform.GetChild(0).gameObject.SetActive(false);
                focusSelectionPage.transform.GetChild(1).gameObject.SetActive(true);
                focusSelectionPage.transform.GetChild(3).gameObject.SetActive(true);
                focusSelectionPage.transform.GetChild(2).gameObject.SetActive(false);
            }
            else if ((intManager.rightHandPos.y > intManager.leftHandPos.y && Config.difficultyLevel != 2) || ((intManager.rightHandPos.z > intManager.leftHandPos.z) && Config.difficultyLevel == 2))
            {
                //    Debug.Log("activated right");
                //  focusSelected = true;
                SceneManager.focusSide = 1;
                focusSelectionPage.transform.GetChild(0).gameObject.SetActive(false);
                focusSelectionPage.transform.GetChild(2).gameObject.SetActive(true);
                focusSelectionPage.transform.GetChild(3).gameObject.SetActive(true);
                focusSelectionPage.transform.GetChild(1).gameObject.SetActive(false);
            }
                currentTime += Time.deltaTime;
                yield return 0;
            }
            firstTime = false;

        //commenting out until Calibration Phase is implemented
            //  yield return StartCoroutine("RunCalibrationPhase");
        yield return StartCoroutine("ShowGameSelection");
        yield return null;
    }

    IEnumerator ShowGameSelection()
    {
        Debug.Log("can show game selection screen");
        titlePage.SetActive(false);
        focusSelectionPage.SetActive(false);
        gamePage.SetActive(true);
        focusSelected = true;
        yield return null;
    }

    IEnumerator RunCalibrationPhase()
    {
        yield return StartCoroutine("OutstretchArms");
        yield return StartCoroutine("RaiseThemUp");
        yield return StartCoroutine("LowerThemToYourSides");
        yield return null;
    }

    IEnumerator OutstretchArms()
    {
		cubeMan.SetActive (true);
        if(Mathf.Abs(Vector3.Angle(avatarCont.handLeftPos,avatarCont.handRightPos)) > 160f)
        {

        }
        yield return null;
    }

    IEnumerator RaiseThemUp()
    {
        yield return null;
    }

    IEnumerator LowerThemToYourSides()
    {
        yield return null;
    }

    IEnumerator ActivateLeft()
    {
        Debug.Log("activated left");
      //  yield return new WaitForSeconds(2f);
        if (intManager.leftHandPos.y > intManager.rightHandPos.y)
        {
            Debug.Log("OOOH NO");
            focusSelected = true;
            SceneManager.focusSide = 0;
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(false);
            titlePage.SetActive(false);
            focusSelectionPage.SetActive(false);
         //   gamePage.SetActive(true);
        }

            yield return null;
    }
    IEnumerator ActivateRight()
    {

        Debug.Log("activated right");
      //  yield return new WaitForSeconds(2f);
        if (intManager.rightHandPos.y > intManager.leftHandPos.y)
        {

            Debug.Log("OOOH NO");
            focusSelected = true;
            SceneManager.focusSide = 1;
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(false);
            titlePage.SetActive(false);
            focusSelectionPage.SetActive(false);
          //  gamePage.SetActive(true);
        }

            yield return null;
    }

    public void SelectDifficultyLevel(int level)
    {
        Debug.Log("clicked a button");
        switch(level)
        {
            case 0:
                //easy
                Config.difficultyLevel = 0;
                SetDifficultyLevel(0);
                levelSelected = true;
                break;
            case 1:
                //medium
                Config.difficultyLevel = 1;
                SetDifficultyLevel(1);
                levelSelected = true;
                break;
            case 2:
                //hard
                Config.difficultyLevel = 2;
                SetDifficultyLevel(2);
                levelSelected = true;
                break;

        }
    }
    
    void SetDifficultyLevel(int difficultyLevel)
    {
       switch(difficultyLevel)
        {
            case 0:
                Config.sensitivityMovement = 50;
                break;
            case 1:
                Config.sensitivityMovement = 100;
                break;
            case 2:
                Config.sensitivityMovement = 100;
                break;
        }
    }
}
