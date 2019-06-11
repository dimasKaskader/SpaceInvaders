using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingShelter : MonoBehaviour {
    
	void Update ()
    {
        GetComponentInChildren<Light>().intensity = SheltersController.lightIntensity;
	}
}
