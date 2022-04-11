using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;        
    }

    void Update()
    {
        if (this.transform.position.y + 7 < mainCamera.transform.position.y) // Destroys the object if object falls out of view of Camera
        {
            Destroy(this.gameObject, 0f); 
        }
    }
}
