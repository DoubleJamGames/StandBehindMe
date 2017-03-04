using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour
{
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        Destroy(gameObject);

    }
}
