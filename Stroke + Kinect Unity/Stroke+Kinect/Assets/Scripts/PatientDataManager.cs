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
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SubmitDetails()
    {
        patients.Add(patientName.text);
        csvReader.CSVWrite(patientName.text,patientAge.text,focusSide.value); // 0 is Left, 1 is Right for focusSide.value 
        Application.LoadLevel(1);
    }
}
