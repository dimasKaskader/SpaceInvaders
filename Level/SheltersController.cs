using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheltersController : MonoBehaviour {

    int[,] shelter = {
            { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            { 0,1,1,1,1,1,2,1,1,1,1,1,1,2,1,1,1,1,1,0},
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,2,1,1,1,0,0,0,0,1,1,1,2,1,1,1,1},
            { 1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1}};
    public GameObject block;
    public Material noLightingMaterial;
    public Material lightingMaterial;
    
    void Start ()
    {
        SpaceInvaders.SetDefault();
        if(SpaceInvaders.lightingEffects)
        {
            block.GetComponent<SpriteRenderer>().material = lightingMaterial;
        }
        else
            block.GetComponent<SpriteRenderer>().material = noLightingMaterial;
        randomAppearing = new System.Random();
        float x = -6.6F, y = -2.25F;
        for (int k = 0; k < 4; k++)
        {
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (shelter[i, j] > 0)
                    {
                        GameObject thisBlock = Instantiate(block, new Vector3(x, y, 0), Quaternion.identity);
                        if (shelter[i, j] == 2 && SpaceInvaders.lightingEffects)
                        {
                            GameObject light = new GameObject("Light");
                            light.transform.SetParent(thisBlock.transform);
                            light.transform.position = thisBlock.transform.position + new Vector3(0,0,-0.32F);
                            Light svet = light.AddComponent<Light>();
                            svet.range = 5F;
                            svet.intensity = 0.8F;
                            svet.color = new Color(0.133F, 0.694F, 0.298F, 1);

                            thisBlock.AddComponent<LightingShelter>();
                        }
                    }
                    x += 0.075F;
                }
                y -= 0.075F;
                x -= 20*0.075F;
            }
            x += 3.9F;
            y = -2.25F;
        }
	}

    public static System.Random randomAppearing; 
    public static float lightIntensity;
    // Update is called once per frame
    void Update ()
    {
        /*
        if (lightIntensity <= 0.81F)
            targetIntensity = 1.2F;
        if (lightIntensity >= 1.19F)
            targetIntensity = 0.5F;
        lightIntensity = Mathf.Lerp(lightIntensity, targetIntensity, Time.deltaTime);
        */
        lightIntensity = Mathf.Sin(Time.time)/3+1F;
    }
}
