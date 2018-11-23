using UnityEngine;

public class Button : MonoBehaviour
{
    private bool _isTriggered = false;

    public bool IsTriggered
    {
        get
        {
            return _isTriggered;
        }

        set
        {
            _isTriggered = value;
            Debug.Log(IsTriggered);
        }
    }

    private void OnTriggerEnter(Collider other) => IsTriggered = true;

    private void OnTriggerExit(Collider other) => IsTriggered = false;

}