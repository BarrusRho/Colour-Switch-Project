using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string playerColour;

    public string previousPlayerColour;
    
    public Color magentaColour, blueColour, greenColour, redColour;

    public int score = 0;

    private int highScore;

    public bool gameOver = false;

    public bool canClick = false;

    private void Awake()
    {
        instance = this; // Sets instance of GameManager Singleton
    }
    
    void Start()
    {
        gameOver = false;

        canClick = false;

        UIManager.instance.scoreText.text = "" + score; // Shows the score on the UI Text

        highScore = PlayerPrefs.GetInt("HighScore"); // Gets the highest score recorded
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameOver == true && canClick == true) // If the game is over and the "Click to Restart" UI Text is visible, then Load the current scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }                     
    }

    public void GiveRandomColour() // Gives a "random" of 4 colours to the Player object
    {
        int index = Random.Range(0, 4);

        switch (index) // Switch statements are cleaner and easier to read than many if else statements
        {
            case 0: playerColour = "Magenta";

                PlayerController.instance.theSR.color = magentaColour;

                break;

            case 1: playerColour = "Blue";

                PlayerController.instance.theSR.color = blueColour;

                break;

            case 2: playerColour = "Green";

                PlayerController.instance.theSR.color = greenColour;

                break;

            case 3: playerColour = "Red";

                PlayerController.instance.theSR.color = redColour;

                break;
        }

        if (playerColour == previousPlayerColour) // Checks if the new Player colour is the same as the previous colour and if it is, a new colour is randomised 
        {
            Debug.Log("Colours are the same. Assigning new colour");

            GiveRandomColour();
        }

        previousPlayerColour = playerColour;
    }

    public void AddScore(int scoreToAdd) // Adds score to UI Text and updates the highest score if a new high score is achieved
    {
        score += scoreToAdd;        

        UIManager.instance.scoreText.text = "" + score;        

        if (score > highScore)
        {
            highScore = score;

            UIManager.instance.highScoreText.text = "High Score: " + highScore;

            PlayerPrefs.SetInt("HighScore", highScore);
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

        UIManager.instance.gameOverPanel.SetActive(true);

        UIManager.instance.gameOverScoreText.text = "" + score;        

        UIManager.instance.highScoreText.text = "" + highScore;

        yield return new WaitForSeconds(0.75f);

        UIManager.instance.restartText.gameObject.SetActive(true);

        canClick = true;
    }
}
