using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    private float spawnRate = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
        UpdateScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (true)
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
}