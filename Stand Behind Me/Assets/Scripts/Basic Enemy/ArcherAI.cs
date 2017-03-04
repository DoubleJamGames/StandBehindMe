using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    public GameObject target;
    public GameObject self;
    public GameObject fireball;
    GameObject clone;

    float speed = 3f;
    float distance;

    float Health = 5f;
    float timer = 0f;
    float delayMove = 0f;
    string facing;
    bool stopped = false;
    bool playerFound = false;

    void FixedUpdate()
    {
        if (stopped && (timer >= 5f))
        {
            ///Debug.Log(facing);
            if (facing == "left")
            {
                clone = (Instantiate(fireball, new Vector3((transform.position.x - 1), transform.position.y), transform.rotation)) as GameObject; // Quaternion.Euler(new Vector2(0,0)))) as GameObject;
                clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 200f);
                timer = 0f;
            }
            else if (facing == "right")
            {
                clone = (Instantiate(fireball, new Vector3((transform.position.x + 1), transform.position.y), transform.rotation)) as GameObject;
                clone.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200f);
                timer = 0f;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void Update()
    {
        distance = self.transform.position.x - target.transform.position.x;

        if (playerFound)
        {
            if (delayMove >= 5f)
            {
                Move(distance);
                delayMove = 0f;
            }
            else
            {
                delayMove += Time.deltaTime;
            }
        }
        else
        {
            Move(distance);
        }
    }

    void Move(float dist)
    {
        Vector3 offset = Vector3.zero;
        //Debug.Log(dist);
        if (dist >= 0)
        {
            if (dist <= 2)
            {
                stopped = true;
                playerFound = true;
            }
            else if (dist < 10)
            {
                playerFound = false;
                stopped = false;
                facing = "left";
                offset = Vector3.left * speed;
            }

            Vector3 mT = transform.position += offset * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mT, 5);

        }
        else
        {
            if (dist >= -2)
            {
                playerFound = true;
                stopped = true;
            }
            else if (dist > -10)
            {
                playerFound = false;

                stopped = false;
                facing = "right";
                offset = Vector3.right * speed;
            }

            Vector3 mT = transform.position += offset * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mT, 5);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(Health);
            if (Health == 0f)
                Destroy(gameObject);
            else
            {
                Health--;
            }
        }
    }

}