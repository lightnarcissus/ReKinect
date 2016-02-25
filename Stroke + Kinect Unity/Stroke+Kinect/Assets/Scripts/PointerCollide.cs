using UnityEngine;
using System.Collections;

public class PointerCollide : MonoBehaviour {

    public GameObject[] activeImg;
    public AudioSource source;
    public AudioClip[] orcClips;
    private int activeArc = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        switch(gameObject.name)
        {
            case "LeftArc":
                activeImg[activeArc].SetActive(false);
                activeImg[0].SetActive(true);
                activeArc = 0;
                source.clip = orcClips[0];
                if (!source.isPlaying)
                    source.Play();
                break;
            case "CentreArc":
                activeImg[activeArc].SetActive(false);
                activeImg[1].SetActive(true);
                source.clip = orcClips[1];
                activeArc = 1;
                if(!source.isPlaying)
                    source.Play();
                break;
            case "RightArc":
                activeImg[activeArc].SetActive(false);
                activeImg[2].SetActive(true);
                source.clip = orcClips[2];
                activeArc = 2;
                //Debug.Log("in right");
                if (!source.isPlaying)
                    source.Play();
                break;
        }
   
    }
}
