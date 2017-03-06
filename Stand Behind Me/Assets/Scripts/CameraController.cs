using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject shield;
	public GameObject spear;
	public Transform target;
    public float smoothTime = 0.3f;

	private Vector3 playerCenter;
    private Vector3 velocity = Vector3.zero;

	void Start () {
//		playerCenter = Mathf.Abs(playerCenter);
	}

	void LateUpdate () {
		playerCenter = shield.transform.position;
		playerCenter.y = .5f;
		playerCenter.z = -10f;
		playerCenter.x = playerCenter.x + (spear.transform.position.x - playerCenter.x) / 2;
		transform.position = Vector3.SmoothDamp(transform.position, playerCenter, ref velocity, smoothTime);
	}
}
