using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public GameObject ufo, cannon, mainUI, GameOverCanvas;
    public GameObject cannonLifes;
    private int randomTime;
    private System.Random a;
    private bool cannonDestroyed;
    private bool banPause;
    private float ufoTime = 0;
    public static bool ifufo;

    public GameObject pauseCanvas;
    public bool pause;


    void Start()
    {
        Time.timeScale = 1;
        banPause = false;
        Cursor.visible = false;
        ifufo = false;
        cannonDestroyed = false;
        //SpaceInvaders.SetDefault();
        Instantiate(cannon);
        a = new System.Random();
        randomTime = a.Next(10, 15);
        Instantiate(mainUI);
        AddLifes(SpaceInvaders.currentLifes);
        pause = false;
        GameObject.FindWithTag("Score").GetComponent<Text>().text = "счет " + SpaceInvaders.score.ToString();
    }

    public void RemoveLife(bool deleteAll = false)
    {
        if (!deleteAll)
        {
            SpaceInvaders.currentLifes--;
            if (SpaceInvaders.currentLifes > 0)
                Destroy(GameObject.FindGameObjectsWithTag("Lifes")[SpaceInvaders.currentLifes]);
            cannonDestroyed = true;
            GameObject.FindGameObjectsWithTag("Lifes")[0].GetComponent<Text>().text = "Жизни " + SpaceInvaders.currentLifes;
        }
        if (SpaceInvaders.currentLifes == 0 || deleteAll)
        {
            Time.timeScale = 0;
            ufo = GameObject.Find("UFO(Clone)");
            if (ufo)
            {
                ufo.GetComponent<AudioSource>().Stop();
            }

            StartCoroutine(WriteGameOverCoroutine());
            StartCoroutine(InstantiateGameOverCanvas());
            banPause = true;
        }
    }

    IEnumerator WriteGameOverCoroutine()
    {
        yield return new WaitForSecondsRealtime(2F);
        string gameOver = "игра окончена";
        for (int i = 0; i < gameOver.Length; i++)
        {
            GameObject.Find("Game Over").GetComponent<Text>().text += gameOver[i];
            yield return new WaitForSecondsRealtime(0.1F);
        }
    }

    IEnumerator InstantiateGameOverCanvas()
    {
        yield return new WaitForSecondsRealtime(5F);
        Instantiate(GameOverCanvas);
    }

    public void AddLifes(int lifes)
    {
        int counter = 0;

        while (counter != lifes - 1)
        {
            Instantiate(cannonLifes, cannonLifes.GetComponent<RectTransform>().anchoredPosition + new Vector2(counter * 90, 0), Quaternion.identity).transform.SetParent(GameObject.FindWithTag("MainUI").GetComponent<RectTransform>(), false);
            counter++;
        }
        GameObject.FindGameObjectsWithTag("Lifes")[0].GetComponent<Text>().text = "Жизни " + SpaceInvaders.currentLifes;
    }

    public void DeleteAllGameObjects()
    {
        Destroy(GameObject.FindWithTag("Cannon"));
        Destroy(GameObject.FindWithTag("Bullet"));
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("AlienBullet"))
            Destroy(x);
        Destroy(GameObject.FindWithTag("MainUI"));
        Destroy(GameObject.Find("UFO(Clone)"));
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Alien"))
            Destroy(x);
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Block"))
            Destroy(x);
        Destroy(GameObject.FindWithTag("LevelController"));
    }   //удалить все объекты со сцены

    public void CallNextLevel()
    {
        SpaceInvaders.level++;
        SpaceInvaders.currentLifes++;
        Time.timeScale = 0;
        StartCoroutine(LoadLevelWithDelay());
    }

    IEnumerator LoadLevelWithDelay()
    {
        yield return new WaitForSecondsRealtime(2F);
        SceneManager.LoadSceneAsync("Game Scene");
    }

    private void UfoSpawner()
    {
        if (ufoTime >= randomTime)
        {
            if (a.Next(0, 2) == 0)
            {
                Instantiate(ufo, new Vector3(-10.3F, 4.4F, 0), Quaternion.identity);
            }
            else
                Instantiate(ufo);
            ifufo = true;
            ufoTime = 0;
            randomTime = a.Next(10, 15);
        }
        if (!ifufo)     //время считается только когда НЛО пропало
        {
            ufoTime += Time.deltaTime;
        }
    }
    private void CannonRespawner()
    {
        if (cannonDestroyed)
        {
            cannonDestroyed = false;
            StartCoroutine(RespawnCanon());
        }
    }

    IEnumerator RespawnCanon()
    {
        yield return new WaitForSeconds(2F);
        Instantiate(cannon);
    }

    void PauseGame()
    {
        if (!banPause)
            if (Input.GetButtonDown("Cancel") && pause == false)
            {
                Instantiate(pauseCanvas);
                Time.timeScale = 0;
                pause = true;
                GameObject ufo = GameObject.Find("UFO(Clone)");
                if (ufo)
                {
                    ufo.GetComponent<AudioSource>().Pause();
                }
            }
            else if (Input.GetButtonDown("Cancel") && pause == true)
            {
                Time.timeScale = 1;
                pause = false;
                Destroy(GameObject.FindWithTag("Canvas"));
                Cursor.visible = false;
                GameObject ufo = GameObject.Find("UFO(Clone)");
                if (ufo)
                {
                    ufo.GetComponent<AudioSource>().Play();
                }
            }
    }               //проверка постановки на паузу

    void Update()
    {
        UfoSpawner();
        CannonRespawner();
        PauseGame();
    }
}
