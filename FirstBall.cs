using UnityEngine;

public class FirstBall : MonoBehaviour
{
	public Rigidbody ballRigidBody;
	public Vector3 v0;

	public float size;

	public SecondBall secondBall;

	public UI ui;

	private bool isMoving = false;

	private float firstBallMass;
	private float secondBallMass;
	private Vector3 firstBallVelocity;
	private Vector3 secondBallVelocity;

	public void init ()
	{
		this.transform.localScale = size * Vector3.one;

		firstBallMass = ballRigidBody.mass;
		secondBallMass = secondBall.ballRigidBody.mass;
	}

	public void speedUp ()
	{
		init();

		ballRigidBody.velocity = v0;

		isMoving = true;
	}

	private void OnCollisionEnter ( Collision collision )
	{
		if ( collision.gameObject.tag == "Ball" )
		{
			firstBallVelocity = ( firstBallMass - secondBallMass ) / ( firstBallMass + secondBallMass ) * v0
				+ ( 2 * secondBallMass ) / ( firstBallMass + secondBallMass ) * secondBall.v0;

			secondBallVelocity = ( secondBallMass - firstBallMass ) / ( firstBallMass + secondBallMass ) * secondBall.v0
				+ ( 2 * firstBallMass ) / ( firstBallMass + secondBallMass ) * v0;

			v0 = firstBallVelocity.x * Vector3.right;
			secondBall.v0 = secondBallVelocity.x * Vector3.right;

			ballRigidBody.velocity = v0;
			secondBall.ballRigidBody.velocity = secondBall.v0;

			ui.showInfoPanel();
		}
		else if ( collision.gameObject.tag == "Wall" )
		{
			v0 = Vector3.zero;
		}
	}

	private void FixedUpdate ()
	{
		if ( isMoving )
			ballRigidBody.velocity = v0;
	}
}
