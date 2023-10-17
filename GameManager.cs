using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;

    private int score;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI gameOverText;

    public bool isGameActive;

    public Button restartButton;

    public GameObject titleScreen;

    public TextMeshProUGUI livesText;
    private int lives;

    public GameObject pauseScreen;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        isGameActive = true;    
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0; 
        spawnRate = spawnRate /= difficulty;
        
        UpdateScore(0);
        UpdateLives(3);
        StartCoroutine(SpawnTarget());
       
        titleScreen.gameObject.SetActive(false);
    }
    void ChangePaused()
    {
        if(!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTarget()
    {
       
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
          
        }
    }

      public  void UpdateScore( int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }

    public void UpdateLives(int livesToChange) 
    {  lives += livesToChange; 
        livesText.text = "Lives: " + lives; 
        if (lives <= 0) 
        { 
            GameOver(); 
        } 
    }

}
