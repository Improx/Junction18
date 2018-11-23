using UnityEngine;

public class Door : MonoBehaviour {
	
	public bool _isOpen = false;

    public bool IsOpen
    {
        get
        {
            return _isOpen;
        }

        set
        {
            _isOpen = value;
			Debug.Log(IsOpen);
        }
    }

	public void Toggle() {
		IsOpen = !IsOpen;
	}

}
