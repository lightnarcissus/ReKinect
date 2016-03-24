using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Level1Manager : MonoBehaviour
{
    //Assign cards globally from card prefabs
    public GameObject RedPrefab, BluePrefab, GreenPrefab, YellowPrefab;
    private GameObject RedCard, BlueCard, GreenCard, YellowCard;

    public GameObject timerManager, scoreManager;

    //Initiate slots to place cards
    public Vector3[] slots;
    Vector3 initPos;
    //public GameObject[] Cards;

        private GameObject[] tempArray;

    public List<GameObject> Cards = new List<GameObject>();
   

    float gapX, gapY;

    void OnEnable() {
       
        InitCards();
        InitSlots();
        
        Shuffle();

        timerManager.GetComponent<TimerText>().ResetTimer();
        scoreManager.GetComponent<ScoreManager>().ResetScore();

        for (int i = 0; i < Cards.Count; i++){
            Cards[i].transform.position = slots[i];
        }
    }

    void InitSlots()
    {
        slots = new Vector3[16];
        initPos = new Vector3(-3.2f, 2.8f, 0f);
        gapX = 2.2f;
        gapY = -1.8f;

        for (int i = 0; i < Cards.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i % 4 == j) slots[i] = new Vector3(initPos.x + gapX * j, initPos.y + gapY * (int)(i / 4), 0f);
            }
        }
    }

    void InitCards()
    {
        for (int i = 0; i < 2; i++)
        {
            RedCard = (GameObject)Instantiate(RedPrefab);
            BlueCard = (GameObject)Instantiate(BluePrefab);
            GreenCard = (GameObject)Instantiate(GreenPrefab);
            YellowCard = (GameObject)Instantiate(YellowPrefab);
        }

        // Cards = GameObject.FindGameObjectsWithTag("Lv1Card");
        tempArray = GameObject.FindGameObjectsWithTag("Lv1Card");

        for (int j = 0; j < tempArray.Length; j++) { 
        Cards.Add(tempArray[j]);
        }

    }

    void Shuffle() {
        for(int i = 0; i < Cards.Count; i++){
            int r = Random.Range(i, Cards.Count);
            GameObject tmp = Cards[i];
            Cards[i] = Cards[r];
            Cards[r] = tmp;
        }
    }

    void CheckDestroyedCards()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i] == null) {
                //Debug.Log("Destroyed Card Number is   " + i);
                Cards.Remove(Cards[i]);
            }
        }
    }

	void Update () {
        //Test shuffling
        if (Input.GetKeyDown("s")){
            Shuffle();
            for (int i = 0; i < Cards.Count; i++) {
               Cards[i].transform.position = slots[i];
            }
        }

        CheckDestroyedCards();

    }

}
