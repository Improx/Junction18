using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour {

	public List<Robber> Robs;
	public AudioSource source;
	public AudioClip Alarm;
	void OnEnable () => FieldOfView.Detected += PlayEffects;
	void OnDisable () => FieldOfView.Detected -= PlayEffects;
	void PlayEffects (Robber r, string status) {
		if (status == "Add" && !Robs.Contains(r)) {
			source.PlayOneShot(Alarm);
			Robs.Add(r);
		}
		else if (status == "Remove" && Robs.Contains(r)) {
			Robs.Remove(r);
		}
	}
}