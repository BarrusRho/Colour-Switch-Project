using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera _mainCamera;
    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _playerSpriteRenderer;
    private string _playerColour;
    private string _previousPlayerColour;
    private float _upwardsForce = 7.5f;
    private float _gravityScale = 2.5f;
    public Color magentaColour, blueColour, greenColour, redColour;
    public GameObject deathEffect, starCollectedEffect;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _playerRigidbody.gravityScale = 0.0f; // Sets gravity to 0 so the ball does not fall on Start
        GiveRandomColour(); // Gives a new colour to the Player at the Start of the game
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
            _playerRigidbody.gravityScale = _gravityScale; // Sets gravity so the ball can fall
            _playerRigidbody.velocity = Vector2.up * _upwardsForce; // Jumping control with Left Mouse Click
            AudioManager.instance.PlayJumpAudio(); // Plays a jumping sound with every Left Mouse Click
        }
    }

    public void PlayerFall() // Destroys the Player if Player falls out of view of Camera  
    {
        var cameraOffset = 5.5f;
        if (this.transform.position.y + cameraOffset < _mainCamera.transform.position.y)
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        Instantiate(deathEffect, transform.position, transform.rotation); // Instantiates a player explosion particle system at the Player's transform position
        AudioManager.instance.PlayDefeatAudio(); // Plays a player explosion sound on Player death
        GameManager.instance.GameOver(); // Begins the game over state
        this.gameObject.SetActive(false); // Disables the Player 
    }

    public void GiveRandomColour() // Gives a "random" of 4 colours to the Player object
    {
        int index = Random.Range(0, 4);
        switch (index) // Switch statements are cleaner and easier to read than many if else statements
        {
            case 0: _playerColour = "Magenta";
                _playerSpriteRenderer.color = magentaColour;
                break;

            case 1: _playerColour = "Blue";
                _playerSpriteRenderer.color = blueColour;
                break;

            case 2: _playerColour = "Green";
                _playerSpriteRenderer.color = greenColour;
                break;

            case 3: _playerColour = "Red";
                _playerSpriteRenderer.color = redColour;
                break;
        }

        if (_playerColour == _previousPlayerColour) // Checks if the new Player colour is the same as the previous colour and if it is, a new colour is randomised 
        {
            Debug.Log($"Colours are the same. Assigning new colour");
            GiveRandomColour();
        }

        _previousPlayerColour = _playerColour;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ColourSwitch"))
        {
            GiveRandomColour(); // Assigns new colour to Player when object is touched by Player
            SpawnManager.instance.ColourSwitchSpawn(); // Instantiates a new Colour Switch
            AudioManager.instance.PlayColourChangeAudio();
            Destroy(other.gameObject, 0f); // Destroys current Colour Switch
            return;
        }

        if (other.gameObject.CompareTag("StarPickup"))
        {
            CollectStar();
            SpawnManager.instance.ObstacleSpawn(); // Instantiates a new obstacle from an array
            Destroy(other.gameObject, 0f); // Destroys current Star Pickup
            return;
        }

        if (other.tag == _playerColour) // Checks if Player colour matches obstacle colour
        {
            Debug.Log($"{other.tag} Colours match. You shall pass");
        }
        else
        {
            Debug.Log($"{other.tag} Colours do not match. You shall not pass");
            PlayerDeath();
        }
    }

    public void CollectStar()
    {
        var starScore = 1;
        Instantiate(starCollectedEffect, transform.position, transform.rotation);
        AudioManager.instance.PlayStarCollectAudio();
        GameManager.instance.UpdateScore(starScore); // Adds score to the Text UI
    }
}
