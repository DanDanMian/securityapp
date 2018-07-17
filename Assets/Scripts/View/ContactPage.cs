using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ContactPage : MonoBehaviour {

	public static List<Contact> searchlist = new List<Contact>();

	public static InputField[] name, email, phone;
    GameObject sl1, sl2, sl3;
	GameObject af1, af2, af3;
	MenuSwitch menuswitch;
	public GameObject buttonPrefab;
    public GameObject smallbuttonPrefab;
    SettingPage settingpage;
	int recipientnum;

	void Awake(){
		name = new InputField[3];
		email = new InputField[3];
		phone = new InputField[3];
		for (int i = 0; i < 3; i++) {
			name[i] = GameObject.Find ("Name Input "+(i+1)).GetComponent<InputField> ();
			email[i] = GameObject.Find ("Email Input "+(i+1)).GetComponent<InputField> ();
			phone[i] = GameObject.Find ("Number Input "+(i+1)).GetComponent<InputField> ();
		}

	}

	// Use this for initialization
	void Start () {
		sl1 = GameObject.Find ("SearchedList1");
		sl2 = GameObject.Find ("SearchedList2");
		sl3 = GameObject.Find ("SearchedList3");
		af1 = GameObject.Find ("AutoFill1");
		af2 = GameObject.Find ("AutoFill2");
		af3 = GameObject.Find ("AutoFill3");
		menuswitch = GameObject.Find ("MenuSwitch").GetComponent<MenuSwitch> ();
        settingpage = GameObject.Find ("Main Camera").GetComponent<SettingPage> ();

		MetaData.readRecipients ();
		for (int i = 0; i < 3; i++) {
			name [i].text = MetaData.name [i];
			email [i].text = MetaData.email [i];
			phone [i].text = MetaData.phone [i];
		}
		Clear ();
		/*
		Debug.Log ("Save Contact List");
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/contactlist.data");
		bf.Serialize(file, contactlist);
		file.Close();
		*/
	}

	public void Cancel() {
		StartCoroutine (CancelAutoComplete ());
	}

	IEnumerator CancelAutoComplete(){
		yield return new WaitForSeconds (1.0f);
		Clear ();
	}


	public void Clear() {
		af1.SetActive (false);
		af2.SetActive (false);
		af3.SetActive (false);
        foreach (Transform child in sl1.transform) {
            GameObject.Destroy(child.gameObject);
        }
		foreach (Transform child in sl2.transform) {
			GameObject.Destroy(child.gameObject);
		}
		foreach (Transform child in sl3.transform) {
			GameObject.Destroy(child.gameObject);
		}
        searchlist.Clear ();
	}

	public void AutoSearch1(string s) {
		recipientnum = 0;
		AutoSearch (s);
	}

	public void AutoSearch2(string s) {
		recipientnum = 1;
		AutoSearch (s);
	}

	public void AutoSearch3(string s) {
		recipientnum = 2;
		AutoSearch (s);
	}
				
	public void AutoSearch(string s) {
        if (s == null || s == "") {
			Clear ();
            return;
        }
		Clear ();

        for (int i = 0; i < Contacts.ContactsList.Count; i++) {
            if (searchlist.Count > 5)
                break;
            Contact c = Contacts.ContactsList [i];
            int index = c.Name.ToLower().IndexOf (s.ToLower());
            if (index < 0)
                continue;
            if (index == 0 || c.Name[index-1] == ' '){
                searchlist.Add (c);
            }
        }
		if (searchlist.Count > 0) {
			if (recipientnum == 0) {
				af1.SetActive (true);
			}
			else if (recipientnum == 1) {
				af2.SetActive (true);
			}
			else if (recipientnum == 2) {
				af3.SetActive (true);
			}
		}

        //ADD NEW
        for (int i = 0; i < searchlist.Count; i++) {
            AddSearchedButton (i);
        }
    }

    void AddSearchedButton(int index) {
        Contact contact = searchlist [index];
        GameObject button = (GameObject)Instantiate(smallbuttonPrefab);
		if (recipientnum == 0) {
			button.transform.SetParent(sl1.transform);
		}
		else if (recipientnum == 1) {
			button.transform.SetParent(sl2.transform);
		}
		else if (recipientnum == 2) {
			button.transform.SetParent(sl3.transform);
		}
        button.GetComponent<Button> ().onClick.AddListener (delegate{SelectAutoComplete(index);});
        button.transform.GetChild(0).GetComponent<Text>().text = contact.Name;
    }

    void SelectAutoComplete(int index) {
		Debug.Log ("SelectAutoComplete");
        Contact contact = searchlist [index];
		name[recipientnum].text = contact.Name;
		if (contact.Phones.Count > 0)
			phone[recipientnum].text = contact.Phones [0].Number;
		else
			phone[recipientnum].text = "";
		if (contact.Emails.Count > 0)
			email[recipientnum].text = contact.Emails [0].Address;
		else
			email[recipientnum].text = "";
		Clear ();
    }
}