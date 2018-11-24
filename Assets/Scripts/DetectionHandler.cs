using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour {

	public List<Robber> Robs;
	public GameObject Source;
	public AudioClip Alarm;
	public GameObject Exclamation;
	public SpriteRenderer ExPoint;
	void OnEnable () => FieldOfView.Detected += PlayEffects;
	void OnDisable () => FieldOfView.Detected -= PlayEffects;
	void PlayEffects (Robber r, string status) {
		if (status == "Add" && !Robs.Contains (r)) {
			Exclamation = r.transform.Find ("Exclamation").gameObject;
			ExPoint = Exclamation.GetComponent<SpriteRenderer> ();
			Source = GameObject.Find("Detecthandler");
			AudioSource ASource = Source.GetComponent<AudioSource>();
			ASource.Play();
			StartCoroutine (FlashPoint ());
			Robs.Add (r);
		} else if (status == "Remove" && Robs.Contains (r)) {
			Robs.Remove (r);
		}
	}

	public IEnumerator FlashPoint () {
		ExPoint.enabled = !ExPoint.enabled;
		yield return new WaitForSeconds (0.6f);
		ExPoint.enabled = !ExPoint.enabled;
	}
}