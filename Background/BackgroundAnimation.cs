using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    public Sprite back1, back2;
    public GameObject background;

	void Start ()
    {
        SpaceInvaders.SetDefault();
        if (SpaceInvaders.drawBackground)
        {
            background = Instantiate(background);
            StartCoroutine(SpriteChanger());
        }
	}

    IEnumerator SpriteChanger()
    {
        while(true)
        {
            if (background.GetComponent<SpriteRenderer>().sprite == back1)
                background.GetComponent<SpriteRenderer>().sprite = back2;
            else
                background.GetComponent<SpriteRenderer>().sprite = back1;
            yield return new WaitForSeconds(1F);
        }
    }
}
