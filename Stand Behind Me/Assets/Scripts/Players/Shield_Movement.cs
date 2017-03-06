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

	string shieldDir = "right"; // right || left || up

	public List<GameObject> shields;

	// Use this for initialization
	void Start()
	{
		foreach (Transform child in transform)
		{
			if (child.tag == "Shield")
			{
				shields.Add(child.gameObject);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		this.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		float moveHorizontal = Input.GetAxis ("L Horizontal J1");
		Vector3 new_offset = new Vector3 (moveHorizontal, 0, 0);

		float lookHorizontal = Input.GetAxis ("R Horizontal J1");
		Vector3 facingH = new Vector3 (lookHorizontal, 0, 0);

		//Debug.Log (facingH);

		float lookVertical = Input.GetAxis ("R Vertical J1");
		Vector3 facingV = new Vector3 (0, lookVertical, 0);

		//============= Input Detection ==================
		// Detect lateral movement

		new_offset += new_offset * speed;

		//if (new_offset.x < 0) {
			
			//			this.GetComponent<Animator> ().SetBool ("running", true);
		//} else if (new_offset.x > 0) {
			//new_offset += new_offset * speed;
			//			this.GetComponent<Animator> ().SetBool ("running", true);
		//}

		// Shield Input
		if (facingH.x < 0 && facingH.x < -.5) {
			shieldDir = "left";
		} else if (facingH.x > 0 && facingH.x > .5) {
			shieldDir = "right";
		} else if (facingV.y < 0 && Mathf.Abs(facingH.x) < .5) {
			shieldDir = "up";
		}

		// If Jump input detected, set the state to the rising jump phase
		if (Input.GetButtonDown("A/X J1") && !jumping) 
		{
			jumping = true;
			// notify animator that we are jumping
			if (shieldDir.Equals("up")) {
				this.GetComponent<Animator> ().SetInteger ("jump_state", -1);
			} else {
				this.GetComponent<Animator> ().SetInteger ("jump_state", 1);
			}
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

		//===================== ANIMATOR MOVE STATE UPDATE =================
		if (new_offset.x < 0) {
			//			this.GetComponent<Animator> ().SetBool ("horizontal_flip", true);
			if (shieldDir.Equals ("left")) {
				this.GetComponent<Animator> ().SetInteger ("move_state", 1);
				transform.localScale = new Vector3(-1.3f, 1.3f, 1f);
			} else if (shieldDir.Equals ("right")) {
				this.GetComponent<Animator> ().SetInteger ("move_state", 5);
				transform.localScale = new Vector3(1.3f, 1.3f, 1f);
			} else if (shieldDir.Equals("up")){
				this.GetComponent<Animator> ().SetInteger ("move_state", 3);
				transform.localScale = new Vector3(-1.3f, 1.3f, 1f);
			}
		} else if (new_offset.x > 0) {
			//			this.GetComponent<Animator> ().SetBool ("horizontal_flip", false);
			//			transform.localScale = new Vector3(1f, 1f, 1f);
			if (shieldDir.Equals ("right")) {
				this.GetComponent<Animator> ().SetInteger ("move_state", 6);
				transform.localScale = new Vector3(1.3f, 1.3f, 1f);
			} else if (shieldDir.Equals ("left")) {
				this.GetComponent<Animator> ().SetInteger ("move_state", 2);
				transform.localScale = new Vector3(-1.3f, 1.3f, 1f);
			} else if (shieldDir.Equals("up")) {
				this.GetComponent<Animator> ().SetInteger ("move_state", 4);
				transform.localScale = new Vector3(1.3f, 1.3f, 1f);
			}
		} else if (new_offset.x == 0) {
			if (shieldDir.Equals ("right")) {
				this.GetComponent<Animator> ().SetInteger ("move_state", 0);
				transform.localScale = new Vector3(1.3f, 1.3f, 1f);
			} else if (shieldDir.Equals ("left")) {
				this.GetComponent<Animator> ().SetInteger ("move_state", -1);
				transform.localScale = new Vector3(-1.3f, 1.3f, 1f);
			} else if (shieldDir.Equals("up")) {
				this.GetComponent<Animator> ().SetInteger ("move_state", -2);
//				transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}

		//===================== SHIELD COLLIDERS UPDATE =================
		if (shieldDir.Equals ("right")) {
			shields [0].SetActive(true);
			shields [1].SetActive(false);
		} else if (shieldDir.Equals ("left")) {
			shields [0].SetActive(true);
			shields [1].SetActive(false);
		} else if (shieldDir.Equals("up")) {
			shields [0].SetActive(false);
			shields [1].SetActive(true);
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