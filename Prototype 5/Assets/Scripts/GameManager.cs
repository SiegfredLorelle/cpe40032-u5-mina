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
    public bool inMenu;
    public int lives;
    public int score;
    public float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set the game as not active because still in menu
        isGameAcitve = false;
        inMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Pauses the game when esc is presesd
        if (!inMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    // Routine for spawning targets
    IEnumerator SpawnTarget()
    {
        // Spawn a random target at an interval of spawn rate (varies on difficulty)
        while (isGameAcitve)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Update the score, both the variable and the text in the player's screen
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    // Update the lives, both the variable and the text in the player's screen
    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = $"Lives: {lives}";

        // Game over if no more lives
        if (lives == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        // Show the game over screen
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        // Set game state accordingly
        isGameAcitve = false;
        inMenu = true;

        backgroundMusic.Stop();
    }

    public void RestartGame()
    {
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called when clicking the diffculty buttons
    public void StartGame(int difficulty)
    {
        // Set game state accordingly
        isGameAcitve = true;
        inMenu = false;

        // Set initial score and lives
        score = 0;
        lives = 3;
        UpdateScore(0);
        UpdateLives(0);

        // Set the spawn rate based on the difficulty selected
        spawnRate /= difficulty;

        // Start spawning random targets
        StartCoroutine(SpawnTarget());

        // Remove the title screen
        titleScreen.SetActive(false);
    }

    // Pause mechanic works by utilizing Time.timeScale (1 means normal speed, 0.5 means 2x slower, 0 means stop)
    // more details on Time.timeScale -> https://docs.unity3d.com/ScriptReference/Time-timeScale.html
    public void PauseGame()
    {
        // Pause if currently unpaused
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            isGameAcitve = false;
            backgroundMusic.Pause();
        }

        // Unpause if currently paused
        else
        {
            Time.timeScale = 1
                ;
            pauseUI.SetActive(false);
            isGameAcitve = true;
            backgroundMusic.UnPause();
        }
    }
}