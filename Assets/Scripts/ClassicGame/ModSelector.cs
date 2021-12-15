using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenQuest(GameObject button)
    {
        PlayerPrefs.SetString("currentQuest", button.name);
        GameObject.Find("Main Camera").GetComponent<GenFunx>().SwitchScreen("QuestedGame");
    }
}
