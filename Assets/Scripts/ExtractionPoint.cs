using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour {

	public int points;

	void OnTriggerStay2D(Collider2D other)
    {
        // pickup with left click if object has item tag 
        if (other.CompareTag("Item")  && Input.GetButtonDown("Fire2"))
        {
			other.enabled = !other.enabled;
			points += 1;
        }
    }

    private void Update() => Debug.Log(points);

}
