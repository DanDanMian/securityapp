using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSText : MonoBehaviour {
    public Text coordniates;

    private void Update()
    {
        coordniates.text = "Lat:" + GPS.latitude.ToString() + "   Lon:" + GPS.longtitude.ToString();
    }
}
