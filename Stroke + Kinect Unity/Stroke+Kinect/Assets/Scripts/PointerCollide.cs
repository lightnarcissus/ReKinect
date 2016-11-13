using UnityEngine;
using System.Collections;


public class PointerCollide : MonoBehaviour {

    public GameObject[] activeImg;
    private AudioSource source;
    public AudioClip[] orcClips;
    private int activeArc = 0;
	private OrchestraSegment orcSeg;
    private int bgNum = 0;
    public static float volumeVal = 0f;
    public float timeToSlowDown = 10f;
    public static bool canPlay = false;
	// Use this for initialization
	void Start () {

        source = GetComponent<AudioSource>();
        string temp = gameObject.name;
      //  Debug.Log(temp.Substring(2));
        bgNum=int.Parse(temp.Substring(2));
        orcSeg = gameObject.GetComponent<OrchestraSegment> ();
        source.clip = orcClips[bgNum - 1];
        source.Play();
        source.loop = true;
        source.volume = 0f;
    }
	
	// Update is called once per frame
	void Update () {

       // Debug.Log(gameObject.name + " has volume of " + source.volume);
	}

    void OnMouseEnter()
    {
    }
    void OnMouseExit()
    {

       // Debug.Log(activeArc);
        activeImg[activeArc].SetActive(false);
        StartCoroutine("DecreaseVolumeSlowly");
    }


    void OnMouseOver()
    {
        
        
        activeImg[bgNum-1].SetActive(true);
    //    activeImg[bgNum - 1].GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, volumeVal);
        activeArc = bgNum-1;
        source.volume = 1f;
       /*source.clip = orcClips[bgNum-1];
        if (!source.isPlaying)
           source.Play();
           */
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

    IEnumerator DecreaseVolumeSlowly()
    {
        while(source.volume>0)
        {
            source.volume -= 0.1f * ((1f/60f) * timeToSlowDown);
            yield return null;
        }   
        yield return null;
    }
}
