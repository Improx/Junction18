using UnityEngine;

public class CollectItems : MonoBehaviour
{
    public Transform carryLocation; // empty gameobject as a child of player, object will be carried on this position
    public Item currentItem = null;

    void Update()
    {
        if (!GetComponent<Player>().isLocalPlayer) return;

        // right click to drop item
        if (Input.GetButtonDown("Fire2"))
        {
            if (currentItem!=null)
            {
                GetComponent<Player>().UnGrab(currentItem);
                currentItem = null;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!GetComponent<Player>().isLocalPlayer) return;
        
        // pickup with left click if object has item tag 
        if (other.CompareTag("Item") && currentItem == null && Input.GetButtonDown("Fire1"))
        {
            GetComponent<Player>().GrabItem(other.GetComponent<Item>());
            currentItem = other.GetComponent<Item>();
        }
    }
}
