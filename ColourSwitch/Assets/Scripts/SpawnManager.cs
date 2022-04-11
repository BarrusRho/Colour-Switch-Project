using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public Transform playerTransform;

    public GameObject colourSwitch;

    public float spawnDistance = 8f;

    public GameObject[] obstacle;

    private void Awake()
    {
        instance = this; // Sets instance of SpawnManager Singleton
    }

    public void ColourSwitchSpawn() // Spawns a new Colour Switch a set distance above the Player
    {
        Instantiate(colourSwitch, new Vector2(playerTransform.transform.position.x, playerTransform.transform.position.y + spawnDistance), transform.rotation);
    }

    public void ObstacleSpawn() // Spawns an obstacle from the array at a set distance from the Player
    {
        int randomNumber = Random.Range(0, 11);

        Instantiate(obstacle[randomNumber], new Vector2(playerTransform.transform.position.x, playerTransform.transform.position.y + spawnDistance), transform.rotation);
    }
}
