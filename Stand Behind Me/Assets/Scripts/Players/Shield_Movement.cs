using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Movement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    public float moveForce = 6f;
    bool jumpReady = true;
    

    private Rigidbody2D _myRigidbody;


    // Use this for initialization
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate(){

        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal * _myRigidbody.velocity.x < speed)
            _myRigidbody.AddForce(Vector2.right * moveHorizontal * moveForce);

        if (Mathf.Abs(_myRigidbody.velocity.x) > speed)
            _myRigidbody.velocity = new Vector2(Mathf.Sign(_myRigidbody.velocity.x) * speed, _myRigidbody.velocity.y);

        if (Input.GetButtonDown("Fire2") && jumpReady)
        {
            jumpReady = false;
            _myRigidbody.AddForce(new Vector2(0f, jumpForce));
        }

    }
}