using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PatientDataManager : MonoBehaviour {

    public InputField patientName;
    public InputField patientAge;
    public Dropdown focusSide;

    public CSVReader csvReader;

    public List<string>patients;
    public int focusSideValue = 0; //0 is left, 1 is right
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SubmitDetails()
    {
        patients.Add(patientName.text);
        SceneManager.focusSide = focusSide.value;
        focusSideValue = focusSide.value;
        csvReader.CSVWrite(patientName.text,patientAge.text,focusSide.value); // 0 is Left, 1 is Right for focusSide.value 
        Application.LoadLevel(1);
    }
}
