using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class PlaceCardRandomly : MonoBehaviour
{
    //
    public GameObject RedPrefab, BluePrefab, GreenPrefab, YellowPrefab;
    private GameObject RedCard, BlueCard, GreenCard, YellowCard;
    //

    Vector3[] slots;
    Vector3 initPos;
    public GameObject[] Cards;

    float gapX, gapY;

    void OnEnable() {
       
        InitCards();
        InitSlots();
        
        Shuffle();

        for (int i = 0; i < Cards.Length; i++){
            Cards[i].transform.position = slots[i];
        }
    }

    void InitSlots()
    {
        slots = new Vector3[16];
        initPos = new Vector3(-3.2f, 2.8f, 0f);
        gapX = 2.2f;
        gapY = -1.8f;

        for (int i = 0; i < Cards.Length; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i % 4 == j) slots[i] = new Vector3(initPos.x + gapX * j, initPos.y + gapY * (int)(i / 4), 0f);
            }
        }
    }

    void InitCards()
    {
        for (int i = 0; i < 2; i++){
            RedCard = (GameObject)Instantiate(RedPrefab);
            BlueCard = (GameObject)Instantiate(BluePrefab);
            GreenCard = (GameObject)Instantiate(GreenPrefab);
            YellowCard = (GameObject)Instantiate(YellowPrefab);
        }

        Cards = GameObject.FindGameObjectsWithTag("Lv1Card");

    }

    void Shuffle() {
        for(int i = 0; i < Cards.Length; i++){
            int r = Random.Range(i, Cards.Length);
            GameObject tmp = Cards[i];
            Cards[i] = Cards[r];
            Cards[r] = tmp;
        }
    }

	void Update () {
        if (Input.GetKeyDown("s")){
            Shuffle();

            for (int i = 0; i < Cards.Length; i++) {
               Cards[i].transform.position = slots[i];
            }

        }
    }
}
