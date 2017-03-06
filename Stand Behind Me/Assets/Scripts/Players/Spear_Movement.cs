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
	bool attacking = false;

	public List<GameObject> spearBoxes;

	// Use this for initialization
	void Start()
	{
		foreach (Transform child in transform)
		{
			if (child.tag == "Spear")
			{
				spearBoxes.Add(child.gameObject);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 new_offset = Vector3.zero;
		//============= Input Detection ==================
		// Detect lateral movement
		if (!(attacking && !jumping)) {
			if (Input.GetKey (KeyCode.F)) {
				new_offset += Vector3.left * speed;
				this.GetComponent<Animator> ().SetBool ("running", true);
			} else if (Input.GetKey (KeyCode.H)) {
				new_offset += Vector3.right * speed;
				this.GetComponent<Animator> ().SetBool ("running", true);
			}
		}


		// Spear Input
		if (!attacking) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				this.GetComponent<Animator> ().SetInteger ("attack_state", 2);
				transform.localScale = new Vector3(-1f, 1f, 1f);
				spearBoxes [0].SetActive (true);
				spearBoxes [1].SetActive (false);
				attacking = true;
			} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				this.GetComponent<Animator> ().SetInteger ("attack_state", 3);
				transform.localScale = new Vector3(1f, 1f, 1f);
				spearBoxes [0].SetActive (true);
				spearBoxes [1].SetActive (false);
				attacking = true;
			} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
				this.GetComponent<Animator> ().SetInteger ("attack_state", 1);
				spearBoxes [0].SetActive (false);
				spearBoxes [1].SetActive (true);
				attacking = true;
			}
		}

		// If Jump input detected, set the state to the rising jump phase
		if (Input.GetKeyDown(KeyCode.T) && !jumping && !attacking) 
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
			if (!attacking) {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
		} else if (new_offset.x > 0) {
			if (!attacking) {
				transform.localScale = new Vector3 (1f, 1f, 1f);
			}
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

	public void attackReady(){
		this.GetComponent<Animator> ().SetInteger ("attack_state", 0);
		spearBoxes [0].SetActive (false);
		spearBoxes [1].SetActive (false);
		attacking = false;
	}
}