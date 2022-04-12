using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _playerTransform;
    
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (_playerTransform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z); // Camera follows the Player location upwards and does not follow the Player downwards
        }
    }
}
