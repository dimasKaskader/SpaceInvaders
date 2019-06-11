using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    void Update ()
    {
        transform.Translate(0, 10F*Time.deltaTime, 0);
        if(transform.position.y >= 5F)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "Alien(Clone)" || coll.name == "Top" || coll.name == "UFO(Clone)" || coll.name == "Alien Bullet(Clone)")
            Destroy(this.gameObject);
    }
}
