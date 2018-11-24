using UnityEngine;

public class Door : MonoBehaviour {
	
	Collider2D d_Collider;
	SpriteRenderer d_Sprite;
	private void Start() {
		d_Collider = GetComponent<Collider2D>();
		d_Sprite = GetComponent<SpriteRenderer>();
	}

	public void Toggle() {
		d_Collider.enabled = !d_Collider.enabled;
		d_Sprite.enabled = !d_Sprite.enabled;	
		//TODO: Add sound effect (& animation?)
	}

}
