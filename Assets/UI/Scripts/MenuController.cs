using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class MenuController : MonoBehaviour {

	public void LoadLever(string name) {
		Debug.Log ("load level");
		SceneManager.LoadScene(name);
	}

	public void QuitRequest() {
		Debug.Log ("quitttt");
		Application.Quit ();
	}
	public void OnYoukeClick() {
		Debug.Log ("youke");
	}

	public void OnWeiXinClick(){
		Debug.Log("weixin");

	}
	public void OnFacebookClick() {
		Debug.Log ("FaceBook");
		
	}
	public void OnGoogleClick() {
		Debug.Log ("google");
	}

	public void OnTwitterClick() {
		Debug.Log ("Twitter");
	}

	public void OnActiveClick() {
		Debug.Log ("active");
	}

	public void OnPauseClick() {
		Debug.Log ("pause");
	}

		
 
}






