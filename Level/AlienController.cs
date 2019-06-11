using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour {

    public GameObject[] aliens;
    private float DelayBetweenMoves;
    private float speed;
    private int direction;
    private float time;
    public static bool countingAllowed;
    float bulletSpawnTime;
    private float bulletTime;
    System.Random a;
    public GameObject alienBullet;
    public GameObject alien;

    int moveDownAtStartAmount;
    
    public Material noLightingMaterial;
    public Material lightingMaterial;

    void Start()
    {
        SpaceInvaders.SetDefault();
        if (SpaceInvaders.lightingEffects)
            alien.GetComponent<SpriteRenderer>().material = lightingMaterial;
        else
            alien.GetComponent<SpriteRenderer>().material = noLightingMaterial;

        moveDownAtStartAmount = 16 + SpaceInvaders.level;
        speed = 1;
        InstantiateAliens();
        DelayBetweenMoves = 0.01F;
        direction = 1;
        time = 0;
        countingAllowed = true;
        a = new System.Random();
        bulletSpawnTime = a.Next(2, 9) / 10F;
    }
	
    private void InstantiateAliens()
    {
        Alien.personalNumberCounter = 0;
        Alien.SetAlienArray();

        alien.transform.position = new Vector3(-SpaceInvaders.HorizontalMax/2 * 0.9F, 6.4F, 0);

        aliens = new GameObject[SpaceInvaders.HorizontalMax * SpaceInvaders.VerticalMax];
        int index = 0;
        for (int i = 0; i < SpaceInvaders.VerticalMax; i++)
            for (int j = 0; j < SpaceInvaders.HorizontalMax; j++)
            {
                aliens[index] = Instantiate(alien, alien.transform.position + new Vector3(j * 0.9F, 0.7F * i, 0), alien.transform.rotation); //Vector3(j * 1.1F, 0.7F * i, 0)
                index++;
            }
    }

    private void BulletSpawner()
    {
        if (bulletTime >= bulletSpawnTime)
        {
            int randomAlien = a.Next(0, Alien.amount);
            GameObject[] aliensList = GameObject.FindGameObjectsWithTag("Alien");
            int i = 0;
            foreach (GameObject x in aliensList)
            {
                if (i == randomAlien)
                {
                    Vector3 alienBulletPosition = x.transform.position;
                    while (Physics2D.Raycast(alienBulletPosition, new Vector3(0, -1, 0)).collider.name == "Alien(Clone)")
                    {
                        alienBulletPosition += new Vector3(0, -0.4F, 0);
                    }
                    Instantiate(alienBullet, alienBulletPosition, x.transform.rotation);
                    bulletSpawnTime = a.Next(2, 9) / 10F;
                    break;
                }
                i++;
            }
            bulletTime = 0;
        }
        bulletTime += Time.deltaTime;
    }

    void Update ()
    {
        AlienMovement();
        BulletSpawner();
    }

    public void DecreaseAliens(int destroyedIndex) //когда алиен уничтожен, опускает всех в массиве, кто выше на 1
    {
        int previousElement = destroyedIndex;
        for(int i = destroyedIndex + 1; i < SpaceInvaders.HorizontalMax * SpaceInvaders.VerticalMax; i++)
        {
            if (aliens[i] != null)
            {
                aliens[previousElement] = aliens[i];
                previousElement = i;
            }
        }
        aliens[previousElement] = null;
    }

    void AlienMovement()
    {
        if (time >= DelayBetweenMoves && aliens[0])
        {
            countingAllowed = false;
            if (moveDownAtStartAmount == 0)
            {
                DelayBetweenMoves = 0.008F * Alien.amount / speed;
                if (Alien.nextFrameMove == 1)
                    StartCoroutine(MoveAllWithDelay(2F, true));
                else
                    StartCoroutine(MoveAllWithDelay(2F, false));
            }
            else
            {
                DelayBetweenMoves = 0.05F;
                StartCoroutine(MoveAllWithDelay(SpaceInvaders.HorizontalMax, true));
                moveDownAtStartAmount--;
                if (moveDownAtStartAmount == 0)
                {
                    DelayBetweenMoves = 0.008F * Alien.amount / speed;
                    System.Random a = new System.Random();
                    if (a.Next(0, 2) == 0)   
                    {
                        Alien.InvertAlienNumbers();
                    }
                    else
                    {
                        direction = -direction;
                    }
                }
            }
            
            time = 0;
        }
        if (countingAllowed)
            time += Time.deltaTime;
    }

    IEnumerator MoveAllWithDelay(float amountPerFrame, bool downMove)
    {
        aliens[0].GetComponent<Alien>().PlayMoveSound();
        if (downMove && moveDownAtStartAmount == 0)
        {
            direction = -direction;
            speed += 0.05F;
            Alien.nextFrameMove = 0;
            Alien.InvertAlienNumbers();
        }
        int alreadyMovedAmount = 0;
        for (int row = 0; row < SpaceInvaders.VerticalMax; row++)
            for (int column = 0; column < SpaceInvaders.HorizontalMax; column++)
            {
                if (row == SpaceInvaders.VerticalMax - 1 && column == SpaceInvaders.HorizontalMax - 1)
                {
                    countingAllowed = true;
                }

                int alienIndex = Alien.alienArray[row, column];

                if (alienIndex != -1)
                {
                    if (aliens[alienIndex - 1].GetComponent<Alien>().thisCompleted == false)
                    {
                        if (downMove)
                            aliens[alienIndex - 1].transform.Translate(0, -0.35F, 0);
                        else
                            aliens[alienIndex - 1].transform.Translate(0.2F * direction, 0, 0);

                        aliens[alienIndex - 1].GetComponent<Alien>().thisCompleted = true;

                        aliens[Alien.alienArray[row, column] - 1].GetComponent<Alien>().ChangeSprite();
                        alreadyMovedAmount++;
                    }
                }
                if (alreadyMovedAmount >= amountPerFrame)
                {
                    alreadyMovedAmount = 0;
                    yield return new WaitForFixedUpdate();
                }
            }

        for (int row = 0; row < SpaceInvaders.VerticalMax; row++)
            for (int column = 0; column < SpaceInvaders.HorizontalMax; column++)
            {
                if (Alien.alienArray[row, column] != -1)
                {
                    if (aliens[Alien.alienArray[row, column] - 1].GetComponent<Alien>().thisCompleted == true)
                    {
                        aliens[Alien.alienArray[row, column] - 1].GetComponent<Alien>().thisCompleted = false;
                    }
                    else
                    {
                        /*if (downMove)
                            aliens[Alien.alienArray[row, column] - 1].transform.Translate(0, -0.5F, 0);
                        else*/
                        if (!downMove)
                        {
                            aliens[Alien.alienArray[row, column] - 1].transform.Translate(0.2F * direction, 0, 0);
                            aliens[Alien.alienArray[row, column] - 1].GetComponent<Alien>().ChangeSprite();
                        }
                    }

                }
            }
    }
}
