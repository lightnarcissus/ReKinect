using UnityEngine;
using System.Collections;

public class CardLevelManager : MonoBehaviour {

    public int correctMatches = 0;
    public int currentLevel = 0;
    public int[] levelTarget;
    public GameObject[] cardCollections;

    public GameObject prompt1;

    public GameObject leftCanvas;
    public GameObject rightCanvas;
    GameObject instMatching, instSorting;

    int timer;

	void Start () {
        timer = 0;
        prompt1.SetActive(false);

        for (int i=0; i < cardCollections.Length; i++) {
            cardCollections[i].SetActive(false);
        }
        cardCollections[0].SetActive(true);

        instMatching = GameObject.Find("Inst_CardMatching");
        instSorting = GameObject.Find("Inst_CardSorting");

        instMatching.SetActive(true);
        instSorting.SetActive(false);

        if(SceneManager.focusSide==1)
        {
            leftCanvas.SetActive(true);
            rightCanvas.SetActive(false);
        }
        else
        {
            leftCanvas.SetActive(false);
            rightCanvas.SetActive(true);
        }
    }
	
	void Update () {

       // Debug.Log(correctMatches);

        if (correctMatches >= levelTarget[currentLevel]) {
            NextLevel();
        }


    }

    public void CorrectMatch() {
        correctMatches++;
    }


    public void NextLevel() {
        timer++;
        Debug.Log(timer);

        prompt1.SetActive(true);
        if (timer > 60) {
            currentLevel++;
            cardCollections[currentLevel - 1].SetActive(false);

            if (currentLevel > 3) currentLevel = 0;
            cardCollections[currentLevel].SetActive(true);

            correctMatches = 0;

            timer = 0;
            prompt1.SetActive(false);

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
        }

    }
}
