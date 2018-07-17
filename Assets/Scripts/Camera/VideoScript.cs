using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour {

    public string path;

	void Start() {
		Debug.Log ("video script starts");
		OnClickTakeVideo ();
	}

    public void OnClickTakeVideo()
    {
        Debug.Log("video started");
        StartCoroutine(StartVideo());
    }

    public void OnClickSendVideo()
    {
        StartCoroutine(StartFilePath());
        Debug.Log(Application.persistentDataPath);
        if (path != null)
        {
            Debug.Log("This is the path: " + path);
            StartCoroutine(StartSend());
        }
        else
        {
            Debug.Log("failed to get file path");
        }
    }

    IEnumerator StartVideo()
    {
        ///////////////////////Video fuction//////////////////////////////
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //string s = "ssss";
        jo.Call("dispatchTakeVideoIntent");
        Debug.Log("video finished");
		yield return null;
		yield return new WaitForSeconds (1);

		OnClickSendVideo ();
    }

    IEnumerator StartFilePath()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        path = jo.GetStatic<string>("ggg");
        Debug.Log("Finally This is the actual path: " + path);
        yield return path;
    }

    IEnumerator StartSend()
    {
        //string filePath = "file://" + "/storage/emulated/0/DCIM/Camera/VID_20180110_143939.mp4";
        string filePath = "file://" + path;
        var www = new WWW(filePath);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("StartSend: Can't read!!!!!!!!!!!!!!!!!!");
        }

        byte[] bytes1;

        bytes1 = www.bytes;
        if (bytes1 != null)
        {
            Debug.Log("There are smth in bytes1");
        }
        else
        {
            Debug.Log("Unity!!!! Failed to read");
        }
        List<byte[]> data = new List<byte[]>();
        data.Add(bytes1);
        Sending.Send(data);
		yield return null;
		yield return new WaitForSeconds (1);
        SceneManager.LoadScene("Active", LoadSceneMode.Single);
    }
}
