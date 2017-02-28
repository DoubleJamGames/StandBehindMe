using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Movement : MonoBehaviour
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
		if (Input.GetKey (KeyCode.A)) 
		{
			new_offset += Vector3.left * speed;
			this.GetComponent<Animator> ().SetBool ("running", true);
		} else if (Input.GetKey (KeyCode.D)) {
			new_offset += Vector3.right * speed;
			this.GetComponent<Animator> ().SetBool ("running", true);
		}

		// If Jump input detected, set the state to the rising jump phase
		if (Input.GetKey(KeyCode.W) && !jumping) 
		{
			jumping = true;
			// notify animator that we are jumping
			this.GetComponent<Animator> ().SetInteger ("jump_state", 1);
		}
		//============================================


		if (jumping) {
			float vertVelocity = (jumpPower - (jumpDecel * currJumpTime));
			if (vertVelocity < 0) {
				// notify animator that we are falling
				this.GetComponent<Animator> ().SetInteger ("jump_state", 2);
			}
			new_offset += Vector3.up * vertVelocity;
			currJumpTime += Time.deltaTime;
		}

		//===================== TRANSFORM POSITION UPDATE =================
		Vector3 target_point = transform.position += new_offset * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target_point, 5);

		if (new_offset.x < 0) {
//			this.GetComponent<Animator> ().SetBool ("horizontal_flip", true);
			transform.localScale = new Vector3(-1f, 1f, 1f);
		} else if (new_offset.x > 0) {
//			this.GetComponent<Animator> ().SetBool ("horizontal_flip", false);
			transform.localScale = new Vector3(1f, 1f, 1f);
		} else if (new_offset.x == 0) {
			this.GetComponent<Animator> ().SetBool ("running", false);
		}

	}

	void OnCollisionEnter2D(Collision2D c)
	{
		// Detect Landing
		if (c.gameObject.tag == "Platform")
		{
			jumping = false;
			//notify animator that we are no longer jumping
			this.GetComponent<Animator> ().SetInteger ("jump_state", 0);
			currJumpTime = 0f;
		}
	}
}