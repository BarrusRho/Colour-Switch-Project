using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 100f;
 
    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);  // Rotates object on the Z-axis
    }
}
