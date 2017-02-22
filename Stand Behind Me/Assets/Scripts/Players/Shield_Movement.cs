using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Movement : MonoBehaviour
{
    public float speed;
    public float jump;
    bool jumpReady = true;

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Platform")
        {
            jumpReady = true;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.W)) && jumpReady)
        {
            transform.position += Vector3.up * Time.deltaTime * jump;
            jumpReady = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }
}