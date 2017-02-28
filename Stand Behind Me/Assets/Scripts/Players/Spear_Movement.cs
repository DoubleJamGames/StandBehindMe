using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear_Movement : MonoBehaviour
{
    public float speed = 3f;
	public float maxMovementDist = 1f;
	public float jumpPower = 12f;
	public float jumpDecel = 20f;
    
	bool jumping = false;
	float currJumpTime = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		Vector3 new_offset = Vector3.zero;
		//============= Input Detection ==================
		// Detect lateral movement
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			new_offset += Vector3.left * speed;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			new_offset += Vector3.right * speed;
		}

		// If Jump input detected, set the state to the rising jump phase
		if (Input.GetKey(KeyCode.UpArrow) && !jumping) 
		{
			jumping = true;
        }
		//============================================


		if (jumping) {
			new_offset += Vector3.up * (jumpPower - (jumpDecel * currJumpTime));
			currJumpTime += Time.deltaTime;
		}

		//===================== TRANSFORM POSITION UPDATE =================
		Vector3 target_point = transform.position += new_offset * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target_point, 5);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
		// Detect Landing
        if (c.gameObject.tag == "Platform")
        {
            jumping = false;
			currJumpTime = 0f;
        }
    }
}