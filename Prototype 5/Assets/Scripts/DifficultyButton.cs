using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button button;

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Called on clicking difficulty buttons (easy, medium, hard btns)
    void SetDifficulty()
    {
        // Start the game and pass the difficulty of the clicked button
        gameManager.StartGame(difficulty);
    }
}
