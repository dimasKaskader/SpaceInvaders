using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoScript : MonoBehaviour {

    public AudioClip destructionSound;
    public Sprite sprite100;
    int direction;
    Color[] nextColor = new Color[2];
    System.Random a = new System.Random();
    
    void Start()
    {
        if (transform.position.x > 0)
            direction = -1;
        else
            direction = 1;
        foreach(Light a in GetComponentsInChildren<Light>())
        {
            if(a.gameObject.name == "Left1" || a.gameObject.name == "Left2")
            {
                a.color = Color.yellow;
            }
            if (a.gameObject.name == "Right1" || a.gameObject.name == "Right2")
            {
                a.color = Color.cyan;
            }
        }
    }
    
    Color randomColor()
    {
        int rand = a.Next(0, 6);
        Color color = Color.black;
        switch (rand)
        {
            case 0: color = Color.blue; break;
            case 1: color = Color.cyan; break;
            case 2: color = Color.green; break;
            case 3: color = Color.magenta; break;
            case 4: color = Color.red; break;
            case 5: color = Color.yellow; break;
        }
        return color;
    }

    void Update ()
    {
        if(transform.position.x < -10.3F || transform.position.x > 10.3F)
        {
            Destroy(gameObject);
            LevelController.ifufo = false;
        }
        transform.Translate(4F * Time.deltaTime * direction, 0, 0);
        foreach (Light a in GetComponentsInChildren<Light>())
        {
            if((a.gameObject.name == "Left1" || a.gameObject.name == "Left2") && a.color == nextColor[0])
                nextColor[0] = randomColor();
            if ((a.gameObject.name == "Right1" || a.gameObject.name == "Right2") && a.color == nextColor[1])
                nextColor[1] = randomColor();
            if (a.gameObject.name == "Left1" || a.gameObject.name == "Left2")
                a.color = Color.Lerp(a.color, nextColor[0], Time.deltaTime*15);
            if (a.gameObject.name == "Right1" || a.gameObject.name == "Right2")
                a.color = Color.Lerp(a.color, nextColor[1], Time.deltaTime*15);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bullet(Clone)")
        {
            GetComponent<AudioSource>().clip = destructionSound;
            GetComponent<AudioSource>().Play();

            foreach (Transform a in GetComponentsInChildren<Transform>()) //уничтожение лампочек на нло
            {
                if (a.gameObject != gameObject)
                    Destroy(a.gameObject);
            }

            GetComponent<SpriteRenderer>().sprite = sprite100;
            GetComponent<Transform>().localScale = new Vector3(1, 1, 1);

            Destroy(this);
            Destroy(gameObject, 1.5F);
            LevelController.ifufo = false;
            Alien.AddScore(100);
        }
    }
}
