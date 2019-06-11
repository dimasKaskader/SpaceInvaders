using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{
    public Sprite bullet11, bullet12, bullet13, bullet14;
    public Sprite bullet21, bullet22, bullet23, bullet24;

    System.Random random;
    void Start()
    {
        random = new System.Random();
        if(random.Next(0,2) == 0)
            gameObject.GetComponent<SpriteRenderer>().sprite = bullet11;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = bullet21;
        StartCoroutine(SpriteChanger());
    }
    
    void Update()
    {
        transform.Translate(0, -5F * Time.deltaTime, 0);
    }

    IEnumerator SpriteChanger()
    {
        while(true)
        {
            if(gameObject.GetComponent<SpriteRenderer>().sprite == bullet11)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bullet12;
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == bullet12)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bullet13;
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == bullet13)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bullet14;
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == bullet14)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bullet11;
            }


            if (gameObject.GetComponent<SpriteRenderer>().sprite == bullet21)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bullet22;
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == bullet22)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bullet23;
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == bullet23)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bullet24;
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == bullet24)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bullet21;
            }
            yield return new WaitForSeconds(0.1F);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name != "Alien(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
