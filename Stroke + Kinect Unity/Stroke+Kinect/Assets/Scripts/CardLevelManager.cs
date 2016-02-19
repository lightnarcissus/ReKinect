using UnityEngine;
using System.Collections;

public class CardLevelManager : MonoBehaviour {

    public int correctMatches = 0;
    public int currentLevel = 0;
    public int[] levelTarget;
    public GameObject[] cardCollections;

    public GameObject prompt1;
    int timer;

	void Start () {
        timer = 0;
        prompt1.SetActive(false);

        for (int i=0; i < cardCollections.Length; i++) {
            cardCollections[i].SetActive(false);
        }
        cardCollections[0].SetActive(true);
	}
	
	void Update () {

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

            if (currentLevel > 2) currentLevel = 0;
            cardCollections[currentLevel].SetActive(true);

            correctMatches = 0;

            timer = 0;
            prompt1.SetActive(false);
        }
    }
}
