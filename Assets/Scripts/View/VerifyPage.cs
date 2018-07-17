using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerifyPage : MonoBehaviour {

	Networking network;

	void Start () {
		network = GameObject.Find ("Main Camera").GetComponent<Networking> ();
	}

	public void VerifyCode() {
		SceneManager.LoadScene("Active", LoadSceneMode.Single);
	}
}
