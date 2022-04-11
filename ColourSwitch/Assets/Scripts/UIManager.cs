using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject gameOverPanel;

    public Text scoreText, gameOverScoreText, highScoreText, restartText;

    private void Awake()
    {
        instance = this; // Sets instance of UIManager Singleton
    }
}
