using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuSwitch : MonoBehaviour {

	public RectTransform MenuContainer;
	private Vector3 PanelPosition;
	private GameObject MainMenuPanel;
	private GameObject OptionsPanel;
	private GameObject ContactListPanel;
	private GameObject AddContactPanel;
	private GameObject HelpPanel;
	private GameObject LoginPanel; 
	private GameObject MainPanel;
	private GameObject RegisterPanel1;
	private GameObject RegisterPanel2;
	private GameObject VerifyPanel;

	public int ID = 0;

	void Start () {
		MainMenuPanel = GameObject.Find("MainMenuPanel");
		OptionsPanel = GameObject.Find("OptionsPanel");
		ContactListPanel = GameObject.Find("ContactListPanel");
		AddContactPanel = GameObject.Find("AddContactPanel");
		HelpPanel = GameObject.Find ("HelpPanel");
		LoginPanel = GameObject.Find ("LoginPanel");
		MainPanel = GameObject.Find ("MainPanel");
		RegisterPanel1 = GameObject.Find ("RegisterPanel1");
		RegisterPanel2 = GameObject.Find ("RegisterPanel2");
		VerifyPanel = GameObject.Find ("VerifyPanel");

        try {
		SwitchContactPanel (ID);
		SwitchToMenu (ID);
        } catch (Exception e) {
            Debug.Log ("MenuSwitch 39: "+ e.Message);
        }
	}
	public void SwitchToMenu(int menuID) {

		switch (menuID) {
		case 0:
			OptionsPanel.gameObject.SetActive (false);
			MainMenuPanel.gameObject.SetActive (true);
			HelpPanel.gameObject.SetActive (false);
			break;

		//
		case 1:
			MainMenuPanel.gameObject.SetActive (false);
			OptionsPanel.gameObject.SetActive (true);
			HelpPanel.gameObject.SetActive (false);
			break;

		//
		case 2:
			MainMenuPanel.gameObject.SetActive (false);
			OptionsPanel.gameObject.SetActive (false);
			HelpPanel.gameObject.SetActive (true);
			break;

		//
		case 3:
			MainPanel.gameObject.SetActive (false);
			LoginPanel.gameObject.SetActive (true);
			break;

		//
		case 4:
			MainPanel.gameObject.SetActive (true);
			LoginPanel.gameObject.SetActive (false);
			break;

		//Sign up
		case 5:
			MainPanel.gameObject.SetActive (false);
			LoginPanel.gameObject.SetActive (false);
			break;

		//RegisterPanel1 Back
		case 6:
			MainPanel.gameObject.SetActive (true);
			LoginPanel.gameObject.SetActive (true);
			break;

		//RegisterPanel2 Active
		case 7:
			MainPanel.gameObject.SetActive (false);
			LoginPanel.gameObject.SetActive (false);
			RegisterPanel1.gameObject.SetActive (false);
			break;

		//RegisterPanel2 Deactive
		case 8:
			MainPanel.gameObject.SetActive (false);
			LoginPanel.gameObject.SetActive (false);
			RegisterPanel1.gameObject.SetActive (true);
			break;

		//VerifyPanel Active
		case 9:
			MainPanel.gameObject.SetActive (false);
			LoginPanel.gameObject.SetActive (false);
			RegisterPanel1.gameObject.SetActive (false);
			RegisterPanel2.gameObject.SetActive (false);
			VerifyPanel.gameObject.SetActive (true);
			break;

		//VerifyPanel Deactive
		case 10:
			MainPanel.gameObject.SetActive (false);
			LoginPanel.gameObject.SetActive (false);
			RegisterPanel1.gameObject.SetActive (false);
			RegisterPanel2.gameObject.SetActive (true);
			break;
		
		//register2 confirm
		case 11:
			RegisterPanel2.gameObject.SetActive (false);
			VerifyPanel.gameObject.SetActive (true);
			break;

		//verify back
		case 12:
			RegisterPanel2.gameObject.SetActive (true);
			VerifyPanel.gameObject.SetActive (false);
			break;
		
		//verify 
		case 13:
			break;



		}
	}

	public void SwitchContactPanel(int menuID) {

		switch (menuID) {
		case 0:
			AddContactPanel.gameObject.SetActive (false);
			ContactListPanel.gameObject.SetActive (true);
			break;

		case 1:
			AddContactPanel.gameObject.SetActive (true);
			ContactListPanel.gameObject.SetActive (false);
			break;


		}
	}
	private void Update() {
		//menu naviagation
		MenuContainer.anchoredPosition3D = Vector3.Lerp (MenuContainer.anchoredPosition3D, PanelPosition, 0.1f);
	}

	private void NavigateTo(int menuIndex) {
		switch (menuIndex) {
		//0 default case = main menu
		default:
		case 0:
			PanelPosition = Vector3.zero;
			break;

			// 1= Main Main Panel
		case 1:
			PanelPosition = Vector3.left * 1280;
			break;

		case 2:
			PanelPosition = Vector3.right * 1280;
			break;

			//Setting
		case 3:
			PanelPosition = Vector3.left * 2560;
			break;


		}
	}

	public bool getmainmenuactive(){
		return MainMenuPanel.gameObject.activeSelf;
	}
		

	public void onMainMenuPanelClick() {
		SwitchToMenu (0);
	}

	public void OnOptionClick() {
		SwitchToMenu (1);
		Debug.Log("option menu has been clicked");
	}

	public void OnHelpClick() {
		SwitchToMenu (2);
	}
	public void OnOptionsBackClick() {
		SwitchToMenu (0);
	}
	public void onGeneralSetting(){
		NavigateTo (2);
		Debug.Log("General setting");
	}
	public void onGeneralBack() {
		NavigateTo (0);
	}
	public void OnBackClick() {
		NavigateTo (0);
		Debug.Log ("Back has been clicked");
	}

	public void OnConfirmedClick() {
		NavigateTo (1);
		Debug.Log ("confirmed button has been clicked");
	}
	public void onRegisterClick() {
		NavigateTo (1);
		Debug.Log("register button has been clicked");
	}


	public void onModeSetting() {
		NavigateTo (1);
		Debug.Log ("Timer CLiked");
	}
	public void OnAddContactCLick() {
		SwitchContactPanel (1);
		Debug.Log("add contact button has been clicked");
}
	public void onAddContactPanelBack() {
		SwitchContactPanel (0);
		Debug.Log("add contact button has been clicked");
		
}
	public void onSignInClick(){
		SwitchToMenu(3);
}
	public void onMainBackClick() {
		SwitchToMenu (4);
	}

	public void onSignUplick() {
		SwitchToMenu (5);
	}

	public void onRegister1BackClick() {
		SwitchToMenu (6);
	}

	public void onRegister2Confirm() {
		SwitchToMenu (11);
	}

	public void onVerifyBack() {
		SwitchToMenu (12);
	}
	public void onVerifyConfirm(){
		NavigateTo (0);
		SwitchToMenu (3);
	}

	public void onRegister1Confirm() {
		NavigateTo (1);
	}

	public void onRegister2BackClick() {
		NavigateTo (0);
	}



	public void onVerifyBackClick() {
		NavigateTo (1);
	}
}
