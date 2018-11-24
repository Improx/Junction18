using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField]
    public GameObject Target;
    public void OnTriggerEnter2D(Collider2D other) => Target.GetComponent<Door>().Toggle();

    public void OnTriggerExit2D(Collider2D other) => Target.GetComponent<Door>().Toggle();

}