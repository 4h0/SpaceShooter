using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text[] uiText;
    public GameObject[] asteroidEnemy;

    private PlayerController playerControllerReference;

    public bool gameEnd;
    public int score;

    private int whichAsteroid;

    private void Awake()
    {
        playerControllerReference = FindObjectOfType<PlayerController>();

        for (int counter = 0; counter < uiText.Length; counter++)
        {
            uiText[counter].enabled = false;
        }

        gameEnd = false;
        score = 0;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());

        uiText[0].text = "Player Score: " + score;
        uiText[1].text = "Press R to Restart";
        uiText[2].text = "Game Over";

        uiText[0].enabled = true;
    }

    private void Update()
    {
        uiText[0].text = "Player Score: " + score;

        if (gameEnd)
        {
            StartCoroutine(SlowGame());

            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;

                SceneManager.LoadScene("SampleScene");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SlowGame()
    {
        uiText[1].enabled = true;
        uiText[2].enabled = true;

        yield return new WaitForSeconds(.1f);

        Time.timeScale = .001f;
    }

    IEnumerator SpawnEnemy()
    {
        while (!gameEnd)
        {
            for (int counter = 0; counter < Random.Range(6, 13); counter++)
            {
                whichAsteroid = Random.Range(0, 6);

                Instantiate(asteroidEnemy[whichAsteroid], new Vector3(Random.Range(playerControllerReference.playerBoundary.xMin, playerControllerReference.playerBoundary.xMax), 0f, Random.Range(6f, 9f)), Quaternion.identity);

                yield return new WaitForSeconds(.9f);
            }

            yield return new WaitForSeconds(6f);
        }
    }
}