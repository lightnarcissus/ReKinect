﻿using UnityEngine;
using System.Collections;
using System.IO;

public class Lv4GamePlayer : MonoBehaviour {

    public KinectInterop.JointType rightHand = KinectInterop.JointType.HandRight;
    public KinectInterop.JointType spineMid = KinectInterop.JointType.SpineMid;

    public Vector3 rightHandPos, spindMidPos;

    //
    public GameObject Level4, CardLevelManager;
    Level4Manager Lv4; 
    CardLevelManager LvManager;

    public GameObject pointer, gPointer;
    float fuzz = 0.5f;

    Vector3 mappedRHandPos;
    float stretchRH;

    bool[] Picking;

    int stayTimer = 0;
    int currState = 0;

    int whichOneAmIMoving = -1;
    int whichCard = -1;
    int match = 0;

    int stayCounter = 0;

    // 
    void Start () {
        Lv4 = Level4.GetComponent<Level4Manager>();
        LvManager = CardLevelManager.GetComponent<CardLevelManager>();

        Picking = new bool[Lv4.Cards.Length];
    }
	
	// 
	void Update () {
        //for (int i = 0; i < 8; i++) { Picking[i] = false; }

        // get the joint position
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized()){
            if (manager.IsUserDetected()){
                long userId = manager.GetPrimaryUserID();

                if (manager.IsJointTracked(userId, (int)rightHand) && manager.IsJointTracked(userId, (int)spineMid)){
                    //////////////////////////////////////////////
                    // output the joint position for easy tracking
                    Vector3 jointPos = manager.GetJointPosition(userId, (int)rightHand);
                    rightHandPos = jointPos;

                    spindMidPos = manager.GetJointPosition(userId, (int)spineMid);
                    stretchRH = spindMidPos.z - rightHandPos.z;
                }
            }
        }

        gPointer.transform.position = new Vector3(spindMidPos.x, spindMidPos.y, -0.01f);

        CalibrateIt();

        //
        testFunc();


    } // End of Update

    void testFunc(){

        if (whichCard == -1)
        {
            for (int j = 0; j < Lv4.Cards.Length; j++){
                if (Lv4.Cards[j] != null) Picking[j] = false;
            }
        }

        for (int i = 0; i < Lv4.Cards.Length; i++) {

            if (Lv4.Cards[i] != null)
            {
                isInBoundary(mappedRHandPos, i);
                if (isInBoundary(mappedRHandPos, i)) whichCard = i;

                if (Picking[i]) Lv4.Cards[i].transform.localScale = new Vector3(1f, 1f, 1f); //1.17
                else { Lv4.Cards[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f); } //1
            }
     
        }

       // if(!isInBoundary(mappedRHandPos, 0) && !isInBoundary(mappedRHandPos, 1) && !isInBoundary(mappedRHandPos, 2) 
       //    && !isInBoundary(mappedRHandPos, 3) && !isInBoundary(mappedRHandPos, 4) && !isInBoundary(mappedRHandPos, 5) 
       //    && !isInBoundary(mappedRHandPos, 6) && !isInBoundary(mappedRHandPos, 7)) {
       //     whichCard = -1;
       // }

        if (whichCard != -1) {
            for (int j = 0; j < Lv4.Cards.Length; j++) {
                if (Lv4.Cards[j] != null) Picking[j] = false;
            }
            Picking[whichCard] = true;

            if (stretchRH > 0.3f)
            {
                stayCounter++;

                //Debug.Log(LvManager.correctMatches);
                if (Lv4.Cards[whichCard] == null)
                {
                    Debug.Log("NOOOOOOOOOOO0000OOOOOOOOO");
                    whichCard = -1;
                    stayCounter = 0;
                }
                else { if (stayCounter > 30) Lv4.Cards[whichCard].transform.position = mappedRHandPos; }
            }
            else {
                whichCard = -1;
                stayCounter = 0;
            }
        }
    }

    void CalibrateIt(){
        Vector3 centerPos = new Vector3(0f, 1f, -0.01f);
        mappedRHandPos.x = rightHandPos.x * 10f;
        mappedRHandPos.y = (rightHandPos.y - 1f) * 6.3f;

        if (spindMidPos.x > centerPos.x - fuzz && spindMidPos.x < centerPos.x + fuzz &&
            spindMidPos.y > centerPos.y - fuzz && spindMidPos.y < centerPos.y + fuzz) {
            pointer.transform.position = new Vector3(mappedRHandPos.x, mappedRHandPos.y, -0.01f);
        }
    }

    bool isInBoundary(Vector3 handPos, int whichCard){
        if (handPos.x > Lv4.Cards[whichCard].transform.position.x - fuzz && handPos.x < Lv4.Cards[whichCard].transform.position.x + fuzz &&
            handPos.y > Lv4.Cards[whichCard].transform.position.y - fuzz && handPos.y < Lv4.Cards[whichCard].transform.position.y + fuzz) {
            return true;
        }
        else return false;
    }
}

