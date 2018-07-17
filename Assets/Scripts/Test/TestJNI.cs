using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestJNI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");
		int i = jo.Call<int> ("secret");
		Debug.Log (i);
		GameObject.Find ("Text").GetComponent<Text> ().text = i.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
