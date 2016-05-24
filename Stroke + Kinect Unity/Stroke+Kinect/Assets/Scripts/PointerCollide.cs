using UnityEngine;
using System.Collections;

public class PointerCollide : MonoBehaviour {

    public GameObject[] activeImg;
    public AudioSource source;
    public AudioClip[] orcClips;
    private int activeArc = 0;
	private OrchestraSegment orcSeg;
    private int bgNum = 0;
    public static float volumeVal = 0f;

	// Use this for initialization
	void Start () {

        string temp = gameObject.name;
      //  Debug.Log(temp.Substring(2));
        bgNum=int.Parse(temp.Substring(2));
        orcSeg = gameObject.GetComponent<OrchestraSegment> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
    }
    void OnMouseExit()
    {

       // Debug.Log(activeArc);
        activeImg[activeArc].SetActive(false);
    }


    void OnMouseOver()
    {
        
        activeImg[bgNum-1].SetActive(true);
    //    activeImg[bgNum - 1].GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, volumeVal);
        activeArc = bgNum-1;
       source.clip = orcClips[bgNum-1];
        if (!source.isPlaying)
           source.Play();
        orcSeg.allow = true;

        /*     switch(gameObject.name)
             {
             case "LeftArc":
                 activeImg [activeArc].SetActive (false);
                 activeImg [0].SetActive (true);
                 activeArc = 0;
                 source.clip = orcClips [0];
                 if (!source.isPlaying)
                     source.Play ();
                     orcSeg.allow = true;
                     break;
                 case "CentreArc":
                     activeImg[activeArc].SetActive(false);
                     activeImg[1].SetActive(true);
                     source.clip = orcClips[1];
                     activeArc = 1;
                     if(!source.isPlaying)
                         source.Play();
                     orcSeg.allow = true;
                     break;
                 case "RightArc":
                     activeImg[activeArc].SetActive(false);
                     activeImg[2].SetActive(true);
                     source.clip = orcClips[2];
                     activeArc = 2;
                     //Debug.Log("in right");
                     if (!source.isPlaying)
                         source.Play();
                     orcSeg.allow = true;
                     break;
             }
        */
    }
}
