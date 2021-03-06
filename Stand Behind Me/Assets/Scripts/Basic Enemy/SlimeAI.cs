﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour {

    public GameObject Target;
    public GameObject self;

    float Health = 5f;
    float delayMove = 0f;
    float speed = 1f;
    float jumpPower = 2.25f;
    float distance;
    
	// Update is called once per frame
	void FixedUpdate () {
        //Delay the movement of the slime
        if(delayMove >= 3f)
        {
            Vector3 new_offset = Vector3.zero;
            distance = self.transform.position.x - Target.transform.position.x;
            
			if (Mathf.Abs (distance) < 15f) {
				//The target is to the left of the slime
				if (distance >= 0) {
					new_offset = (Vector3.left * speed) + (Vector3.up * jumpPower);
				}
            //The target is to the right of the slime
            else {
					new_offset = (Vector3.right * speed) + (Vector3.up * jumpPower);
				}
			}
            self.GetComponent<Rigidbody2D>().AddForce(new_offset * jumpPower, ForceMode2D.Impulse);

            delayMove = 0f;
        }
        else
        {
            delayMove += Time.deltaTime;       
        }
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.collider.gameObject.tag.Equals ("Spear")) {
			print ("You hit an enemy!");
			Health--;
			if (Health == 0f) {
				print ("You killed an enemy!!!!");
				Destroy (gameObject);
			}
		} else if (collision.collider.gameObject.tag.Equals ("Player")) {
			print ("you got hit!");
		}
    }

	void OnTriggerEnter2D(Collider2D c) {
		print (c.gameObject.tag);
	}
}
