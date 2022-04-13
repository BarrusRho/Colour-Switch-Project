using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager instance 
    {
        get
        {
            return _instance;
        }
    }

    private int _score = 0;
    private int _highScore;
    public bool gameOver = false;
    public bool canClick = false;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText, gameOverScoreText, highScoreText, restartText;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // Sets instance of GameManager Singleton
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject, 0f);
        }
    }
    
    void Start()
    {
        gameOver = false;
        canClick = false;
        scoreText.text = $"{_score}"; // Shows the score on the UI Text
        _highScore = PlayerPrefs.GetInt("HighScore"); // Gets the highest score recorded
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameOver == true && canClick == true) // If the game is over and the "Click to Restart" UI Text is visible, then Load the current scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }                     
    }

    public void UpdateScore(int scoreToAdd) // Adds score to UI Text and updates the highest score if a new high score is achieved
    {
        _score += scoreToAdd;
        scoreText.text = $"{_score}";        

        if (_score > _highScore)
        {
            _highScore = _score;
            highScoreText.text = $"High Score: {_highScore}";
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
    }

    public void GameOver() // Sets game over state to be true and starts running the end state UI
    {
        gameOver = true;
        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine() // Starts running the end state UI and sets bool to allow clicking to restart the game
    {
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = $"{_score}";
        highScoreText.text = $"{_highScore}";
        yield return new WaitForSeconds(0.75f);
        restartText.gameObject.SetActive(true);
        canClick = true;
    }
}
