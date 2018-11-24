using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public GameObject PlayerModel;
	public bool MouseAim;

    void Update()
    {
        if (!isLocalPlayer) {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //transform.Rotate(0, x, 0);
        transform.Translate(x, 0, z);

        if (MouseAim) {
            var ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            var plane=new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (plane.Raycast(ray, out distance)) {
                var target = ray.GetPoint(distance);
                var direction = target - transform.position;
                var rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                PlayerModel.transform.rotation = Quaternion.Euler(0, rotation, 0);
            }
        } else {
            var rotate = Input.GetAxis("LookHorizontal") * Time.deltaTime * 150.0f;
            PlayerModel.transform.Rotate(0, rotate, z);
        }

        if (Input.GetButtonDown("ToggleMouse")) {
            MouseAim = !MouseAim;
        }
    }
}