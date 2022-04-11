using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float destroyTime = 2f;
    
    void Start()
    {
        Destroy(this.gameObject, destroyTime); // Destroys object after allocated amount of time
    }
}
