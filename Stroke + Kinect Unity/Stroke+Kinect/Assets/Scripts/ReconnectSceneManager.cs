using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
public class ReconnectSceneManager : MonoBehaviour {

    public int appValue = 0;
    void Awake()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(DoSomething);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DoSomething()
    {
        SceneManager.Instance.ActivateApp(appValue);
    }
}
