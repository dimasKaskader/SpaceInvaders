using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{

    public int row, column;
    public AudioClip destructionSound, move1, move2, move3, move4;

    private static int destroyedNumber;

    public Sprite AlienR, AlienR1, AlienP, AlienP1, AlienG, AlienG1, ExplosionR, ExplosionP, ExplosionG;
    public static int amount;
    public static int nextFrameMove;
    private bool animationPhase = false;
    private int personalNumber;
    public static int personalNumberCounter;
    private static int audioPhase;
    public static int[,] alienArray;
    public bool thisCompleted;
    float randomIntensityPhase;
    static System.Random random;

    static bool gameOverWritten;

    void Start()
    {
        nextFrameMove = 0;
        gameOverWritten = false;
        thisCompleted = false;
        destroyedNumber = 55;
        amount = SpaceInvaders.HorizontalMax * SpaceInvaders.VerticalMax;
        personalNumberCounter++;
        personalNumber = personalNumberCounter;
        column = personalNumber;
        while (column > SpaceInvaders.HorizontalMax)
        {
            column -= SpaceInvaders.HorizontalMax;
        }
        column--;
        row = (personalNumber - 1) / SpaceInvaders.HorizontalMax;
        alienArray[row, column] = personalNumber;
        audioPhase = 1;
        if (personalNumber == 1)
            random = new System.Random();
        randomIntensityPhase = random.Next(0, 63) / 10F;
        if (personalNumber <= SpaceInvaders.HorizontalMax * 2)
        {
            GetComponent<SpriteRenderer>().sprite = AlienP;
            GetComponent<BoxCollider2D>().size = new Vector2(0.98F, 0.85F);
        }
        else if (personalNumber > SpaceInvaders.HorizontalMax * 2 && personalNumber <= (SpaceInvaders.HorizontalMax * 4))
        {
            GetComponent<SpriteRenderer>().sprite = AlienR;
            transform.localScale = new Vector3(0.5F, 0.5F, 1);
        }
        else 
        {
            GetComponent<SpriteRenderer>().sprite = AlienG;
            GetComponent<BoxCollider2D>().size = new Vector2(0.9F, 0.9F);
            transform.localScale = new Vector3(0.5F, 0.5F, 1);
            if (SpaceInvaders.lightingEffects)
            {
                GameObject light = new GameObject("Light");
                light.transform.SetParent(transform);
                light.transform.position = transform.position + new Vector3(0, -0.62F, -0.11F);
                Light svet = light.AddComponent<Light>();
                svet.range = 2F;
                svet.intensity = 8F;
                svet.color = new Color(0.329F, 0.780F, 1F, 1);
            }
        }
    }

    public static void AddScore(int scor)
    {
        SpaceInvaders.score += scor;
        GameObject.FindWithTag("Score").GetComponent<Text>().text = "Счет " + SpaceInvaders.score;
    }

    public void ChangeSprite()
    {
        if (animationPhase)
        {
            if (GetComponent<SpriteRenderer>().sprite == AlienR1)
            {
                GetComponent<SpriteRenderer>().sprite = AlienR;
            }
            if (GetComponent<SpriteRenderer>().sprite == AlienG1)
            {
                GetComponent<SpriteRenderer>().sprite = AlienG;
            }
            if (GetComponent<SpriteRenderer>().sprite == AlienP1)
            {
                GetComponent<SpriteRenderer>().sprite = AlienP;
            }
            animationPhase = false;
        }
        else
        {
            if (GetComponent<SpriteRenderer>().sprite == AlienR)
            {
                GetComponent<SpriteRenderer>().sprite = AlienR1;
            }
            if (GetComponent<SpriteRenderer>().sprite == AlienG)
            {
                GetComponent<SpriteRenderer>().sprite = AlienG1;
            }
            if (GetComponent<SpriteRenderer>().sprite == AlienP)
            {
                GetComponent<SpriteRenderer>().sprite = AlienP1;
            }
            animationPhase = true;
        }
    }

    public void PlayMoveSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioPhase == 1)
            audioSource.clip = move1;
        if (audioPhase == 2)
            audioSource.clip = move2;
        if (audioPhase == 3)
            audioSource.clip = move3;
        if (audioPhase == 4)
            audioSource.clip = move4;
        audioSource.Play();
        audioPhase++;
        if (audioPhase == 5)
            audioPhase = 1;
    }


    public static void InvertAlienNumbers()
    {
        for (int j = 0; j < SpaceInvaders.VerticalMax; j++)
        {
            int invertAmount = 0;
            for (int i = 0; i < SpaceInvaders.HorizontalMax; i++)
            {
                if (alienArray[j, i] != -1)
                {
                    invertAmount++;
                }
            }
            int leftElem = 0, rightElem = SpaceInvaders.HorizontalMax - 1;
            for (int i = 0; i < invertAmount / 2; i++)
            {
                while (alienArray[j, leftElem] == -1)
                {
                    leftElem++;
                }
                while (alienArray[j, rightElem] == -1)
                {
                    rightElem--;
                }
                int buff = alienArray[j, leftElem];
                alienArray[j, leftElem] = alienArray[j, rightElem];
                alienArray[j, rightElem] = buff;
                leftElem++;
                rightElem--;
            }
        }
    }

    public static void SetAlienArray()
    {
        alienArray = new int[5, 11];
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 11; j++)
                alienArray[i, j] = -1;
    }
    void RewriteAlienArray()
    {
        for (int i = 0; i < SpaceInvaders.VerticalMax; i++)
            for (int j = 0; j < SpaceInvaders.HorizontalMax; j++)
                if (alienArray[i, j] > alienArray[row, column] && alienArray[i, j] != -1)
                    alienArray[i, j]--;
        alienArray[row, column] = -1;
    }
    void Update()
    {
        if (personalNumber > destroyedNumber)
        {
            personalNumber--;
        }
        if (personalNumber == amount)
        {
            destroyedNumber = 55;
        }

        Light light = GetComponentInChildren<Light>();
        if (light)
        {
            light.intensity = Mathf.Sin(Time.time * 3 + randomIntensityPhase) * 4 + 4;
        }

        if (transform.position.y < -3.05 && gameOverWritten == false)
        {
            gameOverWritten = true;
            GameObject.Find("LevelController").GetComponent<LevelController>().RemoveLife(true);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "Bullet(Clone)")
        {
            if (amount > 1)
            {
                amount--;
            }
            else
            {
                GameObject.FindWithTag("LevelController").GetComponent<LevelController>().CallNextLevel();
                personalNumberCounter = 0;
            }
            RewriteAlienArray();
            destroyedNumber = personalNumber;
            GameObject.Find("LevelController").GetComponent<AlienController>().DecreaseAliens(personalNumber - 1);
            Destroy(this);
            GameObject.Find("LevelController").GetComponent<AudioSource>().Play();
            if (GetComponent<SpriteRenderer>().sprite == AlienR1 || GetComponent<SpriteRenderer>().sprite == AlienR)
            {
                SpaceInvaders.score += 20;
                GetComponent<SpriteRenderer>().sprite = ExplosionR;
            }
            if (GetComponent<SpriteRenderer>().sprite == AlienP1 || GetComponent<SpriteRenderer>().sprite == AlienP)
            {
                SpaceInvaders.score += 10;
                GetComponent<SpriteRenderer>().sprite = ExplosionP;
            }
            if (GetComponent<SpriteRenderer>().sprite == AlienG1 || GetComponent<SpriteRenderer>().sprite == AlienG)
            {
                SpaceInvaders.score += 30;
                GetComponent<SpriteRenderer>().sprite = ExplosionG;
            }
            GameObject.FindWithTag("Score").GetComponent<Text>().text = "Счет " + SpaceInvaders.score;
            transform.localScale = new Vector3(0.6F, 0.6F, 1);
            Destroy(gameObject, 0.5F);
        }
        if (coll.name == "Right" || coll.name == "Left")
        {
            nextFrameMove = 1;
        }
    }
}
