using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {

    public bool isPicking = false;
    private Vector3 tempPos;
    public GameObject levelManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(isPicking)
        {
            tempPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(tempPos.x, tempPos.y,0f);
        }
	
	}

    void OnMouseDown()
    {
        isPicking = true;
    }
    void OnMouseUp()
    {
        isPicking = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name);
        if(col.gameObject.name==gameObject.name)
        {
            levelManager.GetComponent<CardLevelManager>().CorrectMatch();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
