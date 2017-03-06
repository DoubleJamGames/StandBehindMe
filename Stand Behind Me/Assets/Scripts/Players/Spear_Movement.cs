using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear_Movement : MonoBehaviour
{
	public float speed = 3f;
	public float maxMovementDist = 1f;
	public float jumpPower = 20f;
	public float jumpDecel = 20f;
	public AudioClip jumpSound;
	public float jumpSoundVol = .5f;
	public AudioClip attackSound;
	public float attackSoundVol = .5f;

	bool jumping = false;
	float currJumpTime = 0f;
	bool attacking = false;
	string spearDir = "right";

	public List<GameObject> spearBoxes;
	private AudioSource source;

	void Awake () {
		source = GetComponent<AudioSource>();
	}

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
		Vector3 oldVelocity = this.GetComponent<Rigidbody2D> ().velocity;
		oldVelocity.x = 0f;
		this.GetComponent<Rigidbody2D> ().velocity = oldVelocity;

		float moveHorizontal = Input.GetAxis ("L Horizontal J2");
		Vector3 new_offset = new Vector3 (moveHorizontal, 0, 0);

		float lookHorizontal = Input.GetAxis ("R Horizontal J2");
		Vector3 facingH = new Vector3 (lookHorizontal, 0, 0);

		//Debug.Log(Input.GetButtonDown ("A/X J2"));	

		float lookVertical = Input.GetAxis ("R Vertical J2");
		Vector3 facingV = new Vector3 (0, lookVertical, 0);

		float action = Input.GetAxis ("RT J2");


		//============= Input Detection ==================
		// Detect lateral movement
		if (!(attacking && !jumping)) {
			if (Mathf.Abs(new_offset.x) > 0) {
				new_offset += new_offset * speed;
				this.GetComponent<Animator> ().SetBool ("running", true);
			}
		}

		// Direction Input
		if (facingH.x < 0 && facingH.x < -.5) {
			spearDir = "left";
		} else if (facingH.x > 0 && facingH.x > .5) {
			spearDir = "right";
		} else if (facingV.y < 0 && Mathf.Abs(facingH.x) < .5) {
			spearDir = "up";
		}

		// Attack Input
		if (!attacking) {
			if (spearDir == "left" && action > .2) {
				source.PlayOneShot(attackSound, attackSoundVol);
				this.GetComponent<Animator> ().SetInteger ("attack_state", 2);
				transform.localScale = new Vector3(-1f, 1f, 1f);
				spearBoxes [0].SetActive (true);
				spearBoxes [1].SetActive (false);
				attacking = true;
			} else if (spearDir == "right" && action > .2) {
				source.PlayOneShot(attackSound, attackSoundVol);
				this.GetComponent<Animator> ().SetInteger ("attack_state", 3);
				transform.localScale = new Vector3(1f, 1f, 1f);
				spearBoxes [0].SetActive (true);
				spearBoxes [1].SetActive (false);
				attacking = true;
			} else if (spearDir == "up" && action > .2) {
				source.PlayOneShot(attackSound, attackSoundVol);
				this.GetComponent<Animator> ().SetInteger ("attack_state", 1);
				spearBoxes [0].SetActive (false);
				spearBoxes [1].SetActive (true);
				attacking = true;
			}
		}

		// If Jump input detected, set the state to the rising jump phase
		if (Input.GetButtonDown("A/X J2") && !jumping && !attacking) 
		{
			jumping = true;
			source.PlayOneShot(jumpSound, jumpSoundVol);
			// notify animator that we are jumping
			this.GetComponent<Animator> ().SetInteger ("jump_state", 1);
		}
		//============================================


		if (jumping) {
			float vertVelocity = (jumpPower - (jumpDecel * currJumpTime));
			if (vertVelocity >= 0) {
				gameObject.layer = 14;
			}
			if (vertVelocity < 0) {
				gameObject.layer = 8;
				// notify animator that we are falling
				this.GetComponent<Animator> ().SetInteger ("jump_state", 2);
			} else {
				new_offset += Vector3.up * vertVelocity;
			}
			currJumpTime += Time.deltaTime;
		}

		//===================== TRANSFORM POSITION UPDATE =================
		Vector3 target_point = transform.position += new_offset * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target_point, 5);

		if (facingH.x < 0) {
			if (!attacking) {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
		} else if (facingH.x > 0) {
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
		if (c.collider.gameObject.tag == "Platform" || c.collider.gameObject.tag == "Shield_Platform" 
			|| c.collider.gameObject.layer == 16)
		{
			jumping = false;
			//notify animator that we are no longer jumping
			this.GetComponent<Animator> ().SetInteger ("jump_state", 0);
			currJumpTime = 0f;
		}
	}

	void onTriggerEnter2D(Collider2D c) {
		if (c.gameObject.layer.Equals ("Projectiles")) {
			print ("You got hit!");
		}
	}

	public void attackReady(){
		this.GetComponent<Animator> ().SetInteger ("attack_state", 0);
		spearBoxes [0].SetActive (false);
		spearBoxes [1].SetActive (false);
		attacking = false;
	}
}