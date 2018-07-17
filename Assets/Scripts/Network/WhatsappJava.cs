using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatsappJava : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }

    public void SendToWhatsapp()
    {
        // Android的Java接口 
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        jo.Call("SendToWhatsapp");
    }

    // Update is called once per frame
    void Update () {
		
	}
}