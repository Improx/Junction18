using Cinemachine;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public PlayerType Team;

    public CinemachineVirtualCamera RobberCameraPrefab;

	private Robber _robber;

    private void Start() {
        if (!isLocalPlayer) return;

        if (Team == PlayerType.Robber) {
            var mainCamera = FindObjectOfType<Camera>();

            var vmCam = Instantiate(RobberCameraPrefab, new Vector3(0, 0, -200), new Quaternion());
            var nightVision = vmCam.gameObject.AddComponent<DeferredNightVisionEffect>();
            nightVision.m_LightSensitivityMultiplier = 0.1f;

            vmCam.GetComponent<Camera>().orthographic = true;
            vmCam.Follow = transform;
            vmCam.m_Lens.OrthographicSize = 3;
            mainCamera.GetComponent<CinemachineBrain>().enabled = true;

			_robber = gameObject.AddComponent<Robber>();
        }

		else
		{
			//We are Guard

		}
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var otherPlayer = other.rigidbody.GetComponent<Player>();

        if (!otherPlayer) return;

        if (otherPlayer
            && otherPlayer.Team == PlayerType.Robber
            && Team == PlayerType.Guard) {
            GameManager.Instance.CmdCapture(this.gameObject, otherPlayer.gameObject);
        }
    }
    
    [ClientRpc]
    public void RpcGetCaptured()
    {
        var mover = GetComponent<PlayerMove>();
        mover.enabled = false;
    }

	private void Update()
	{
		if (isLocalPlayer && Team == PlayerType.Robber)
		{
			FlashScreenOverlay.SetAmount(_robber.FlashlightRadiance);
		}
	}
}