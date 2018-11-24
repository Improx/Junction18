using Cinemachine;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public PlayerType Team;

    public CinemachineVirtualCamera RobberCameraPrefab;

    private void Start() {
        if (!isLocalPlayer) return;

        if (Team == PlayerType.Robber) {
            var mainCamera = FindObjectOfType<Camera>();

            var vmCam = Instantiate(RobberCameraPrefab, new Vector3(0, 0, -200), new Quaternion());
            var nightVision = vmCam.gameObject.AddComponent<DeferredNightVisionEffect>();
            nightVision.m_LightSensitivityMultiplier = 0;

            vmCam.GetComponent<Camera>().orthographic = true;
            vmCam.Follow = transform;
            vmCam.m_Lens.OrthographicSize = 3;
            mainCamera.GetComponent<CinemachineBrain>().enabled = true;

        }

		else
		{
			//We are Guard

		}
    }
}