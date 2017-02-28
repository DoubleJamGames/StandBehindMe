using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {

	public GameObject Players;
	public GameObject Enemy;
    public float speed = 3f;
    public float distance;

    [HideInInspector] public string currentState;
 
	// Use this for initialization
	void Start () {
        currentState = "Look";

		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 offset = Vector3.zero;

        //RaycastHit2D hit = Physics2D.Raycast(Enemy.transform.position, Vector3.left);
        //if (hit.collider.gameObject.tag.Equals("Player"))

        distance = Mathf.Abs(Players.transform.position.x) - Mathf.Abs(Enemy.transform.position.x);

        Debug.Log(distance);

        if (distance <= 2)
        {
            //Call attack
        }
        else if(distance < 10)
        {
            offset = Vector3.left * speed;
        }

        //------Move the enemy-----
        Vector3 target = transform.position += offset * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, 5);
        
	}
}
