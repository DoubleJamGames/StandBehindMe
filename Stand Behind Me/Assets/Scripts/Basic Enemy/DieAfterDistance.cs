using UnityEngine;
using System.Collections;

public class DieAfterDistance : MonoBehaviour
{

    public float maxDistance;

    private Vector3 spawnPos;

    void Start()
    {
        spawnPos = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(spawnPos, transform.position) >= maxDistance) Destroy(gameObject);
    }
}
