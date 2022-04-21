using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float _spawnDistance = 8f;
    public Transform _playerTransform;
    public GameObject colourSwitch;
    public GameObject[] obstacle;

    private void OnEnable()
    {
        PlayerController.onColourSwitchSpawn += ColourSwitchSpawn;
        PlayerController.onObstacleSpawn += ObstacleSpawn;
    }

    private void OnDisable()
    {
        PlayerController.onColourSwitchSpawn -= ColourSwitchSpawn;
        PlayerController.onObstacleSpawn -= ObstacleSpawn;
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
