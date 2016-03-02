using UnityEngine;
using System.Collections;
using System.IO;

public class GetJointPositionTest : MonoBehaviour 
{
	[Tooltip("The Kinect joint we want to track.")]
	public KinectInterop.JointType rightHand = KinectInterop.JointType.HandRight;
    public KinectInterop.JointType spineMid = KinectInterop.JointType.SpineMid;

    [Tooltip("Current joint position in Kinect coordinates (meters).")]
	public Vector3 rightHandPos, spindMidPos;

    [Tooltip("Whether we save the joint data to a CSV file or not.")]
	public bool isSaving = false;

	[Tooltip("Path to the CSV file, we want to save the joint data to.")]
	public string saveFilePath = "joint_pos.csv";
	
	[Tooltip("How many seconds to save data to the CSV file, or 0 to save non-stop.")]
	public float secondsToSave = 0f;

	// start time of data saving to csv file
	private float saveStartTime = -1f;

    //
    public GameObject Level1;
    Level1Manager Lv1;

    public GameObject pointer, gPointer; 
    float fuzz = 0.5f;

    Vector3 mappedRHandPos;
    float stretchRH;

    bool[] Picking = new bool[8];

    int stayTimer = 0;
    int currState = 0;

    //Start
    void Start(){
        Lv1 = Level1.GetComponent<Level1Manager>();

        if (isSaving && File.Exists(saveFilePath)) File.Delete(saveFilePath);
	}

    //Update
	void Update () 
	{
        for (int i = 0; i < 8; i++) { Picking[i] = false; }

		if(isSaving)
		{
			// create the file, if needed
			if(!File.Exists(saveFilePath))
			{
				using(StreamWriter writer = File.CreateText(saveFilePath))
				{
					// csv file header
					string sLine = "time,joint,pos_x,pos_y,poz_z";
					writer.WriteLine(sLine);
				}
			}

			// check the start time
			if(saveStartTime < 0f)
			{
				saveStartTime = Time.time;
			}
		}

		// get the joint position
		KinectManager manager = KinectManager.Instance;

		if(manager && manager.IsInitialized()) {
			if(manager.IsUserDetected()) {
				long userId = manager.GetPrimaryUserID();

				if(manager.IsJointTracked(userId, (int)rightHand) && manager.IsJointTracked(userId, (int)spineMid))
				{
                    //////////////////////////////////////////////
					// output the joint position for easy tracking
					Vector3 jointPos = manager.GetJointPosition(userId, (int)rightHand);
                    rightHandPos = jointPos;

                    spindMidPos = manager.GetJointPosition(userId, (int)spineMid);
                    stretchRH = spindMidPos.z - rightHandPos.z;


                    if (isSaving) {
						if((secondsToSave == 0f) || ((Time.time - saveStartTime) <= secondsToSave)) {
							using(StreamWriter writer = File.AppendText(saveFilePath)) {
								string sLine = string.Format("{0:F3},{1},{2:F3},{3:F3},{4:F3}", Time.time, ((KinectInterop.JointType)rightHand).ToString(), jointPos.x, jointPos.y, jointPos.z);
								writer.WriteLine(sLine);
							}
						}
					}
				}
			}
		}

        gPointer.transform.position = new Vector3(spindMidPos.x, spindMidPos.y, -0.01f);

        CalibrateIt();

       // for (int i = 0; i < 4; i++)
       // {
         //   if (Lv1.Cards[i] != null)
          //  {
             //   if(currState == 0)
            MoveCard(2);
        // }
        // }


    } // End of Update

    void CalibrateIt()
    {
        Vector3 centerPos = new Vector3(0f, 1f, -0.01f);
        mappedRHandPos.x = rightHandPos.x * 10f;
        mappedRHandPos.y = (rightHandPos.y - 1f) * 6.3f;

        if (spindMidPos.x > centerPos.x - fuzz && spindMidPos.x < centerPos.x + fuzz &&
            spindMidPos.y > centerPos.y - fuzz && spindMidPos.y < centerPos.y + fuzz) {
            pointer.transform.position = new Vector3(mappedRHandPos.x, mappedRHandPos.y, -0.01f);
        }
    }

    void MoveCard(int whichCard){ // only when a user stretch his arm  
        //   Debug.Log("Grab!");

        if (Lv1.Cards[whichCard] == null)
        {
            //Debug.Log("Card is NOT THERE!!!!!!!");
            return;
        }

            if (stretchRH > 0.3f) {

                if (isInBoundary(mappedRHandPos, 2))
                {
                    stayTimer++;
                    Lv1.Cards[whichCard].transform.localScale = new Vector3(1.17f, 1.17f, 1.17f);
                if (stayTimer > 30)
                {
                    currState = 1; // grabbed & moving somthing
                    Picking[whichCard] = true;
                    Lv1.Cards[whichCard].transform.position = mappedRHandPos;
                }
                else { currState = 0;  Picking[whichCard] = false; }
                }
                else {
                stayTimer = 0;
                currState = 0;
                Picking[whichCard] = false;
                Lv1.Cards[whichCard].transform.localScale = new Vector3(1f, 1f, 1f);
                }
   
        } else {
                stayTimer = 0;
            currState = 0;
            Picking[whichCard] = false;
                Lv1.Cards[whichCard].transform.localScale = new Vector3(1f, 1f, 1f);
        }

       //if( Picking[whichCard] == true) Lv1.Cards[whichCard].transform.position = mappedRHandPos;
    }

    bool isInBoundary(Vector3 handPos, int whichCard) {
        if (handPos.x > Lv1.Cards[whichCard].transform.position.x - fuzz && handPos.x < Lv1.Cards[whichCard].transform.position.x + fuzz &&
            handPos.y > Lv1.Cards[whichCard].transform.position.y - fuzz && handPos.y < Lv1.Cards[whichCard].transform.position.y + fuzz) {
            return true;
        } else return false;
    }
            
}
