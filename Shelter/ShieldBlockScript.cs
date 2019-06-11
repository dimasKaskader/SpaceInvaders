using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlockScript : MonoBehaviour {

    static int id = 0;
    public Sprite blockSprite;

    private void Start()
    {
        StartCoroutine(RandomAppearance(SheltersController.randomAppearing.Next(0, 128)/100F));
    }

    IEnumerator RandomAppearance(float time)
    {
        yield return new WaitForSeconds(time);
        //GetComponent<SpriteRenderer>().flipX = false;
        GetComponent<SpriteRenderer>().sprite = blockSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetInstanceID() != id)
            if (collision.name != "Alien(Clone)")
            {
                id = collision.gameObject.GetInstanceID();
                Destroy(collision.gameObject);
                System.Random a = new System.Random();
                foreach (var element in Physics2D.CircleCastAll(transform.position, 0.15F, new Vector2(0, 0)))
                {
                    if (a.Next(0, 5) < 4 && element.collider.name == "ShieldBlock(Clone)")
                        Destroy(element.collider.gameObject);
                }
            }
            else
                Destroy(this.gameObject);
    }
}
