using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TestListScript : MonoBehaviour {

    public List <GameObject> Cardss;
    public GameObject RedCard, YellowCard, BlueCard;

    // Use this for initialization
    void Start () {
        Cardss = new List<GameObject>();
        Cardss.Add(RedCard);
        Cardss.Add(YellowCard);
        Cardss.Add(BlueCard);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space")) {
            Cardss.Remove(RedCard);
        }

        Debug.Log(Cardss.Count);
	}
}
