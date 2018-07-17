using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public struct fileio{
    public string success;
    public string key;
    public string link;
    public string expiry;
}
public class SMS : MonoBehaviour
{
    AndroidJavaObject currentActivity;
    List<string> phones;
    string text;

	public void Share(string app) {
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		jo.Call("shareSetup", this.text, app);
		Debug.Log("finished share with " + app);
	}

    public void StartUploadPhoto(string text, List<byte[]> data){
        Debug.Log("start upload photos");
        StartCoroutine(uploadPhoto(text, data));
    }

    IEnumerator uploadPhoto(string text, List<byte[]> data){
        string urls = "";
		WWWForm form;
		UnityWebRequest w;
        string uploadBaseUrl = "https://file.io";
		if (data.Count > 1) {
			for (int i = 0; i < data.Count; i++) {
				form = new WWWForm ();
				form.AddBinaryData ("file", data [i], "photo" + (i + 1) + ".jpg", "image/jpg");
				w = UnityWebRequest.Post (uploadBaseUrl, form);
				yield return w.Send ();
				if (w.isNetworkError || w.isHttpError) {
					Debug.Log (w.error + " " + w.downloadHandler.text);
				} else {
					Debug.Log ("Finished Uploading photo" + w.downloadHandler.text);
					fileio article = JsonUtility.FromJson<fileio> (w.downloadHandler.text);
					urls += article.link + "\n";
				}
			}
		} else {
			form = new WWWForm ();
			form.AddBinaryData ("file", data [0], "video.mp4", "video/mp4");
			w = UnityWebRequest.Post (uploadBaseUrl, form);
			yield return w.Send ();
			if (w.isNetworkError || w.isHttpError) {
				Debug.Log (w.error + " " + w.downloadHandler.text);
			} else {
				Debug.Log ("Finished Uploading photo" + w.downloadHandler.text);
				fileio article = JsonUtility.FromJson<fileio> (w.downloadHandler.text);
				urls += article.link + "\n";
			}

		}


        //map photo
        form = new WWWForm();
        form.AddBinaryData("file", GoogleAPI.texture.EncodeToJPG(), "map.jpg", "image/jpg");
        w = UnityWebRequest.Post(uploadBaseUrl, form);
        yield return w.Send();
        if (w.isNetworkError || w.isHttpError) {
            Debug.Log(w.error+ " " + w.downloadHandler.text);
        }
        else {
            Debug.Log("Finished Uploading photo"+w.downloadHandler.text);
            fileio article = JsonUtility.FromJson<fileio>(w.downloadHandler.text);
            urls += article.link;
        }

        Debug.Log("urls: "+ urls);
        this.text = text + urls;
        Debug.Log("sms test: "+this.text);
        RunAndroidUiThread();
    }

     public void Send(string phone, string text) {
        if (Application.platform == RuntimePlatform.Android)
        {
            this.phones = new List<string> ();
            this.phones.Add(phone);
            this.text = text + MetaData.content+"over";
            RunAndroidUiThread();
        }
    }
    public void Send(string phone, string text, List<byte[]> data) {
        if (Application.platform == RuntimePlatform.Android)
        {
            this.phones = new List<string> ();
            this.phones.Add(phone);
            StartUploadPhoto(text,data);
        }
    }

    public void Send(List<string> phones, string text,List<byte[]> data)

    {
        if (Application.platform == RuntimePlatform.Android)
        {
            this.phones = phones;
            StartUploadPhoto(text,data);
        }
    }

    void RunAndroidUiThread()
    {
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(SendProcess));
    }

    void SendProcess()
    {
        Debug.Log("Running on UI thread");
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

        // SMS Information
		string alert = "Message sent successfully.";
        try
        {
            // SMS Manager
            AndroidJavaClass SMSManagerClass = new AndroidJavaClass("android.telephony.SmsManager");
            AndroidJavaObject SMSManagerObject = SMSManagerClass.CallStatic<AndroidJavaObject>("getDefault");
            foreach (string phone in phones) {
                SMSManagerObject.Call("sendTextMessage", phone, null, text, null, null);
            }
            alert = "Message sent successfully.";
        }
        catch (System.Exception e)
        {
            Debug.Log("Error : " + e.StackTrace.ToString());
            alert = "Error has been Occurred. Fail to send message.";
        }
		if (MetaData.m1 == 1 || MetaData.m2 == 1 || MetaData.m3 == 1 || MetaData.m1 == 2 || MetaData.m2 == 2 || MetaData.m3 == 2) {
			// Show Toast
			AndroidJavaClass Toast = new AndroidJavaClass ("android.widget.Toast");
			AndroidJavaObject javaString = new AndroidJavaObject ("java.lang.String", alert);
			AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject> ("makeText", context, javaString, Toast.GetStatic<int> ("LENGTH_SHORT"));
			toast.Call ("show");
		}
		if (MetaData.m1 > 2 || MetaData.m2 > 2 || MetaData.m3 > 2) {
			AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
			AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", "Ready to share.");
			AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
			toast.Call("show");
		}

		if (MetaData.m1 == 3)
			Share ("whatsapp");
		else if (MetaData.m1 == 4)
			Share ("facebook");
		else if (MetaData.m1 == 5)
			Share ("wechat");


		if (MetaData.m2 == 3)
			Share ("whatsapp");
		else if (MetaData.m2 == 4)
			Share ("facebook");
		else if (MetaData.m2 == 5)
			Share ("wechat");

		if (MetaData.m3 == 3)
			Share ("whatsapp");
		else if (MetaData.m3 == 4)
			Share ("facebook");
		else if (MetaData.m3 == 5)
			Share ("wechat");
    }
}