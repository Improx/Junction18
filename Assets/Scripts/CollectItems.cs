using UnityEngine;

public class CollectItems : MonoBehaviour
{
    public Transform carryLocation; // empty gameobject as a child of player, object will be carried on this position
    Transform currentItem = null;

    void Update()
    {
        // right click to drop item
        if (Input.GetButtonDown("Fire2"))
        {
            if (currentItem!=null)
            {
                currentItem.parent = null;

                currentItem.position = transform.GetComponent<SpriteRenderer>().bounds.max;
                currentItem = null;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // pickup with left click if object has item tag 
        if (other.CompareTag("Item") && currentItem == null && Input.GetButtonDown("Fire1"))
        {
            currentItem = other.transform;
            currentItem.position = carryLocation.position;
            currentItem.parent = transform;
        }
    }
}
