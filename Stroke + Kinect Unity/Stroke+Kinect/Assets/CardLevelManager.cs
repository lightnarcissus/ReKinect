using UnityEngine;
using System.Collections;

public class CardLevelManager : MonoBehaviour {

    public int correctMatches = 0;
    public int currentLevel = 0;
    public int[] levelTarget;
    public GameObject[] cardCollections;
	// Use this for initialization
	void Start () {

        for(int i=0;i<cardCollections.Length;i++)
        {
            cardCollections[i].SetActive(false);
        }
        cardCollections[0].SetActive(true);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void CorrectMatch()
    {
        correctMatches++;
        if(correctMatches>=levelTarget[currentLevel])
        {
            UpdateCurrentLevel();
        }
    }
    public void UpdateCurrentLevel()
    {
        currentLevel++;
        correctMatches = 0;
        cardCollections[currentLevel-1].SetActive(false);
        cardCollections[currentLevel].SetActive(true);

    }
}
