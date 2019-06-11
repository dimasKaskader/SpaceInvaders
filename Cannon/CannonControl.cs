using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CannonControl : MonoBehaviour
{
	bool notAndroid;
    private int collision = 0;
    public GameObject bullet;
    public AudioClip destructionSound;
    int instanceID;

    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
            notAndroid = true;
    }
    void CannonMovement()
    {
        float speed = Input.GetAxis("Horizontal") * 8F;
        if (speed == 0)
            speed = TouchButton.horizontalTouchAxis * 8F;
        else
            GetComponent<Animator>().Play("move");
        speed *= Time.deltaTime;
        if ((collision == 1 && speed > 0) || (collision == 2 && speed < 0) || collision == 0)
        {
            transform.Translate(speed, 0, 0);
        }
    }
    void CannonShot()
    {
        if ((Input.GetButtonDown("Fire1") && notAndroid) || TouchButton.fire1TouchAxis)
            if (!GameObject.FindWithTag("Bullet") && Time.timeScale != 0)
            {
                Vector3 bulletposition = transform.position + new Vector3(0, 0.25F, 0); ;
                Instantiate(bullet, bulletposition, transform.rotation);
                GetComponent<AudioSource>().Play();
            }
    }
    
    void Update ()
    {
        CannonMovement();
        CannonShot();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "LeftCannon")
        {
            collision = 1;
        }
        if (coll.name == "RightCannon")
        {
            collision = 2;
        }
        if (coll.name == "Alien Bullet(Clone)" && coll.gameObject.GetInstanceID() != instanceID)
        {
            instanceID = coll.gameObject.GetInstanceID();
            GameObject.FindWithTag("LevelController").GetComponent<LevelController>().RemoveLife();
            GetComponent<AudioSource>().clip = destructionSound;
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().Play("broken");
            Destroy(this);
            Destroy(gameObject, 1F);
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.name == "LeftCannon" || coll.name == "RightCannon")
            collision = 0;
    }
}
