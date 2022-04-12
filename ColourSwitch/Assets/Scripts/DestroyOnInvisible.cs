using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    private Camera _mainCamera;
    private float _cameraOffset = 7.0f;

    private void Awake()
    {
        _mainCamera = Camera.main;        
    }

    private void Update()
    {
        if (this.transform.position.y + _cameraOffset < _mainCamera.transform.position.y) // Destroys the object if object falls out of view of Camera
        {
            Destroy(this.gameObject, 0f); 
        }
    }
}
