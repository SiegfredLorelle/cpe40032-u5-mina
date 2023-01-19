using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public AudioSource backgroundMusic;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public GameObject pauseUI;
    public bool isGameAcitve;
    public bool inStartMenu;
    public int lives;
    public int score;
    public float spawnRate = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
        isGameAcitve = false;
        inStartMenu = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!inStartMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }


    }

    IEnumerator SpawnTarget()
    {
        while (isGameAcitve)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = $"Lives: {lives}";

        if (lives == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameAcitve = false;
        inStartMenu = true;

        backgroundMusic.Stop();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameAcitve = true;
        inStartMenu = false;
        score = 0;
        lives = 3;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        titleScreen.SetActive(false);
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            isGameAcitve = false;
            backgroundMusic.Pause();

        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            isGameAcitve = true;
            backgroundMusic.UnPause();

        }
    }

}