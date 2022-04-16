using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    private int _highScore;
    public bool gameOver = false;
    public bool canClick = false;
    public GameObject startGamePanel, gameOverPanel;
    public TextMeshProUGUI scoreText, gameOverScoreText, highScoreText, restartText;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && startGamePanel.activeInHierarchy == true)
        {
            startGamePanel.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && gameOver == true && canClick == true) // If the game is over and the "Click to Restart" UI Text is visible, then Load the current scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void StartGame()
    {
        gameOver = false;
        canClick = false;
        startGamePanel.SetActive(true);
        scoreText.text = $"{_score}"; // Shows the score on the UI Text
        _highScore = PlayerPrefs.GetInt("HighScore"); // Gets the highest score recorded
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
