using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class FadePreloader : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float loadTime;
	private float minimumLogoTime = 3.0f;


	private void Start() {
		//grab the only CanavasGroup in the scene
		fadeGroup = FindObjectOfType <CanvasGroup> ();

		//start with a white screen
		fadeGroup.alpha = 1;
		//preload the game

		if (Time.time < minimumLogoTime)
			loadTime = minimumLogoTime;
		else
			loadTime = Time.time;   
	}

	private void Update() {
		//fade in 
		if(Time.time < minimumLogoTime){
			fadeGroup.alpha = 1 - Time.time;
		}
		//fade out
		if(Time.time > minimumLogoTime && loadTime !=0) {
			fadeGroup.alpha = Time.time - minimumLogoTime;
			if(fadeGroup.alpha >= 1) {
				SceneManager.LoadScene ("LoginMenu");
			}
		}
	}
}
