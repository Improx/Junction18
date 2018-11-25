using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour {

	public SpriteRenderer TreasureSprite;

	void OnTriggerEnter2D(Collider2D other)
    {
        
        var player = other.attachedRigidbody.GetComponent<Player>();

        if (!player) return;

        if (player.Team == PlayerType.Guard) return;

        if (player.isLocalPlayer) {
            if (player.GetComponent<CollectItems>().currentItem) {
                player.ScoreItem(player.GetComponent<CollectItems>().currentItem);
            }
        }
        // pickup with left click if object has item tag 
        if (other.CompareTag("Item")  && Input.GetButtonDown("Fire2"))
        {
			TreasureSprite = other.GetComponentInParent<SpriteRenderer>();
			TreasureSprite.enabled = !TreasureSprite.enabled;
			other.enabled = !other.enabled;
			GameManager.Instance.RobberPoints += 1;
        }
    }


}
