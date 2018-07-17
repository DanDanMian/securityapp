using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Sending : MonoBehaviour {

    public static void Send (List<byte[]> data) {
        List<string> emails = new List<string> ();
        List<string> phones = new List<string> ();
		String messages = "Lat:" + GPS.latitude.ToString() + " Lon:" + GPS.longtitude.ToString();
        Debug.Log("This is Lat and Lon: " + messages);
		messages = MetaData.content + "\n" + messages + "\n";

        
        Debug.Log ("Adding r1");
        if (MetaData.m1 == (int)MetaData.Method.Email) {
			if (MetaData.email [0] != null && MetaData.email [0] != "")
				emails.Add (MetaData.email [0]);
        }
        else if (MetaData.m1 == (int)MetaData.Method.SMS) {
			if (MetaData.phone [0] != null && MetaData.phone [0] != "")
				phones.Add (MetaData.phone [0]);
        }
        Debug.Log ("Adding r2");
        if (MetaData.m2 == (int)MetaData.Method.Email) {
			if (MetaData.email [1] != null && MetaData.email [1] != "")
				emails.Add (MetaData.email [1]);
        }
        else if (MetaData.m2 == (int)MetaData.Method.SMS) {
			if (MetaData.phone [1] != null && MetaData.phone [1] != "")
				phones.Add (MetaData.phone [1]);
        }
        Debug.Log ("Adding r3");
        if (MetaData.m3 == (int)MetaData.Method.Email) {
			if (MetaData.email [2] != null && MetaData.email [2] != "")
				emails.Add (MetaData.email [2]);
        }
        else if (MetaData.m3 == (int)MetaData.Method.SMS) {
			if (MetaData.phone [2] != null && MetaData.phone [2] != "")
				phones.Add (MetaData.phone [2]);
        }

        
		if (emails.Count > 0)
        	Email.SendMail (emails, data);
		try {
			SMS sms = GameObject.Find("SMS OBJECT").GetComponent<SMS>();
            sms.Send(phones, messages, data);
		} catch (Exception e) {
	     	Debug.Log ("Sending 59: " + e.Message);
		}
    }
}