using UnityEngine;
using System.Collections;

public class NightVisionCamera : MonoBehaviour
{
	public bool bright = true;
	public float brightness = 2.0f;
	public Shader nightvisionShader;

	private Camera _camera;

	private void Start()
	{
		_camera = GetComponent<Camera>();
	}

	public void OnEnable()
	{
		nightvisionShader = Shader.Find("Custom/NightVision");
	}

	public void OnPreCull()
	{
		if (bright)
		{
			Shader.SetGlobalFloat("_Brightness", brightness);
			Shader.SetGlobalFloat("_Bright", 1.0f);
		}
		else
		{
			Shader.SetGlobalFloat("_Bright", 0.0f);
		}

		print("lol");
		_camera.SetReplacementShader(nightvisionShader, null);

			
	}
}