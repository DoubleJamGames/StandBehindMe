using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour
{

    public ParticleSystem Explode;
    private ParticleSystem clone;
    
	void OnTriggerEnter2D(Collider2D c)
    {
		if (c.gameObject.tag.Equals("Player")) {
			print ("Drats. You got hit!");
			Destroy (gameObject);
		} else if (c.gameObject.tag.Equals("Shield")) {
			print ("You blocked a projectile. Good job");
			Destroy (gameObject);

            clone = Instantiate(Explode, c.transform.position, Explode.transform.rotation);
   
        }
        else if (c.gameObject.tag.Equals("Shield_Platform"))
        {
            Destroy(gameObject);
            clone = Instantiate(Explode, c.transform.position, Explode.transform.rotation);
        }
    }
}
