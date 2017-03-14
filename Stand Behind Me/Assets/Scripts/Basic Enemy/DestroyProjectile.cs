using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour
{

    public ParticleSystem Explode;
    
	void OnTriggerEnter2D(Collider2D c)
    {
		if (c.gameObject.tag.Equals("Player")) {
			print ("Drats. You got hit!");
			Destroy (gameObject);
		} else if (c.gameObject.tag.Equals("Shield")) {
			print ("You blocked a projectile. Good job");
			Destroy (gameObject);

            Instantiate(Explode, c.gameObject.transform.position, c.gameObject.transform.rotation);
            
        }
    }
}
