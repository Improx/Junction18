using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour {

	public GameManager GManager;
	public int points;
	public SpriteRenderer TreasureSprite;

	void OnTriggerStay2D(Collider2D other)
    {
        // pickup with left click if object has item tag 
        if (other.CompareTag("Item")  && Input.GetButtonDown("Fire2"))
        {
			TreasureSprite = other.GetComponentInParent<SpriteRenderer>();
			TreasureSprite.enabled = !TreasureSprite.enabled;
			other.enabled = !other.enabled;
			GManager.RobberPoints += 1;
        }
    }

    private void Update() => Debug.Log(GManager.RobberPoints);

}
