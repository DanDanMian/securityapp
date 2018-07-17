using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {
	
	// Update is called once per frame
	void Update() {
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (SceneManager.GetActiveScene ().name == "Active") {
				MenuSwitch ms = GameObject.Find ("MenuSwitch").GetComponent<MenuSwitch> ();
				if (ms.getmainmenuactive()) {
					quit();
				} else {
					ms.onMainMenuPanelClick ();
				}
					
			} else if (SceneManager.GetActiveScene ().name == "FakeCamera") {
				SceneManager.LoadScene("Active", LoadSceneMode.Single);
			} else if (SceneManager.GetActiveScene ().name == "Countdown") {
				if (!GameObject.Find("Time").GetComponent<CountdownScript>().started)
					SceneManager.LoadScene("Active", LoadSceneMode.Single);
				else
					quit ();
			}
			else {
				quit ();
			}
		}

	}

	public void quit() {
		if (Application.platform == RuntimePlatform.Android)
		{
			AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call<bool>("moveTaskToBack", true);
		}
		else
		{
			Application.Quit();
		}
	}
}
