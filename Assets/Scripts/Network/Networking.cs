//#define SERVERDEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Networking : MonoBehaviour {
    #if SERVERDEBUG
    const string serverURL = "localhost:8080/";
    #else
    const string serverURL = "https://securityserver.herokuapp.com/";
    #endif
    
    void Start() {
    }

    IEnumerator Test() {
        UnityWebRequest www = UnityWebRequest.Get (serverURL+"test");
        yield return www.Send ();

        if (www.isNetworkError)
            Debug.Log (www.error);
        else
            Debug.Log (www.downloadHandler.text);
    }

    //input check, note username is limited to phone number
    private int checkPhoneNum(string p){
        if(p == null || p == "" || p.Length < 10){
            return 1;
        }
        return 0;
    }

    private int checkPassword(string p){
        if(p == null || p == "" || p.Length < 6){
            return 1;
        }
        return 0;
    }


    public void ClickedLogin() {
        string username = GameObject.Find("LoginUsername").GetComponent<InputField>().text;
        string password = GameObject.Find("LoginPassword").GetComponent<InputField>().text;
        int result = checkPhoneNum(username);
        if(result != 0){
            switch(result){
                case 1:
                    Debug.Log("Not a phone number.");
                    break;
            }
            return;
        }

        result = checkPassword(password);
        if(result != 0){
            switch(result){
                case 1:
                    Debug.Log("Password must longer than 6 digits.");
                    break;
            }
            return;
        }
        StartLogin (username, password);
    }

    public void ClickedVerify(){
		string verifyCode = GameObject.Find("verifycodeinput").GetComponent<InputField>().text;
        string username = MetaData.verifyingusername;
        StartVerify(verifyCode, username);
    }

    public void StartLogin(string username, string password) {
        StartCoroutine (Login (username, password));
    }

    public void StartVerify(string verifyCode, string username){
        StartCoroutine( Verify(verifyCode, username));
    }

    IEnumerator Login(string username, string password) {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(serverURL+"login", form);
        yield return www.Send();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error + " " + www.downloadHandler.text);
			if (www.downloadHandler.text.Length > 30)
				LoginPage.loginerror.text = www.downloadHandler.text.Substring(0,30);
			else 
				LoginPage.loginerror.text = www.downloadHandler.text;
        }
        else {
            Debug.Log(www.responseCode + " " + www.downloadHandler.text);
            MetaData.setUsername (username);
            MetaData.setPassword (password);
			MetaData.setdisplayname (www.downloadHandler.text);
            SceneManager.LoadScene("Active", LoadSceneMode.Single);
        }
    }

    IEnumerator Verify(string verifyCode, string username){
        WWWForm form = new WWWForm();
        form.AddField("verifyCode", verifyCode);
		form.AddField ("username", username);

        UnityWebRequest www = UnityWebRequest.Post(serverURL+"verify", form);
        yield return www.Send();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error + " " + www.downloadHandler.text);
            LoginPage.loginerror.text = www.downloadHandler.text;
        }
        else {
            Debug.Log(www.responseCode + " " + www.downloadHandler.text);

            SceneManager.LoadScene("Active", LoadSceneMode.Single);
        }
    }

    public void ClickedRegister() {
        System.Random rnd = new System.Random();
		string displayname = GameObject.Find("RegisterUsername").GetComponent<InputField>().text;
		string email = GameObject.Find("RegisterEmail").GetComponent<InputField>().text;
		string username = GameObject.Find("PhoneNumber Input").GetComponent<InputField>().text;
		string password = GameObject.Find("RegisterPassword Input").GetComponent<InputField>().text;
		string confirmPassword = GameObject.Find("ConfirmPassword Input").GetComponent<InputField>().text;
        //random generate verifyCode
        string verifyCode = rnd.Next(000000,999999).ToString();

        if(password != confirmPassword){
            Debug.Log("Password Dismatched.");
			LoginPage.registererror.text = "Password Dismatched.";
            return;
        }

        int result = checkPhoneNum(username);
        if(result != 0){
            switch(result){
                case 1:
                    Debug.Log("Not a phone number.");
					LoginPage.registererror.text = "Not a phone number.";
                    break;
            }
            return;
        }
        result = checkPassword(password);
        if(result != 0){
            switch(result){
                case 1:
                    Debug.Log("Password must longer than 6 digits.");
					LoginPage.registererror.text = "Password must longer than 6 digits.";
                    break;
            }
            return;
        }

		StartCoroutine (Register (username, password, verifyCode, displayname, email));
    }

	IEnumerator Register(string username, string password, string verifyCode, string name, string email) {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("verifyCode", verifyCode);
		form.AddField ("displayname", name);
		form.AddField ("emailaddress", email);
        UnityWebRequest www = UnityWebRequest.Post(serverURL+"register", form);
		Debug.Log ("Ready To Send Register");
        yield return www.Send();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error  + " " + www.downloadHandler.text);
			if (www.downloadHandler.text.Length > 30)
				LoginPage.registererror.text = www.downloadHandler.text.Substring (0, 30);
			else
            	LoginPage.registererror.text = www.downloadHandler.text;
        }
        else if(username == null){
            Debug.Log("username is NULL:"+username);
        }
        else {
			LoginPage.registererror.text = "";
            Debug.Log(www.responseCode + " " + www.downloadHandler.text);
            MetaData.verifyingusername = username;
			GameObject.Find ("Menu Switcher").GetComponent<MenuSwitch> ().onRegister2Confirm ();
            //SceneManager.LoadScene("Verify", LoadSceneMode.Single);
            SMS sms = GameObject.Find ("SMS OBJECT").GetComponent<SMS> ();
            sms.Send (username, "Your "+MetaData.APPNAME+" verify code: "+verifyCode+".");
        }
    }

	/*
    public void StartAutoMode(string regtoken) {
		StartCoroutine (AutoMode (regtoken, MetaData.timeroptions[MetaData.timer]));
    }

    IEnumerator AutoMode(string regtoken, int timedelay) {
        WWWForm form = new WWWForm();
        form.AddField("regtoken", regtoken);
        form.AddField ("timedelay", timedelay);

        UnityWebRequest www = UnityWebRequest.Post(serverURL+"firebase", form);
        yield return www.Send();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error + " " + www.downloadHandler.text);
            CountdownScript.errortext.text = www.downloadHandler.text;
        }
        else {
            Debug.Log(www.responseCode + " " + www.downloadHandler.text);
        }
    }

	public void StartCancelAuto(string regtoken) {
		StartCoroutine (CancelAuto (regtoken));
	}

	IEnumerator CancelAuto(string regtoken) {
		WWWForm form = new WWWForm();
		form.AddField("regtoken", regtoken);

		UnityWebRequest www = UnityWebRequest.Post(serverURL+"firebasecancel", form);
		yield return www.Send();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error + " " + www.downloadHandler.text);
		}
		else {
			Debug.Log(www.responseCode + " " + www.downloadHandler.text);
			MetaData.setInAutomode (false);
			SceneManager.LoadScene("Active", LoadSceneMode.Single);
		}
	}*/
}