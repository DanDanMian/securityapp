using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myNotifi : MonoBehaviour {
    private Text mText;

    // Use this for initialization
    void Start () {
        mText = GameObject.Find("MsgText").GetComponent<Text>();
    }

    public void StartNotifi()
    {
        // Android的Java接口 
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //AndroidJavaObject jo = new AndroidJavaObject("com.yesproject.unitynotificationplugin.MainActivity");


        jo.Call("myNotification");
        string flag = jo.GetStatic<string>("flag1");

        //int res = jo.Call<int>("add", 1, 1);
        //string flag = res.ToString();

        mText.text = flag;
    }

    public void anotherway()
    {
        mText.text = "bbbb";
    }

    // Update is called once per frame
    void Update () {
		
	}

}
