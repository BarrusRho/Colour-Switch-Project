using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance = null;
    public static SpawnManager instance
    {
        get
        {
            return _instance;
        }
    }

    private Transform _playerTransform;
    private float _spawnDistance = 8f;
    public GameObject colourSwitch;
    public GameObject[] obstacle;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // Sets instance of SpawnManager Singleton
        }
        else if (_instance  != this)
        {
            Destroy(this.gameObject, 0f);
        }

        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void ColourSwitchSpawn() // Spawns a new Colour Switch a set distance above the Player
    {
        Instantiate(colourSwitch, new Vector2(_playerTransform.transform.position.x, _playerTransform.transform.position.y + _spawnDistance), transform.rotation);
    }

    public void ObstacleSpawn() // Spawns an obstacle from the array at a set distance from the Player
    {
        int randomNumber = Random.Range(0, obstacle.Length);
        Instantiate(obstacle[randomNumber], new Vector2(_playerTransform.transform.position.x, _playerTransform.transform.position.y + _spawnDistance), transform.rotation);
    }
}
