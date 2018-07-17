using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CountdownScript : MonoBehaviour {

	public bool started = false;
	public static AudioCall audiocall;
    public string scenceLoad;
    public float timer;
	public float secondtimer = 0.2f;
    private Text timerSeconds;
    public bool timeup = false;
    public static Text errortext;
    public Networking network;

	DateTime begin = new DateTime(2017, 1, 1);

	// Use this for initialization
	void Start () {
		
		network = GameObject.Find ("Main Camera").GetComponent<Networking> ();
		errortext = GameObject.Find ("AutoModeError").GetComponent<Text> ();



        //receive the notification from firebase
		/*if (MetaData.firetoken == null) {
			Firebase.FirebaseApp.CheckAndFixDependenciesAsync ().ContinueWith (task => {
				dependencyStatus = task.Result;
				if (dependencyStatus == Firebase.DependencyStatus.Available) {
					InitializeFirebase ();
				} else {
					Debug.LogError ("Could not resolve all Firebase dependencies: " + dependencyStatus);
				}
			});
		} else {
			Debug.Log ("Already Had Token");
			network.StartAutoMode (MetaData.firetoken);
		}*/
        timerSeconds = GetComponent<Text>();

		if (MetaData.savedtimer > 0) {
			timer = MetaData.savedtimer;
			MetaData.savedtimer = -1.0f;
			started = true;
		} else {
			//timer = MetaData.timeroptions[MetaData.timer];
			timer = MetaData.timesecs;
		}

		timerSeconds.text = "Click Answer To Start \n\n " + updatetime((int)timer);

		audiocall = new AudioCall ();
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			secondtimer -= Time.deltaTime;
			if (secondtimer <= 0) {
				secondtimer = 0.2f;
				long ticksnow = DateTime.Now.Ticks;
				long tickslast = MetaData.readLastTicks ();
				MetaData.writeCurrentTicks (ticksnow);
				float timepassed = (ticksnow - tickslast) / 10000000.0f;
				timer -= timepassed;
				//Debug.Log ("Time Passed: " + timepassed + "  Timer: " + timer);
				MetaData.setCountdown (timer);
				timerSeconds.text = updatetime((int)timer);
				if (timer <= 0)
					timeup = true;
				if (timeup) {
					MetaData.setInAutomode (false);
					if (MetaData.photo)
						SceneManager.LoadScene ("Camera", LoadSceneMode.Single);
					else
						SceneManager.LoadScene ("VideoTest", LoadSceneMode.Single);
				}
			}
		}
	}

	public string updatetime(int totalsecs){
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
		result += "\n Time Left To Trigger Automode";
		return result;

	}

	public void starttimer() {
		if (started) {
			GameObject.Find ("Quit").GetComponent<Quit> ().quit ();
		} else {
			MetaData.writeCurrentTicks (DateTime.Now.Ticks);
			started = true;
			MetaData.setInAutomode (true);
			audiocall.StartCountdown ();
		}
	}

	public void stoptimer() {
		if (started) {
			MetaData.setInAutomode (false);
			audiocall.StopCountdown ();
		}
		started = false;
		SceneManager.LoadScene ("Active", LoadSceneMode.Single);
	}


	/*
    void InitializeFirebase() {
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Debug.Log("Firebase Messaging Initialized");
    }

    public virtual void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e) {
        Debug.Log("Received a new message: " + e.Message);
		// Firebase message is saved and might be callen on the next run
        //MetaData.setInAutomode (false);
        //SceneManager.LoadScene("Fake Phone Call", LoadSceneMode.Single);
    }

    public virtual void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
        Debug.Log("Received Registration Token: " + token.Token);
        MetaData.firetoken = token.Token;
		if (!continued)
        	network.StartAutoMode (token.Token);
    }

    public void cancelAutoMode() {
		network.StartCancelAuto (MetaData.firetoken);
    }*/
}