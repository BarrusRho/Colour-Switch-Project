using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float upwardsForce = 7.5f;

    public Camera mainCamera;
    
    public Rigidbody2D theRB;

    public SpriteRenderer theSR;

    public GameObject deathEffect, starCollectedEffect;

    private int starScore = 1;

    public float gravityScale = 2.5f;

    private void Awake()
    {
        instance = this; // Sets instance of PlayerController Singleton
    }

    void Start()
    {
        GameManager.instance.GiveRandomColour(); // Gives a new colour to the Player at the Start of the game

        theRB.gravityScale = 0.0f; // Sets gravity to 0 so the ball does not fall on Start
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        PlayerFall();
    }

    public void MovePlayer() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            theRB.gravityScale = gravityScale; // Sets gravity so the ball can fall

            theRB.velocity = Vector2.up * upwardsForce; // Jumping control with Left Mouse Click

            AudioManager.instance.PlayJumpAudio(); // Plays a jumping sound with every Left Mouse Click
        }
    }

    public void PlayerDeath() 
    {
        Instantiate(deathEffect, transform.position, transform.rotation); // Instantiates a player explosion particle system at the Player's transform position

        AudioManager.instance.PlayDefeatAudio(); // Plays a player explosion sound on Player death

        GameManager.instance.GameOver(); // Begins the game over state
        
        this.gameObject.SetActive(false); // Disables the Player 
    }

    public void CollectStar() 
    {
        Instantiate(starCollectedEffect, transform.position, transform.rotation);

        AudioManager.instance.PlayStarCollectAudio();

        GameManager.instance.AddScore(starScore); // Adds score to the Text UI
    }

    public void PlayerFall() // Destroys the Player if Player falls out of view of Camera  
    {
        if (this.transform.position.y + 5.5 < mainCamera.transform.position.y)        
        {
            PlayerDeath();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ColourSwitch") 
        {
            GameManager.instance.GiveRandomColour(); // Assigns new colour to Player when object is touched by Player

            SpawnManager.instance.ColourSwitchSpawn(); // Instantiates a new Colour Switch

            Destroy(other.gameObject, 0f); // Destroys current Colour Switch

            return;
        }

        if(other.tag == "StarPickup") 
        {
            CollectStar();

            SpawnManager.instance.ObstacleSpawn(); // Instantiates a new obstacle from an array

            Destroy(other.gameObject, 0f); // Destroys current Star Pickup

            return;
        }

        if(other.tag == GameManager.instance.playerColour) // Checks if Player colour matches obstacle colour
        {
            Debug.Log(other.tag + " Colours match. You shall pass");
        }
        else 
        {
            Debug.Log(other.tag + " Colours do not match. You shall not pass");

            PlayerDeath();                     
        }        
    }

}
