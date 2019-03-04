using UnityEngine;

public class InelasticBall : MonoBehaviour
{
	public Rigidbody ballRigidBody;
	public Vector3 v0;

	public InelasticBall otherBall;

	public bool isFirstBall;

	public float size;

	public InelasticUI ui;

	private float horizontalPosition;

	private bool isMoving = false;
	private bool hasAlreadyCollided = false;
	private bool hasCollidedWithWall = false;

	public void init ()
	{
		this.transform.localScale = size * Vector3.one;

		if ( size == 1 )
			horizontalPosition = 0f;
		else
			horizontalPosition = 0.5f;
	}

	public void speedUp ()
	{
		init();

		ballRigidBody.velocity = v0;

		isMoving = true;
	}

	private void OnCollisionEnter ( Collision collision )
	{
		if ( !hasAlreadyCollided && collision.gameObject.tag == "Ball" )
		{
			if ( hasCollidedWithWall )
			{
				otherBall.hasCollidedWithWall = true;

				v0 = Vector3.zero;
				otherBall.v0 = Vector3.zero;
			}

			hasAlreadyCollided = true;

			if ( isFirstBall )
			{
				v0 = ( ballRigidBody.mass * v0 + otherBall.ballRigidBody.mass * otherBall.v0 )
					/ ( ballRigidBody.mass + otherBall.ballRigidBody.mass );

				otherBall.v0 = v0;
			}

			ballRigidBody.velocity = v0.x * Vector3.right;

			ui.showInfoPanel();
		}
		else if ( collision.gameObject.tag == "Wall" )
		{
			hasCollidedWithWall = true;

			v0 = Vector3.zero;

			if ( hasAlreadyCollided )
				otherBall.v0 = Vector3.zero;
		}
	}

	private void FixedUpdate ()
	{
		if ( isMoving )
			ballRigidBody.velocity = v0.x * Vector3.right;

		this.transform.position = new Vector3( this.transform.position.x , horizontalPosition , 0 );
	}
}
