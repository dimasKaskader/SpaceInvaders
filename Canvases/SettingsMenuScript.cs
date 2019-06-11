using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour {
	void Start ()
    {
        GameObject backgroundButton = GameObject.Find("BackgroundButton");
		if(SpaceInvaders.drawBackground)
        {
            if (backgroundButton.GetComponentInChildren<Text>())
                backgroundButton.GetComponentInChildren<Text>().text = "Фон: вкл";
        }
        else if (backgroundButton.GetComponentInChildren<Text>())
            backgroundButton.GetComponentInChildren<Text>().text = "Фон: выкл";

        GameObject lightingButton = GameObject.Find("LightingEffects");
        if (SpaceInvaders.lightingEffects)
        {
            if (lightingButton.GetComponentInChildren<Text>())
                lightingButton.GetComponentInChildren<Text>().text = "Эффекты свечения: вкл";
        }
        else
        {
            if (lightingButton.GetComponentInChildren<Text>())
                lightingButton.GetComponentInChildren<Text>().text = "Эффекты свечения: выкл";
        }
    }
	
	
	void Update ()
    {
		if(Input.GetButtonDown("Cancel"))
        {
             SceneManager.LoadSceneAsync("Main Menu");
        }
	}
}
