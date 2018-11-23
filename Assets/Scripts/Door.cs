using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	
	private bool _isOpen = false;

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

	void toggle() {
		IsOpen = !IsOpen;
	}

}
