using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MalpositionManager : MonoBehaviour {

    public enum MalState { ShoulderShrug,Contraction,InnerRotation,WristDrop};
    public static bool malPosActive = false;
    public GameObject drawingManager;
    private AvatarController avatarController;
    public MalState malPosState;
    private int shrugCount = 0;
    private int contractionCount = 0;
    public int malpositionVal = 0;
    public GameObject leftShoulderShrugWarning;
    public GameObject rightShoulderShrugWarning;
    private int shoulderShrugCount = 0;
    private int poorBalanceCount = 0;
    private int flexionSynergyCount = 0;
    private int innerRotationCount = 0;
    private int extensorSynergyCount = 0;
    private int wristDropCount = 0;
    public GameObject leftContractionWarning;
    public GameObject rightContractionWarning;

    public GameObject contractionText;
    public GameObject straightenText;

    public GameObject balanceWarning;
    public GameObject balanceText;

    public Text debug1;
    public Text debug2;
    private Vector2 leftHandOffset = new Vector2(-0.3f,0.7f);
    private Vector2 rightHandOffset = new Vector2(0.2f, 0.7f);
    private Vector2 leftLegOffset = new Vector2(-0.1f, 0.2f);
    private Vector2 rightLegOffset = new Vector2(0.1f, 0.2f);

	//SINGLETON
	private static MalpositionManager _instance;

	public static MalpositionManager Instance
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

        if (GameManager.activeApp == 1)
            avatarController = drawingManager.GetComponent<DrawingManager>().avatarController.GetComponent<AvatarController>();
        else if (GameManager.activeApp == 2)
            avatarController = drawingManager.GetComponent<CardManager>().avatarController.GetComponent<AvatarController>();
        else if (GameManager.activeApp == 3)
            avatarController = drawingManager.GetComponent<ConductorManager>().avatarController.GetComponent<AvatarController>();
        else if (GameManager.activeApp == 4)
        {

            avatarController = drawingManager.GetComponent<TuningForkManager>().avatarController.GetComponent<AvatarController>();
        }
		if (GameManager.activeApp != 4) {
			InvokeRepeating ("CheckMalPositions", 1f, 1f);

			straightenText.SetActive (false);
			contractionText.SetActive (false);
			leftShoulderShrugWarning.SetActive (false);
			rightShoulderShrugWarning.SetActive (false);
			leftContractionWarning.SetActive (false);
			rightContractionWarning.SetActive (false);
		}
    }
	
	// Update is called once per frame
	void Update () {

        switch(malPosState)
        {
            case MalState.ShoulderShrug:
                break;
            case MalState.Contraction:
                break;
            case MalState.InnerRotation:
                break;
            case MalState.WristDrop:
                break;
        }
	
	}

    public void SetTuningTask(int tuningForkTask)
    {
        switch (tuningForkTask)
        {
            //straight down
            case 0:
                leftHandOffset = new Vector2(-0.3f, 0.7f);
                rightHandOffset = new Vector2(0.2f, 0.7f);
                leftLegOffset = new Vector2(-0.1f, 0.2f);
                rightLegOffset = new Vector2(0.1f, 0.2f);
                break;
              //left on the side
            case 1:
                leftHandOffset = new Vector2(-0.7f, 0.7f);
                break;
                //right on the side
            case 2:
                rightHandOffset = new Vector2(0.7f, 0.7f);
                break;
                //left up top
            case 3:
                leftHandOffset = new Vector2(-0.3f, 0.7f);
                break;
                //right up top
            case 4:
                rightHandOffset = new Vector2(-0.3f, 0.7f);
                break;
                //both up top
            case 5:
                break;
        }
    }
	public IEnumerator CheckMalPosValue()
	{
		while (GameManager.tuningFork) {

         
            float tempVal = 0f;
           // Debug.Log("tuning fork");
                    //left hand
             //       Vector2 leftHand = new Vector2(avatarController.handLeftPos.x, avatarController.handLeftPos.z);
               //     float leftHandAngle = Vector2.Angle(leftHand, leftHandOffset);
                    Debug.Log("the hand pos is : " + avatarController.handLeftPos +" and the offset is" + leftHandOffset);
                    float leftHandAngle =Mathf.Abs(avatarController.handLeftPos.x - leftHandOffset.x);
                  //  Debug.Log(leftHandAngle);
                    if (leftHandAngle>0.2f)
                        tempVal = Mathf.Abs(0.2f - leftHandAngle);

                    //right hand
                    //  Vector2 rightHand = new Vector2(avatarController.handRightPos.x, avatarController.handRightPos.z);
                    //   float rightHandAngle = Vector2.Angle(rightHand, rightHandOffset);
                   // Debug.Log("the hand pos is : " + rightHand + " and the offset is" + rightHandOffset);
                   // Debug.Log("the hand pos is : " + avatarController.handRightPos + " and the offset is" + rightHandOffset);
                    float rightHandAngle = Mathf.Abs(avatarController.handRightPos.x - rightHandOffset.x);
                 //   Debug.Log(rightHandAngle);
                    if (rightHandAngle >0.2f)
                        tempVal = Mathf.Abs(0.2f - rightHandAngle);
                    /*
                                        // left feet
                                        Vector2 leftFeet = new Vector2(avatarController.legLeftPos.x, avatarController.legLeftPos.y);
                                        float leftFeetAngle = Vector2.Angle(leftFeet, leftHandOffset);
                                       // Debug.Log("the feet pos is : " + leftFeet + " and the offset is" + leftLegOffset);
                                        if (leftFeetAngle > 12f)
                                            tempVal += Mathf.Abs(12f - leftFeetAngle);


                                        //right feet
                                        Vector2 rightFeet = new Vector2(avatarController.legRightPos.x, avatarController.legRightPos.y);
                                        float rightFeetAngle = Vector2.Angle(rightFeet, rightHandOffset);
                                      //  Debug.Log("the feet pos is : " + rightFeet + " and the offset is" + rightLegOffset);
                                        if (rightFeetAngle > 12f)
                                            tempVal += Mathf.Abs(12f - rightFeetAngle);
                                            */
                    //head
                    Vector2 headPos = new Vector2(avatarController.headPos.x, 0f);
                    float headAngle = headPos.x;
                    //Debug.Log(headAngle);
                    //Debug.Log("the head pos is : " + avatarController.headPos + " and the offset is" + Vector3.zero);
                    if (headAngle < -0.2f)
                        tempVal += Mathf.Abs(0.2f - headAngle);
                    UnityEngine.Debug.Log("the temp val is: " + tempVal);

                    //final calculation
                    TuningForkManager.malPosFactor = tempVal;
                    tempVal = 0f;
			yield return 0;
		}

		yield return null;
	}


    public int RetrievePoorBalanceCount()
    {
        return poorBalanceCount;
    }


    public int RetrieveExtensorSynergyCount()
    {
        return extensorSynergyCount;
    }


    public int RetrieveInnerRotationCount()
    {
        return innerRotationCount;
    }


    public int RetrieveWristDropCount()
    {
        return wristDropCount;
    }


    public int RetrieveFlexionSynergyCount()
    {
        return flexionSynergyCount;
    }

    public int RetrieveShoulderShrugCount()
    {
        return shoulderShrugCount;
    }

    //called on every level
    public void ResetMalpositionCount()
    {
        shoulderShrugCount = 0;
        poorBalanceCount = 0;
        innerRotationCount = 0;
        wristDropCount = 0;
        flexionSynergyCount = 0;
        extensorSynergyCount = 0;
    }
    void CheckMalPositions()
    {
        //  debug1.text = "LEFT: " + avatarController.shoulderLeftPos.y.ToString();
        //  debug2.text = "RIGHT: " + avatarController.shoulderRightPos.y.ToString();

        //shoulder shrug
        if (!AlternateController.noKinect)
        {
            if (avatarController.outOfBalance)
            {
                balanceWarning.SetActive(true);
                balanceText.SetActive(true);
            }
            else
            {
                balanceWarning.SetActive(false);
                balanceText.SetActive(false);
            }
            if (Mathf.Abs(avatarController.shoulderLeftPos.y - avatarController.shoulderRightPos.y) > 0.05f)
            {

                //avatarController.outOfBalance = true;
                shoulderShrugCount++;
                malPosState = MalState.ShoulderShrug;
                malPosActive = true;
                Debug.Log("SHOULDER SHRUG");
                if (shrugCount < 4)
                    shrugCount++;
                else
                {
                    shrugCount--;
                    straightenText.SetActive(true);
                    if (avatarController.shoulderLeftPos.y > avatarController.shoulderRightPos.y)
                    {

                        leftShoulderShrugWarning.SetActive(true);
                        rightShoulderShrugWarning.SetActive(false);
                    }
                    else
                    {
                        leftShoulderShrugWarning.SetActive(false);
                        rightShoulderShrugWarning.SetActive(true);
                    }

                    Debug.Log("BAD POSITION");
                    // shrugCount = 0;
                    //display correction warning
                    malpositionVal += 5;
                }
                //StartCoroutine("StartShrugTimer");
            }
            else
            {
                straightenText.SetActive(false);
                rightShoulderShrugWarning.SetActive(false);
                leftShoulderShrugWarning.SetActive(false);
                shrugCount = 0;
            }

            //contraction
            //  debug1.text = Vector3.Angle(avatarController.handLeftPos, avatarController.shoulderLeftPos).ToString();
            //   debug2.text = Vector3.Angle(avatarController.handRightPos, avatarController.shoulderRightPos).ToString();
            if (SceneManager.focusSide == 1)
            {
                if (Vector3.Angle(avatarController.handLeftPos, avatarController.shoulderLeftPos) < 6f)
                {
                    avatarController.outOfBalance = true;
                    leftContractionWarning.SetActive(true);
                    if (contractionCount <= 4)
                    {
                        contractionCount++;
                    }
                    else
                    {
                        contractionCount = 0;
                        malPosState = MalState.Contraction;
                        malPosActive = true;
                        malpositionVal += 5;
                    }
                    contractionText.SetActive(true);
                }
                else
                {
                    contractionText.SetActive(false);
                    contractionCount = 0;
                    leftContractionWarning.SetActive(false);
                }

            }
            else if (SceneManager.focusSide == 0)
            {
                if (Vector3.Angle(avatarController.handRightPos, avatarController.shoulderRightPos) < 6f)
                {
                    avatarController.outOfBalance = true;
                    rightContractionWarning.SetActive(true);
                    if (contractionCount <= 4)
                    {
                        contractionCount++;
                    }
                    else
                    {
                        contractionCount = 0;
                        malPosState = MalState.Contraction;
                        malPosActive = true;
                        malpositionVal += 5;
                    }
                    contractionText.SetActive(true);
                }
                else
                {
                    contractionText.SetActive(false);
                    rightContractionWarning.SetActive(false);
                    contractionCount = 0;
                }
            }

            // WRIST DROP
            if(SceneManager.focusSide==0)
            {
                if(Vector3.Distance(avatarController.thumbLeftPos,avatarController.handLeftPos) > 1f)
                {

                }
            }
            else if(SceneManager.focusSide==1)
            {
                if (Vector3.Distance(avatarController.thumbRightPos, avatarController.handRightPos) > 1f)
                {

                }
            }
        }
    }
}
