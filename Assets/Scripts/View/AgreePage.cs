using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgreePage : MonoBehaviour {

	Toggle agree;

	// Use this for initialization
	void Start () {
		agree = GameObject.Find ("ReadCheck").GetComponent<Toggle> ();
	}

	public void AgreeButton() {
		if (agree.isOn)
			GameObject.Find ("Menu Switcher").GetComponent<MenuSwitch> ().onRegisterClick ();
		else
			GameObject.Find ("AgreementPanel Error").GetComponent<Text> ().text = "Please Check Understand";
	}
}
