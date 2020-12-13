using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFissionable : Meteor
{
    [SerializeField] GameObject splitsPrefab;

	override protected void Die()
	{
		SplitMeteor ();

		Destroy (gameObject);
	}

	void SplitMeteor ()
	{
		GameObject meteor;
        
		for (int i = 0; i < 2; i++) 
        {
			meteor = Instantiate (splitsPrefab, transform.position, Quaternion.identity, Parent);
			meteor.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAndRight[i], 5f);
		}
	}
}
