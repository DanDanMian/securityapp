using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : MonoBehaviour {

    public static Text loginerror, registererror;

	// Use this for initialization
	void Start () {
        loginerror = GameObject.Find ("LoginError").GetComponent<Text> ();
        registererror = GameObject.Find ("RegisterError").GetComponent<Text> ();
        GameObject.Find("LoginUsername").GetComponent<InputField>().text = MetaData.getUsername();
        GameObject.Find("LoginPassword").GetComponent<InputField>().text = MetaData.getPassword();
	}
}