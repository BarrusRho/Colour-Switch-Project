using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;
    public static UIManager instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject gameOverPanel;
    public Text scoreText, gameOverScoreText, highScoreText, restartText;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // Sets instance of UIManager Singleton
            //DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject, 0f);
        }
    }
}
