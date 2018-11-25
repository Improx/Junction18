using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTutorial : MonoBehaviour {

  public GameObject tutorialPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey){
      tutorialPanel.SetActive(false);
    }
	}

  public void OpenTutorial() {
    tutorialPanel.SetActive(true);
  }
}
