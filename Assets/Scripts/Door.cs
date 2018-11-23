using UnityEngine;

public class Door : MonoBehaviour {
	
	Collider d_Collider;

	public bool _isOpen = false;
	private void Start() {
		d_Collider = GetComponent<Collider>();
	}

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
		d_Collider.enabled = !d_Collider.enabled;		

	}

}
