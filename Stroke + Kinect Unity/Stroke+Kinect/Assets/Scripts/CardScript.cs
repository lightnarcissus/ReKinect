using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {

    public bool isPicking = false;
    private Vector3 tempPos;
    GameObject levelManager, scoreManager;
    public GameObject effect;

//    private int Lv123score = 0;
//    public Text scoreText;

    void Start () {
        levelManager = GameObject.Find("CardManager");
        scoreManager = GameObject.Find("ScoreManager");
    }
	
	void Update () {
        if(isPicking) {
           // tempPos = Input.mousePosition;
           tempPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(tempPos.x, tempPos.y, 0f);
        }
	}

    void OnMouseDown() {
        isPicking = true;
        GameObject tempObj=Instantiate(effect, transform.position, Quaternion.identity) as GameObject;
        tempObj.transform.parent = transform; 
    }

    void OnMouseUp() {
     //   isPicking = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.GetInstanceID());
        Debug.Log("COL " + col.gameObject.name + "AND MINE " + gameObject.name);
        if (col.gameObject.name == gameObject.name)
        {
            levelManager.GetComponent<CardManager>().CorrectMatch();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}

