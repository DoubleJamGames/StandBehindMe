using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{

    public GameObject Players;
    public GameObject Enemy;
    public GameObject fireballPrefab;
    public float timer = 0f;
    public float delaymove = 0f;
    GameObject clone;
    string facing;
    bool stopped = false;
    

    public float speed = 3f;
    public float distance;

    [HideInInspector]
    public string currentState;

    void FixedUpdate()
    {
        if (stopped && (timer >= 5f))
        {
            Debug.Log(facing);
            if (facing == "left")
            {
                clone = (Instantiate(fireballPrefab, new Vector3((transform.position.x - 1), transform.position.y), transform.rotation)) as GameObject; // Quaternion.Euler(new Vector2(0,0)))) as GameObject;
                clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 200f);
                timer = 0f;
            }
            else if (facing == "right")
            {
                clone = (Instantiate(fireballPrefab, new Vector3((transform.position.x + 1), transform.position.y), transform.rotation)) as GameObject;
                clone.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200f);
                timer = 0f;
            }

        }
        else
        {
            timer++;
        }

    }
    // Update is called once per frame
    void Update()
    {

        Vector3 offset = Vector3.zero;
        distance = Enemy.transform.position.x - Players.transform.position.x;

        //Debug.Log(distance);
        if (distance >= 0)
        {
            facing = "left";
            if (distance <= 2)
            {

                stopped = true;
            }
            else if (distance < 10)
            {
                if (delaymove >= 5f)
                {
                    stopped = false;
                    facing = "left";
                    offset = Vector3.left * speed;
                    delaymove = 0f;
                    //Debug.Log(stopped);
                }
                else
                {
                    delaymove += Time.deltaTime;
                }

            }

            //------Move the enemy-----
            Vector3 target = transform.position += offset * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, 5);
        }
        else
        {
            facing = "right";
            if ((distance < -2) && (distance > -10))
            {
                if (delaymove >= 5f)
                {
                    offset = Vector3.right * speed;
                    stopped = false;
                    delaymove = 0f;
                }
                else
                {
                    delaymove += Time.deltaTime;
                }

            }
            else
            {
                stopped = true;
            }

            //------Move the enemy-----
            Vector3 target = transform.position += offset * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, 5);

        }
    }
}
