using UnityEngine;
using System.Collections;

public class OrchestraSegment : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaySound()
    {
        GetComponent<PointerCollide>().PlaySegment(this.gameObject);
    }
}
