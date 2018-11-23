using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField]
    public GameObject Target;
    public void OnTriggerEnter(Collider other) => Target.GetComponent<Door>().Toggle();

    public void OnTriggerExit(Collider other) => Target.GetComponent<Door>().Toggle();

}