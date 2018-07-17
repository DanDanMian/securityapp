using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioCall {

	AndroidJavaClass jc;
	AndroidJavaObject jo;

	public AudioCall () {
		try {
		jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		}
		catch (Exception e) {
			Debug.Log ("Audiocall: " + e.Message);
		}
	}

	public void StartCountdown() {
		try {
			jo.Call("startCountdown", MetaData.timesecs);
			Debug.Log("Audiocall Started");
		}
		catch (Exception e) {
			Debug.Log ("Audiocall: " + e.Message);
		}
	}

	public void StopCountdown() {
		try {
		jo.Call ("stopCountdown");
		}
		catch (Exception e) {
			Debug.Log ("Audiocall: " + e.Message);
		}
	}

	public void RegisterAccount() {
		try {
		jo.Call ("registerAccount");
		}
		catch (Exception e) {
			Debug.Log ("Audiocall: " + e.Message);
		}
	}
}