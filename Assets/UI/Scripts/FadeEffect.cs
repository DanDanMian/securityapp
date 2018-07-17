using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour {
	private CanvasGroup fadeGroup;
	private float fadeInspeed = 0.33f;

	private void Start() {
		//grab the only CanavasGroup in the scene
		fadeGroup = FindObjectOfType <CanvasGroup> ();

		//start with a white screen
		fadeGroup.alpha = 1;
		//preload the game
	}
	private void Update()
	{
		//fade in
		fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInspeed;

	}

}
