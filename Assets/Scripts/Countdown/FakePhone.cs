using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FakePhone : MonoBehaviour {

	AudioSource ring = null;
	bool stopvibrate = false;

	// Use this for initialization
	void Start () {
		try {
			ring = GameObject.Find("Ringtone").GetComponent<AudioSource>();
		} catch (Exception e) {
			Debug.Log (e.Message);
		}

		if (MetaData.ring && ring != null)
			ring.Play ();
		if (MetaData.vibrate)
			StartCoroutine (Vibration ());
	}

	IEnumerator Vibration()
	{
		if (MetaData.vibrate) {
			for (int i = 0; i < 30 && !stopvibrate; i++) {
				Handheld.Vibrate();
				yield return new WaitForSeconds(1);
			}
		}
	}

	public void Stop() {
		stopvibrate = true;
		if (ring != null)
			ring.Stop ();
        //SceneManager.LoadScene("Camera", LoadSceneMode.Single);
	}

	public void Accept() {
		if (MetaData.photo)
			SceneManager.LoadScene ("Camera", LoadSceneMode.Single);
		else
			SceneManager.LoadScene ("VideoTest", LoadSceneMode.Single);
	}
}