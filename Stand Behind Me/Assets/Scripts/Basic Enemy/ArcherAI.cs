using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    public GameObject target;
    public GameObject fireball;
    GameObject clone;

    float speed = 3f;
    float distance;

    public float Health = 5f;
    float timer = 0f;
    float delayMove = 0f;
    string facing;
    bool stopped = false;
    bool playerFound = false;

    void FixedUpdate()
    {
        if (stopped && (timer >= 2f))
        {
            Transform spawn = this.gameObject.transform.GetChild(0);

			Vector3 towardsTarget = Vector3.Normalize(target.transform.position - spawn.position);

            ///Debug.Log(facing);
            if (facing == "left")
            {
                clone = (Instantiate(fireball, new Vector3((spawn.position.x), spawn.position.y), spawn.rotation)) as GameObject; // Quaternion.Euler(new Vector2(0,0)))) as GameObject;
				clone.GetComponent<Rigidbody2D>().AddForce(towardsTarget * 150f);
                timer = 0f;
            }
            else if (facing == "right")
            {
                clone = (Instantiate(fireball, new Vector3((spawn.position.x), spawn.position.y), spawn.rotation)) as GameObject;
				clone.GetComponent<Rigidbody2D>().AddForce(towardsTarget * 150f);
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
        distance = transform.position.x - target.transform.position.x;

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
                playerFound = true;
                stopped = true;
                facing = "left";
                offset = Vector3.left * speed;
            }
			transform.localScale = new Vector3 (-1f, 1f, 1f);
//            Vector3 mT = transform.position += offset * Time.deltaTime;
//            transform.position = Vector3.MoveTowards(transform.position, mT, 5);

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
                playerFound = true;

                stopped = true;
                facing = "right";
                offset = Vector3.right * speed;
            }
			transform.localScale = new Vector3 (1f, 1f, 1f);
//            Vector3 mT = transform.position += offset * Time.deltaTime;
//            transform.position = Vector3.MoveTowards(transform.position, mT, 5);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.collider.gameObject.tag.Equals("Spear"))
        {
			print ("You hit an enemy!");
			Health--;
			if (Health == 0f) {
				print ("You killed an enemy!!!!");
				Destroy (gameObject);
			}
        }
    }
}