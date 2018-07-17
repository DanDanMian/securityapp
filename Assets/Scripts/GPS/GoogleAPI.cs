using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleAPI : MonoBehaviour {

    public static Texture2D texture;

    public RawImage img;
    string url;

    float lat;
    float lon;

    LocationInfo li;

    public int zoom = 14;
    public int mapWidth = 300;
    public int mapHeight = 300;

    public enum mapType {roadmap, satellite, hybrid, terrian}
    public mapType mapSelected;
    public int scale;

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enabled GPS");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determin device location");
            yield break;
        }

        //use gps to get lattitude and longitude
        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;

        //use google map api to show map
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
                "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale +
                "&maptype=" + mapSelected +
			"&markers=color:blue%7Clabel:S%7C"+lat.ToString()+","+lon.ToString()+"&key = AIzaSyC2_-T6r5P9mCTUdM5wacmo7RKe7ixZZyQ";
        WWW www = new WWW(url);
        yield return www;
        texture = www.texture;
        //img.texture = texture = www.texture;
        //img.SetNativeSize();
    }


	// Use this for initialization
	void Start () { 
        //img = gameObject.GetComponent<RawImage>();
        StartCoroutine(StartLocationService());
	}
	
}
