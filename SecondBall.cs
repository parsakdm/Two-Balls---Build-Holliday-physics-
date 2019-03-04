using UnityEngine;

public class SecondBall : MonoBehaviour
{
	public Rigidbody ballRigidBody;
	public Vector3 v0;

	public float size;

	private bool isMoving = false;

	public void init ()
	{
		this.transform.localScale = size * Vector3.one;
	}

	public void speedUp ()
	{
		init();

		ballRigidBody.velocity = v0;

		isMoving = true;
	}

	private void OnCollisionEnter ( Collision collision )
	{
		if ( collision.gameObject.tag == "Wall" )
		{
			v0 = Vector3.zero;
		}
	}

	private void FixedUpdate ()
	{
		if ( isMoving )
			ballRigidBody.velocity = v0.x * Vector3.right;
	}
}
