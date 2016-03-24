using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Level4Manager : MonoBehaviour
{

    Vector3[] slots;
    Vector3 initPos;
    public GameObject[] Cards;
    public GameObject timerManager, scoreManager;

    float gapX, gapY;

    void OnEnable()
    {
       // Cards = GameObject.FindGameObjectsWithTag("Lv4Card");
        Debug.Log("Card deck size: " + Cards.Length);

        InitSlots();
        Shuffle();

        timerManager.GetComponent<TimerText> ().ResetTimer();
        scoreManager.GetComponent<ScoreManager>().ResetScore();

        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].transform.position = slots[i];
            Cards[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
    }

    void InitSlots()
    {
        slots = new Vector3[9];
        initPos = new Vector3(-3.2f, 2.8f, 0f);
        gapX = 1.8f;
        gapY = -1.5f;


        for (int i = 0; i < Cards.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i % 3 == j) slots[i] = new Vector3(initPos.x + gapX * j, initPos.y + gapY * (int)(i / 3), 0f);
            }
        }
    }

    void Shuffle()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            int r = Random.Range(i, Cards.Length);
            GameObject tmp = Cards[i];
            Cards[i] = Cards[r];
            Cards[r] = tmp;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            Shuffle();

            for (int i = 0; i < Cards.Length; i++)
            {
                Cards[i].transform.position = slots[i];
            }

        }

        if (timerManager.GetComponent<TimerText>().timer > 20f) showTimeOverPrompt();
    }

    void showTimeOverPrompt()
    {

    }
}
