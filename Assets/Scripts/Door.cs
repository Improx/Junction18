using UnityEngine;

public class Door : MonoBehaviour {
	
	Collider d_Collider;
	MeshRenderer d_Mesh;
	private void Start() {
		d_Collider = GetComponent<Collider>();
		d_Mesh = GetComponent<MeshRenderer>();
	}

	public void Toggle() {
		d_Collider.enabled = !d_Collider.enabled;
		d_Mesh.enabled = !d_Mesh.enabled;	
		//TODO: Add sound effect (& animation?)
	}

}
