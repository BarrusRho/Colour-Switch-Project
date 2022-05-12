using System.Collections;
using System.IO;
using UnityEngine;
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

    private void OnEnable()
    {
        PlayerController.OnStarCollected += UpdateScore;
        PlayerController.OnPlayerDeath += GameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnStarCollected -= UpdateScore;
        PlayerController.OnPlayerDeath -= GameOver;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
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
        LoadHighScore(); // Gets the highest score recorded from SavaData

    }

    public void UpdateScore(int scoreToAdd) // Adds score to UI Text and updates the highest score if a new high score is achieved
    {
        _score += scoreToAdd;
        scoreText.text = $"{_score}";

        if (_score > _highScore)
        {
            _highScore = _score;
            highScoreText.text = $"High Score: {_highScore}";
            SaveHighScore(); //Sets the highscore SaveData
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
    
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = _highScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            _highScore = data.highScore;
        }
    }
}

