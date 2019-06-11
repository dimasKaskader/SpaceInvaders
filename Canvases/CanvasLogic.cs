using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasLogic : MonoBehaviour
{

    public void ChangeDrawBackground()
    {
        if (SpaceInvaders.drawBackground)
        {
            SpaceInvaders.drawBackground = false;
            PlayerPrefs.SetInt("Background", 0);
            PlayerPrefs.Save();
        }
        else
        {
            SpaceInvaders.drawBackground = true;
            PlayerPrefs.SetInt("Background", 1);
            PlayerPrefs.Save();
        }

        GameObject backgroundButton = GameObject.Find("BackgroundButton");

        if (SpaceInvaders.drawBackground)
        {
            if (backgroundButton.GetComponentInChildren<Text>())
                backgroundButton.GetComponentInChildren<Text>().text = "Фон: вкл";
        }
        else
        {
            if (backgroundButton.GetComponentInChildren<Text>())
                backgroundButton.GetComponentInChildren<Text>().text = "Фон: выкл";
        }
    }

    public void ChangeLightingEffects()
    {
        if (SpaceInvaders.lightingEffects)
        {
            SpaceInvaders.lightingEffects = false;
            PlayerPrefs.SetInt("LightingEffects", 0);
            PlayerPrefs.Save();
        }
        else
        {
            SpaceInvaders.lightingEffects = true;
            PlayerPrefs.SetInt("LightingEffects", 1);
            PlayerPrefs.Save();
        }

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

    public void ChangeHorizontalMax(int a)
    {
        SpaceInvaders.HorizontalMax = a;
        PlayerPrefs.SetInt("HorizontalMax", a);
        PlayerPrefs.Save();
    }

    public void ChangeVerticalMax(int a)
    {
        SpaceInvaders.VerticalMax = a;
        PlayerPrefs.SetInt("VerticalMax", a);
        PlayerPrefs.Save();
    }
    public void ChangeLifes(int a)
    {
        SpaceInvaders.lifes = a;
        PlayerPrefs.SetInt("Lifes", a);
        PlayerPrefs.Save();
    }

    public void CallSettings()
    {
        SceneManager.LoadSceneAsync("Settings Menu");
        SpaceInvaders.SetDefault();
    }

    public void CallRecords()
    {
        SceneManager.LoadSceneAsync("Records Menu");
    }

    public void CallMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        Time.timeScale = 1;
    }
    public void CallGameScene()
    {
        SceneManager.LoadSceneAsync("Game Scene");
        SpaceInvaders.level = 0;
    }

    public void StartAgain()
    {
        SceneManager.LoadSceneAsync("Game Scene");
        Time.timeScale = 1;
        Cursor.visible = false;
        SpaceInvaders.level = 0;
    }
    public void ContinueFromPause()
    {
        GameObject ufo = GameObject.Find("UFO(Clone)");
        if (ufo)
        {
            ufo.GetComponent<AudioSource>().Play();
        }
        Destroy(GameObject.FindWithTag("Canvas"));
        GameObject.FindWithTag("LevelController").GetComponent<LevelController>().pause = false;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
