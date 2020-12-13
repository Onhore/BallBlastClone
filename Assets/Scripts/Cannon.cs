using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Move options")]
    [SerializeField] private HingeJoint2D[] Wheels;
    [SerializeField] private float CannonSpeed;
	private Rigidbody2D rigidBody;
	private JointMotor2D motor;
	private bool isMoving = false;
	private Vector2 mousePosition;
	private float screenBounds;
	private float velocityX;

    private void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		mousePosition = rigidBody.position;
		motor = Wheels[0].motor;
		screenBounds = GameManager.Instance.ScreenWidth - 0.56f;
	}

    private void Update()
	{
		isMoving = Input.GetMouseButton(0);

		if (isMoving) 
        {
			mousePosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		}
	}

    private void FixedUpdate()
	{
		if (isMoving) 
			rigidBody.MovePosition(Vector2.Lerp(rigidBody.position, mousePosition, CannonSpeed * Time.fixedDeltaTime));
		else 
			rigidBody.velocity = Vector2.zero;
		
		velocityX = rigidBody.GetPointVelocity(rigidBody.position).x;

		if (Mathf.Abs (velocityX) > 0.0f && Mathf.Abs(rigidBody.position.x) < screenBounds) 
        {
			motor.motorSpeed = velocityX * 150f;
			MotorActivate (true);
		} 
        else 
        {
			motor.motorSpeed = 0f;
			MotorActivate (false);
		}
	}

	void MotorActivate (bool isActive)
	{
		Wheels[0].useMotor = isActive;
		Wheels[1].useMotor = isActive;

		Wheels[0].motor = motor;
		Wheels[1].motor = motor;
	}

}
