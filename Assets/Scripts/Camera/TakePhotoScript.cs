using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class TakePhotoScript : MonoBehaviour {

	public static float delay = 0.3f;

	int width, height;
	Texture2D tex1, tex2, tex3;

	void Start() {
		// Create a texture the size of the screen, RGB24 format
		width = Screen.width;
		height = Screen.height;
		tex1 = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex2 = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex3 = new Texture2D(width, height, TextureFormat.RGB24, false);
	}

	public void OnClickTakePhotos()
	{
		StartCoroutine(UploadPNG());
	}

	IEnumerator UploadPNG()
	{
		yield return null;
		GameObject.Find("Button").transform.localScale = new Vector3(0, 0, 0);
		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();

		// Read screen contents into the texture
		tex1.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex1.Apply();

		// Encode texture into JPG
        byte[] bytes1 = tex1.EncodeToJPG();
		Destroy(tex1);
		//debug tex
		Debug.Log("texture1 created");

		yield return new WaitForSeconds(delay);
		yield return new WaitForEndOfFrame();

		// Read screen contents into the texture
		tex2.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex2.Apply();

		// Encode texture into JPG
        byte[] bytes2 = tex2.EncodeToJPG();
		Destroy(tex2);
		//debug tex
		Debug.Log("texture2 created");

		yield return new WaitForSeconds(delay);
		yield return new WaitForEndOfFrame();

		// Read screen contents into the texture
		tex3.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex3.Apply();

		// Encode texture into JPG
        byte[] bytes3 = tex3.EncodeToJPG();
		Destroy(tex3);
		//debug tex
		Debug.Log("texture3 created");

		//close camera
		CameraScript.backCam.Stop();
		CameraScript.camAvailable = false;

		//Show UI after we're done
		GameObject.Find("Button").transform.localScale = new Vector3(1, 1, 1);

		List<byte[]> data = new List<byte[]> ();
		data.Add (bytes1);
		data.Add (bytes2);
		data.Add (bytes3);
		//Email.SendMail(data);
        Sending.Send(data);
		SceneManager.LoadScene("Active", LoadSceneMode.Single);
        //SceneManager.LoadScene("VideoTest", LoadSceneMode.Single);
        yield return null;
	}
}