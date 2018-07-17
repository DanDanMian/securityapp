using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

    public static bool camAvailable;
    public static WebCamTexture backCam;
    //private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    public TakePhotoScript takephoto;

	// Use this for backCam initialization
	void Start () {

        //defaultBackground = background.texture;

        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing) // need to add ! for production
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(backCam == null)
        {
            Debug.Log("Unable to find a back camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;
        camAvailable = true;

        takephoto = GameObject.Find ("clickTakingPhotos").GetComponent<TakePhotoScript> ();
        StartCoroutine (TakePhoto ());
	}
	
	// Update is called once per frame
	void Update () {
        if (!camAvailable)
            return;

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f: 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
	}

    IEnumerator TakePhoto() {
        yield return new WaitForSeconds(2.0f);
        takephoto.OnClickTakePhotos ();
    }
}