using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Level2Manager : MonoBehaviour
{
    //Assign cards globally from card prefabs
    public GameObject RedPrefab, BluePrefab, GreenPrefab, YellowPrefab, Num1Prefab, Num3Prefab;
    private GameObject RedCard, BlueCard, GreenCard, YellowCard, NumCard1, NumCard3;

    public GameObject timerManager, scoreManager;

    //Initiate slots to place cards
    public Vector3[] slots;
    Vector3 initPos;
    //public GameObject[] Cards;
	public GameObject cardContainer;
    private GameObject[] tempArray;

    public List<GameObject> Cards = new List<GameObject>();
   

    float gapX, gapY;
	void OnDisable()
	{
		for (int i = 0; i<cardContainer.transform.childCount; i++) {
			Destroy (cardContainer.transform.GetChild (cardContainer.transform.childCount - 1));
		}
	}
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
			RedCard.transform.parent = cardContainer.transform;
            BlueCard = (GameObject)Instantiate(BluePrefab);
			BlueCard.transform.parent = cardContainer.transform;
            GreenCard = (GameObject)Instantiate(GreenPrefab);
			GreenCard.transform.parent = cardContainer.transform;
            YellowCard = (GameObject)Instantiate(YellowPrefab);
			YellowCard.transform.parent = cardContainer.transform;
            NumCard1 = (GameObject)Instantiate(Num1Prefab);
			NumCard1.transform.parent = cardContainer.transform;
            NumCard3 = (GameObject)Instantiate(Num3Prefab);
			NumCard3.transform.parent = cardContainer.transform;
        }

        // Cards = GameObject.FindGameObjectsWithTag("Lv1Card");
        tempArray = GameObject.FindGameObjectsWithTag("Lv2Card");

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
