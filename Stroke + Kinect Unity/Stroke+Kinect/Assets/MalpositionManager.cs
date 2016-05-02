using UnityEngine;
using System.Collections;

public class MalpositionManager : MonoBehaviour {

    public enum MalState { ShoulderShrug,Contraction,InnerRotation,WristDrop};

    public DrawingManager drawingManager;
    private AvatarController avatarController;
    public MalState malPosState;
    private float shrugTimer = 0f;
	// Use this for initialization
	void Start () {
        avatarController=drawingManager.avatarController.GetComponent<AvatarController>();
        InvokeRepeating("CheckMalPositions", 1f, 1f);
	
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
        //shoulder shrug
        if(avatarController.shoulderLeftPos.y > avatarController.shoulderRightPos.y || avatarController.shoulderRightPos.y > avatarController.shoulderLeftPos.y)
        {

        }
    }
}
