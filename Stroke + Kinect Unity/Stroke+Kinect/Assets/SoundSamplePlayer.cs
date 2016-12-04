using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SoundSamplePlayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public AudioSource aud;
    Button soundButton;
	// Use this for initialization
	void Start () {
        soundButton = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        Debug.Log("enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        aud.volume = 0f;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        aud.volume = 1f;
    }

    void OnMouseDown()
    {
        Debug.Log("DOWN");
    }

    void OnMouseOver()
    {
        Debug.Log("hi");
        aud.volume = 1f;
    }

    void OnMouseExit()
    {
        aud.volume = 0f;
    }
}
