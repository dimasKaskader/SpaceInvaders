using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceInvaders : MonoBehaviour
{
    public static int HorizontalMax, VerticalMax;
    public static int score, lifes, currentLifes;
    public static int level;
    public static bool drawBackground;
    public static bool lightingEffects;
    public static void SetDefault()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        drawBackground = (PlayerPrefs.GetInt("Background", 1) == 1 ? true : false);
        lightingEffects = (PlayerPrefs.GetInt("LightingEffects", 0) == 1 ? true : false);
        if (HorizontalMax == 0)
        {
            HorizontalMax = PlayerPrefs.GetInt("HorizontalMax", 11);
        }
        if (VerticalMax == 0)
        {
            VerticalMax = PlayerPrefs.GetInt("VerticalMax", 5);
        }
        if (level == 0)
        {
            level = 1;
        }
        if (lifes <= 0 || lifes > 5)
        {
            lifes = PlayerPrefs.GetInt("Lifes", 3);
        }
        if (currentLifes > 5)
        {
            currentLifes = 5;
        }
        if (level == 1)
        {
            currentLifes = lifes;
            score = 0;
        }
    }
}
