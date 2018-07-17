using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PreloadPage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        try {
            Contacts.LoadContactList ();
        }
        catch (Exception e) {
            Debug.Log (e.Message);
            testcontact ();
        }

		if (MetaData.getFirstOpen () == 0) {
			try {
			AudioCall audiocall = new AudioCall ();
			audiocall.RegisterAccount ();
			MetaData.setFirstOpen (); 
			} catch (Exception e) {
				Debug.Log ("PreloadPage 27 :" + e.Message);
			}
		}

		if (MetaData.getInAutomode()) {
			MetaData.readRecipients ();
			MetaData.readMethods ();
			MetaData.content = MetaData.getContent ();
			float savedtime = MetaData.getCountdown ();
			long ticksnow = DateTime.Now.Ticks;
			long tickslast = MetaData.readLastTicks ();
			float timepassed = (ticksnow - tickslast) / 10000000.0f;
			if (timepassed >= savedtime) {
				MetaData.setCountdown (0.0f);
				MetaData.setInAutomode (false);
				SceneManager.LoadScene ("Camera");
			} else {
				MetaData.savedtimer = savedtime-timepassed;
				SceneManager.LoadScene ("Countdown");
			}
        }
	}

    public void testcontact() {
        Contacts.ContactsList.Clear();

        Contact c;
        PhoneContact p;
        EmailContact e;

        c = new Contact ();
        c.Name = "Brad Wang";
        p = new PhoneContact ();
        p.Number = "9999999999";
        e = new EmailContact ();
        e.Address = "yesprojecttest123@gmail.com";
        c.Phones.Add (p);
        c.Emails.Add (e);
        Contacts.ContactsList.Add(c);

        c = new Contact ();
        c.Name = "Ye Feng";
        p = new PhoneContact ();
        p.Number = "8888888888";
        e = new EmailContact ();
        e.Address = "ye_feng@hotmail.com";
        c.Phones.Add (p);
        c.Emails.Add (e);
        Contacts.ContactsList.Add(c);

        c = new Contact ();
        c.Name = "Seven Fang";
        p = new PhoneContact ();
        p.Number = "7777777777";
        e = new EmailContact ();
        e.Address = "iprotektur2017@gmail.com";
        c.Phones.Add (p);
        c.Emails.Add (e);
        Contacts.ContactsList.Add(c);
    }

	public void testcontact2() {
		Contacts.ContactsList.Clear();

		Contact c;
		PhoneContact p;
		EmailContact e;

		c = new Contact ();
		c.Name = "Brad";
		p = new PhoneContact ();
		p.Number = "9999999999";
		e = new EmailContact ();
		e.Address = "yesprojecttest123@gmail.com";
		c.Phones.Add (p);
		c.Emails.Add (e);
		Contacts.ContactsList.Add(c);

		c = new Contact ();
		c.Name = "Ye";
		p = new PhoneContact ();
		p.Number = "";
		e = new EmailContact ();
		e.Address = "ye_feng@hotmail.com";
		c.Phones.Add (p);
		c.Emails.Add (e);
		Contacts.ContactsList.Add(c);

		c = new Contact ();
		c.Name = "Fang";
		p = new PhoneContact ();
		p.Number = "";
		e = new EmailContact ();
		e.Address = "";
		c.Phones.Add (p);
		c.Emails.Add (e);
		Contacts.ContactsList.Add(c);

		c = new Contact ();
		c.Name = "Fe";
		p = new PhoneContact ();
		p.Number = "";
		e = new EmailContact ();
		e.Address = "";
		c.Phones.Add (p);
		c.Emails.Add (e);
		Contacts.ContactsList.Add(c);

		c = new Contact ();
		c.Name = "Fen";
		p = new PhoneContact ();
		p.Number = "";
		e = new EmailContact ();
		e.Address = "";
		c.Phones.Add (p);
		c.Emails.Add (e);
		Contacts.ContactsList.Add(c);

		c = new Contact ();
		c.Name = "Feng";
		p = new PhoneContact ();
		p.Number = "";
		e = new EmailContact ();
		e.Address = "";
		c.Phones.Add (p);
		c.Emails.Add (e);
		Contacts.ContactsList.Add(c);

		c = new Contact ();
		c.Name = "Feng1";
		p = new PhoneContact ();
		p.Number = "f1";
		e = new EmailContact ();
		e.Address = "f1";
		c.Phones.Add (p);
		c.Emails.Add (e);
		Contacts.ContactsList.Add(c);

		c = new Contact ();
		c.Name = "Feng2";
		p = new PhoneContact ();
		p.Number = "f2";
		e = new EmailContact ();
		e.Address = "f2";
		c.Phones.Add (p);
		c.Emails.Add (e);
		Contacts.ContactsList.Add(c);
	}

}