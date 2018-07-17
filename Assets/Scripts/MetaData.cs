using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaData {

    public static string APPNAME = "Ring-A-Ruse";

    public enum Method {
        Email       = 1,
        SMS         = 2,
        Whatsapp    = 3,
        Messenger   = 4,
        Wechat      = 5
    }

	public static int[] timeroptions = { 20, 900, 1800, 3600, 7200 };

	public static string defaultcontent = "3 photos and GPS location are attached";

	public static string verifyingusername;
    public static string firetoken;

	public static string[] name = new string[3];
	public static string[] email = new string[3];
	public static string[] phone = new string[3];

    public static int m1 = 0;
    public static int m2 = 0;
    public static int m3 = 0;

	public static string content;
	public static bool vibrate = true;
	public static bool ring = true;
	public static bool photo = true;
	public static int timer = 10;
	public static int timesecs = 10;
	public static float savedtimer = -1.0f;
    public static bool inAutomode = false;

    public static void writeRecipients() {
		for (int i = 0; i < 3; i++) {
			PlayerPrefs.SetString ("name"+i, name[i]);
			PlayerPrefs.SetString ("email"+i, email[i]);
			PlayerPrefs.SetString ("phone"+i, phone[i]);
		}
        
    }

	public static void writeMethods() {
		PlayerPrefs.SetInt("m1", m1);
		PlayerPrefs.SetInt("m2", m2);
		PlayerPrefs.SetInt("m3", m3);
	}

    public static void readRecipients() {
		for (int i = 0; i < 3; i++) {
			name[i] = PlayerPrefs.GetString ("name"+i);
			email[i] = PlayerPrefs.GetString ("email"+i);
			phone[i] = PlayerPrefs.GetString ("phone"+i);
		}
    }

	public static void readMethods() {
		m1 = PlayerPrefs.GetInt ("m1");
		m2 = PlayerPrefs.GetInt ("m2");
		m3 = PlayerPrefs.GetInt ("m3");
	}

	public static void setContent(string cont) {
		PlayerPrefs.SetString ("content", cont);
		PlayerPrefs.Save ();
	}

	public static string getContent() {
		return PlayerPrefs.GetString ("content");
	}

	public static void setVibrate(bool vibrate) {
		if (vibrate)
			PlayerPrefs.SetInt("vibrate", 0);
		else
			PlayerPrefs.SetInt("vibrate", 1);
		PlayerPrefs.Save ();
	}

	public static bool getVibrate() {
		int i = PlayerPrefs.GetInt ("vibrate");
		if (i == 0)
			return true;
		else
			return false;
	}

	public static void setPhoto(bool photo) {
		if (photo)
			PlayerPrefs.SetInt("photo", 0);
		else
			PlayerPrefs.SetInt("photo", 1);
		PlayerPrefs.Save ();
	}

	public static bool getPhoto() {
		int i = PlayerPrefs.GetInt ("photo");
		if (i == 0)
			return true;
		else
			return false;
	}

	public static void setRing(bool ring) {
		if (ring)
			PlayerPrefs.SetInt("ring", 0);
		else
			PlayerPrefs.SetInt("ring", 1);
		PlayerPrefs.Save ();
	}

	public static bool getRing() {
		int i = PlayerPrefs.GetInt ("ring");
		if (i == 0)
			return true;
		else
			return false;
	}

	public static void setTimer(int timer) {
		PlayerPrefs.SetInt("timer", timer);
		PlayerPrefs.Save ();
	}

	public static int getTimer() {
		return PlayerPrefs.GetInt ("timer");
	}

	public static void setCountdown(float timer) {
		PlayerPrefs.SetFloat("Countdown", timer);
		PlayerPrefs.Save ();
	}

	public static float getCountdown() {
		return PlayerPrefs.GetFloat ("Countdown");
	}

    public static void setInAutomode(bool inAuto) {
        inAutomode = inAuto;
        if (inAuto)
            PlayerPrefs.SetInt("InAutomode", 1);
        else
            PlayerPrefs.SetInt("InAutomode", 0);
        PlayerPrefs.Save ();
    }

    public static bool getInAutomode() {
        int i = PlayerPrefs.GetInt ("InAutomode");
        if (i == 1) return inAutomode = true;
        else return inAutomode = false;
    }

    public static void setUsername(string username) {
        PlayerPrefs.SetString ("username", username);
        PlayerPrefs.Save ();
    }

    public static string getUsername() {
        return PlayerPrefs.GetString ("username");
    }

    public static void setPassword(string password) {
        PlayerPrefs.SetString ("nickname", password);
        PlayerPrefs.Save ();
    }

    public static string getPassword() {
        return PlayerPrefs.GetString ("nickname");
    }

	public static void writeCurrentTicks(long sec) {
		PlayerPrefs.SetString ("CurrentTicks", sec.ToString());
		PlayerPrefs.Save ();
	}

	public static long readLastTicks() {
		return long.Parse(PlayerPrefs.GetString ("CurrentTicks"));
	}

	public static int getFirstOpen() {
		return PlayerPrefs.GetInt ("FirstOpen");
	}


	public static void settimesecsstring(string tss) {
		PlayerPrefs.SetString ("settimesecsstring", tss);
		PlayerPrefs.Save ();
	}

	public static string gettimesecsstring() {
		return PlayerPrefs.GetString ("settimesecsstring");
	}

	public static void setFirstOpen() {
		PlayerPrefs.SetInt ("FirstOpen", 1);
		PlayerPrefs.Save ();
	}

	public static void setdisplayname(string password) {
		PlayerPrefs.SetString ("namename", password);
		PlayerPrefs.Save ();
	}

	public static string getdisplayname() {
		return PlayerPrefs.GetString ("namename");
	}
}