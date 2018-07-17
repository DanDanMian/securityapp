using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SettingPage : MonoBehaviour {

	//Slider timer;
	ToggleController ring;
	ToggleController vib;
	ToggleController mode;
	//Text timertext;
	InputField cont;
	InputField datetime;
    Text errormsg;

    Dropdown m1, m2, m3;
	GameObject e1,e2,e3,p1,p2,p3;

	void Start() {
		e1 = GameObject.Find ("Email Input 1");
		p1 = GameObject.Find ("Number Input 1");

		e2 = GameObject.Find ("Email Input 2");
		p2 = GameObject.Find ("Number Input 2");

		e3 = GameObject.Find ("Email Input 3");
		p3 = GameObject.Find ("Number Input 3");

		//timer = GameObject.Find("Timer").GetComponent<Slider>();
		datetime = GameObject.Find("DateTime").GetComponent<InputField>();
		datetime.text = MetaData.gettimesecsstring ();
			
		ring = GameObject.Find ("Ringtone Toggle").GetComponent<ToggleController> ();
		vib = GameObject.Find("Vibration Toggle").GetComponent<ToggleController> ();
		try {
		mode = GameObject.Find ("Mode Togglee").GetComponent<ToggleController> ();
		} catch (Exception e) {
			Debug.Log ("Disabled Video"); 
		}
		//timertext = GameObject.Find ("TimerText").GetComponent<Text> ();
		cont = GameObject.Find ("MessageInput").GetComponent<InputField> ();
        errormsg = GameObject.Find ("ErrorMSG").GetComponent<Text> ();

        m1 = GameObject.Find ("Method1").GetComponent<Dropdown> ();
        m2 = GameObject.Find ("Method2").GetComponent<Dropdown> ();
        m3 = GameObject.Find ("Method3").GetComponent<Dropdown> ();
		try {
			mode.isOn = MetaData.getPhoto ();
			MetaData.photo = mode.isOn;
			mode.Start ();
		} catch (Exception e) {
			Debug.Log ("Disabled Video"); 
		}
		MetaData.photo = MetaData.getPhoto ();

		ring.isOn = MetaData.getRing ();
		MetaData.ring = ring.isOn;
		ring.Start ();
		vib.isOn = MetaData.getVibrate ();
		MetaData.vibrate = vib.isOn;
		vib.Start ();
		//MetaData.timer = MetaData.getTimer ();
		//timer.value = MetaData.timer / 4.0f;
		//updatetime ();//timertext.text = MetaData.timeroptions[MetaData.timer].ToString();
		MetaData.content = MetaData.getContent ();
		if (MetaData.content == null || MetaData.content == "") {
			MetaData.content = MetaData.defaultcontent;
			MetaData.setContent (MetaData.content);
		}
		cont.text = MetaData.content;

		MetaData.readMethods ();
        m1.value = MetaData.m1;
		selectm1 (m1.value);
        m2.value = MetaData.m2;
		selectm2 (m2.value);
        m3.value = MetaData.m3;
		selectm3 (m3.value);
	}

    public void TriggerManualMode() {
		if (isready()) {
            ReadyForCamera ();
			SceneManager.LoadScene ("FakeCamera", LoadSceneMode.Single);
        }
		else {
			if (m1.value + m2.value + m3.value <= 0)
				errormsg.text = "Please Select At Least One Recipient And Method To Continue";
			else
				errormsg.text = "Please Enter Corresponding Contact Info To Continue";
        }
    }

    public void TriggerAutoMode() {
		try {
			String datetimestr = datetime.text;
			DateTime future = DateTime.Parse(datetimestr);
			TimeSpan secs = future - DateTime.Now;
			Debug.Log("Total Seconds: " + secs.TotalSeconds);
			MetaData.timesecs = (int)secs.TotalSeconds;
			if (MetaData.timesecs <= 0) {
				errormsg.text = "Please Enter Some DateTime In the Future To Activate.";
				return;
			} 
			MetaData.settimesecsstring(datetimestr);

		} catch (Exception e) {
			Debug.Log ("Exception isready: " + e.Message);
			errormsg.text = "Please Enter Valid DateTime String According To The Format.";
			return;
		}


		if (isready()) {
            ReadyForCamera ();
            
            SceneManager.LoadScene ("Countdown", LoadSceneMode.Single);
        }
        else {
			if (m1.value + m2.value + m3.value <= 0)
				errormsg.text = "Please Select At Least One Recipient And Method To Continue";
			else
				errormsg.text = "Please Enter Corresponding Contact Info To Continue";
        }
    }

	public bool isready(){
		bool ready = false;
		if (m1.value == 1 && ContactPage.email [0].text.Length > 0)
			ready = true;
		else if (m1.value == 2 && ContactPage.phone [0].text.Length > 0)
			ready = true;
		else if (m1.value > 2)
			ready = true;

		if (m2.value == 1 && ContactPage.email [1].text.Length > 0)
			ready = true;
		else if (m2.value == 2 && ContactPage.phone [1].text.Length > 0)
			ready = true;
		else if (m2.value > 2)
			ready = true;

		if (m3.value == 1 && ContactPage.email [2].text.Length > 0)
			ready = true;
		else if (m3.value == 2 && ContactPage.phone [2].text.Length > 0)
			ready = true;
		else if (m3.value > 2)
			ready = true;
		return ready;
	}


    public void ReadyForCamera () {
		for (int i = 0; i < 3; i++) {
			MetaData.name [i] = ContactPage.name [i].text;
			MetaData.email [i] = ContactPage.email [i].text;
			MetaData.phone [i] = ContactPage.phone [i].text;
		}

        MetaData.m1 = m1.value;
        MetaData.m2 = m2.value;
        MetaData.m3 = m3.value;
        MetaData.content = cont.text;
        MetaData.setContent (MetaData.content);
        MetaData.writeRecipients ();
		MetaData.writeMethods ();
    }

	public void selectm1(int value) {
		if (value == 1) {
			e1.SetActive (true);
			p1.SetActive (false);
		} else if (value == 2) {
			e1.SetActive (false);
			p1.SetActive (true);
		} else {
			e1.SetActive (false);
			p1.SetActive (false);
		}
	}

	public void selectm2(int value) {
		if (value == 1) {
			e2.SetActive (true);
			p2.SetActive (false);
		} else if (value == 2) {
			e2.SetActive (false);
			p2.SetActive (true);
		} else {
			e2.SetActive (false);
			p2.SetActive (false);
		}
	}

	public void selectm3(int value) {
		if (value == 1) {
			e3.SetActive (true);
			p3.SetActive (false);
		} else if (value == 2) {
			e3.SetActive (false);
			p3.SetActive (true);
		} else {
			e3.SetActive (false);
			p3.SetActive (false);
		}
	}



    public void ClearError() {
        errormsg.text = "";
    }

	public void PhotoVideo() {
		MetaData.photo = !MetaData.photo;
		MetaData.setPhoto (MetaData.photo);
	}

	public void Vibrate () {
		MetaData.vibrate = !MetaData.vibrate;
		MetaData.setVibrate (MetaData.vibrate);
	}

	public void Ring() {
		MetaData.ring = !MetaData.ring;
		MetaData.setRing (MetaData.ring);
	}

	public void Timer(){
		//MetaData.timer = (int)Math.Floor (timer.value * 4);
		MetaData.setTimer(MetaData.timer);
		updatetime ();
	}

	public void updatetime(){
		int totalsecs = MetaData.timeroptions[MetaData.timer];
		string result = "";
		int hour = totalsecs / 3600;
		if (hour > 0) {
			result += hour + "h ";
		}
		int mins = (totalsecs / 60) % 60;
		if (mins > 0) {
			result += mins + "m ";
		}
		int secs = totalsecs % 60;
		if (secs > 0)
			result += secs + "s ";
		result += "time remaining to start";
		//timertext.text = result;

	}
}