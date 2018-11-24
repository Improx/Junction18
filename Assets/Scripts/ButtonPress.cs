using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class ButtonPress : MonoBehaviour {

  public Sprite notPressedSprite;
  public Sprite pressedSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private void OnCollisionEnter2D(Collision2D other) {
    GetComponent<SpriteRenderer>().sprite = pressedSprite;
  }

  private void OnCollisionExit2D(Collision2D other) {
    GetComponent<SpriteRenderer>().sprite = notPressedSprite;
  }
}
