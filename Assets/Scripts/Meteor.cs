using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Meteor : MonoBehaviour
{
    
    [Header("Meteor options")]
    [SerializeField] protected Rigidbody2D RigidBody;
	[SerializeField] protected int Health;
	[SerializeField] protected TMP_Text TextHealth;
	[SerializeField] protected float JumpForce;
	[SerializeField] protected float ReflectForce;
	
	public Transform Parent => transform.parent;

    [HideInInspector] public bool IsResultOfFission = true;

	protected float[] leftAndRight = new float[2]{ -1f, 1f };
	protected bool isShowing;

	private void Start()
	{
		UpdateHealthUI();

		isShowing = true;
		RigidBody.gravityScale = 0f;

		if (IsResultOfFission) 
        {
			FallDown();
		} 
        else 
        {
			var direction = leftAndRight[Random.Range (0, 2)];
			var screenOffset = GameManager.Instance.ScreenWidth * 1.3f;

			transform.position = new Vector2(screenOffset * direction, transform.position.y);

			RigidBody.velocity = new Vector2(-direction, 0f);

			Invoke("FallDown", Random.Range(screenOffset - 2.5f, screenOffset - 1f));
		}

	}

	private void FallDown()
	{
		isShowing = false;
		RigidBody.gravityScale = 1f;
		RigidBody.AddTorque(Random.Range (-20f, 20f));
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Equals("Cannon")) 
        {
			Debug.Log ("Game over");
		}

		if (other.tag.Equals("Missile")) 
        {
			TakeDamage (1);
			
			FireAction.Instance.DestroyMissile(other.gameObject);
		}

		if (!isShowing && other.tag.Equals("Wall")) 
        {
			var positionX = transform.position.x;

			if (positionX > 0) 
            {
				RigidBody.velocity = Vector2.zero;
				RigidBody.AddForce(Vector2.left * ReflectForce);
			} 
            else 
            {
				RigidBody.velocity = Vector2.zero;
				RigidBody.AddForce(Vector2.right * ReflectForce);
			}

			RigidBody.AddTorque(positionX * 4f);
		}

		if (other.tag.Equals("Ground")) 
        {
			RigidBody.velocity = new Vector2(RigidBody.velocity.x, JumpForce);
			RigidBody.AddTorque(-RigidBody.angularVelocity * 4f);
		}
	}

	public void TakeDamage(int damage)
	{
		if (Health > 1) 
			Health -= damage;
        else 
			Die ();
		
		UpdateHealthUI ();
	}

	virtual protected void Die() => Destroy (gameObject);

	protected void UpdateHealthUI()
	{
		TextHealth.text = Health.ToString ();
	}
}
