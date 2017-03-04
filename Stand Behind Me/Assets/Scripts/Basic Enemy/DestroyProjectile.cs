using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour
{
    
    void OnTriggerEnter2D()
    {
        Destroy(gameObject);

    }
}
