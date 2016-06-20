using UnityEngine;
using System.Collections;

public class DestroyManagerScript : MonoBehaviour {

    public GameObject GreenCardPrefab,GreenCard, BlueCard;
    public GameObject[] Greens;
    // Use this for initialization

    void Start()
    {
        //Greens = new GameObject[5];

        //for (int i = 0; i < 2; i++)
        //{
        //    GreenCard = (GameObject)Instantiate(GreenCardPrefab, new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y + Random.Range(-6, 6), transform.position.z), Quaternion.identity);
        //    Greens[i] = GreenCard;
        //}
    }

    void OnEnable()
    {
        Greens = new GameObject[5];

        for (int i = 0; i < 2; i++)
        {
            GreenCard = (GameObject)Instantiate(GreenCardPrefab, new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y + Random.Range(-6, 6), transform.position.z), Quaternion.identity);
            Greens[i] = GreenCard;
        }
        //StartCoroutine("Respwan");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space")) {
            StartCoroutine("Respwan");
            //Destroy(GreenCard.gameObject);
        }
    }

    IEnumerator Respwan()
    {
        //Destroy(GreenCard.gameObject);
        Greens = new GameObject[5];

        for (int i = 0; i < 2; i++)
        {
            GreenCard = (GameObject)Instantiate(GreenCardPrefab, new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y + Random.Range(-6, 6), transform.position.z), Quaternion.identity);
            Greens[i] = GreenCard;
        }
         yield return null;
    }

    void OnDisable()
    {
        for (int i = 0; i < 2; i++)
        {
            Destroy(Greens[i].gameObject);
        }
    }

    
}
