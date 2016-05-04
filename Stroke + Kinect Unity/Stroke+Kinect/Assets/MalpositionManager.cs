using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MalpositionManager : MonoBehaviour {

    public enum MalState { ShoulderShrug,Contraction,InnerRotation,WristDrop};

    public GameObject drawingManager;
    private AvatarController avatarController;
    public MalState malPosState;
    private int shrugCount = 0;
    private int contractionCount = 0;
    public int malpositionVal = 0;
    public GameObject shoulderShrugWarning;
    public GameObject contractionWarning;
    public Text debug1;
    public Text debug2;
	// Use this for initialization
	void Start () {
       // if(SceneManager.currentApp==1)
            avatarController=drawingManager.GetComponent<DrawingManager>().avatarController.GetComponent<AvatarController>();
     //   else if(SceneManager.currentApp==3)
       //     avatarController = drawingManager.GetComponent<ConductorManager>().avatarController.GetComponent<AvatarController>();
        InvokeRepeating("CheckMalPositions", 1f, 1f);
        shoulderShrugWarning.SetActive(false);
        contractionWarning.SetActive(false);
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

    void CheckMalPositions()
    {
        //  debug1.text = "LEFT: " + avatarController.shoulderLeftPos.y.ToString();
        //  debug2.text = "RIGHT: " + avatarController.shoulderRightPos.y.ToString();
        //shoulder shrug
        if (Mathf.Abs(avatarController.shoulderLeftPos.y - avatarController.shoulderRightPos.y) > 0.08f)
        {
            avatarController.outOfBalance = true;
            Debug.Log("SHOULDER SHRUG");
            if (shrugCount < 4)
                shrugCount++;
            else
            {
                shrugCount--;
                shoulderShrugWarning.SetActive(true);
                Debug.Log("BAD POSITION");
                // shrugCount = 0;
                //display correction warning
                malpositionVal += 5;
            }
            //StartCoroutine("StartShrugTimer");
        }
        else
        {
            shoulderShrugWarning.SetActive(false);
            shrugCount = 0;
        }

        //contraction
        //  debug1.text = Vector3.Angle(avatarController.handLeftPos, avatarController.shoulderLeftPos).ToString();
        //   debug2.text = Vector3.Angle(avatarController.handRightPos, avatarController.shoulderRightPos).ToString();
        if (SceneManager.focusSide == 0)
        {
            if (Vector3.Angle(avatarController.handLeftPos, avatarController.shoulderLeftPos) < 6f)
            {
                avatarController.outOfBalance = true;
                contractionWarning.SetActive(true);
                if (contractionCount <= 4)
                {
                    contractionCount++;
                }
                else
                {
                    contractionCount = 0;
                    malpositionVal += 5;
                }
            }
            else
            {
                contractionCount = 0;
                contractionWarning.SetActive(false);
            }
        }
        else if (SceneManager.focusSide == 1)
        {
            if (Vector3.Angle(avatarController.handRightPos, avatarController.shoulderRightPos) < 6f)
            {
                avatarController.outOfBalance = true;
                contractionWarning.SetActive(true);
                if (contractionCount <= 4)
                {
                    contractionCount++;
                }
                else
                {
                    contractionCount = 0;
                    malpositionVal += 5;
                }
            }
            else
            {
                contractionWarning.SetActive(false);
                contractionCount = 0;
            }
        }
    }
}
