using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToVideoScen : MonoBehaviour {

	public void onClickSwitchToS() {
        SceneManager.LoadScene("VideoTest", LoadSceneMode.Single);
    }
}
