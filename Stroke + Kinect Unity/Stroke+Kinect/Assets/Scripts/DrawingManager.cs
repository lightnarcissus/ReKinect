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
    public int score = 0;
    public int scoreTarget = 15;

    public Text scoreText;
   // public Text timerText;
	// Use this for initialization
	void Start () {

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
        if(hitTarget!=nextTarget)
        {
			if(score>0)
            	score--;
        }
        else
        {
            score++;
        }

        if(score>scoreTarget)
        {
            UpdateLevel();
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateLevel()
    {

        //first disable the current targets

        targets[currentLevel].SetActive(false);
        //increment and then enable
        currentLevel++;
        targets[currentLevel].SetActive(true);

		currentLevelLimit=levelLimit [currentLevel];
        score = 0;
    }
    
}
