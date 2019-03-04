using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InelasticUI : MonoBehaviour
{
	public InelasticBall firstBall;
	public InelasticBall secondBall;

	public GameObject settingsPanel;
	public GameObject returnButton;
	public GameObject resetButton;

	public GameObject infoPanel;

	public Text velocityText;

	public InputField firstBallVelocity;
	public InputField firstBallMass;
	public InputField secondBallVelocity;
	public InputField secondBallMass;

	public Dropdown firstBallColor;
	public Dropdown secondBallColor;

	public Material[] colors;

	private string path = "data.txt";

	private void Awake ()
	{
		settingsPanel.SetActive( true );
		returnButton.SetActive( false );
		resetButton.SetActive( false );
	}

	public void showInfoPanel ()
	{
		velocityText.text = "Vf: " + secondBall.v0.x + " m/s";

		infoPanel.SetActive( true );
	}

	public void clickedOnStartButton ()
	{
		firstBall.v0 = ( firstBallVelocity.text == "" ? 0 : int.Parse( firstBallVelocity.text ) ) * Vector3.right;
		firstBall.ballRigidBody.mass = firstBallMass.text == "" ? 0 : int.Parse( firstBallMass.text );

		secondBall.v0 = ( secondBallVelocity.text == "" ? 0 : int.Parse( secondBallVelocity.text ) ) * Vector3.right;
		secondBall.ballRigidBody.mass = secondBallMass.text == "" ? 0 : int.Parse( secondBallMass.text );

		if ( firstBall.ballRigidBody.mass == secondBall.ballRigidBody.mass )
		{
			firstBall.size = 1;
			secondBall.size = 1;
		}
		else if ( firstBall.ballRigidBody.mass > secondBall.ballRigidBody.mass )
		{
			firstBall.size = Mathf.Min( 3f , firstBall.ballRigidBody.mass / secondBall.ballRigidBody.mass );
			secondBall.size = 1;
		}
		else
		{
			firstBall.size = 1;
			secondBall.size = Mathf.Min( 3f , secondBall.ballRigidBody.mass / firstBall.ballRigidBody.mass );
		}

		firstBall.init();
		secondBall.init();

		firstBall.GetComponent<MeshRenderer>().material = colors[ firstBallColor.value ];
		secondBall.GetComponent<MeshRenderer>().material = colors[ secondBallColor.value ];

		settingsPanel.SetActive( false );
		returnButton.SetActive( true );
		resetButton.SetActive( true );

		firstBall.speedUp();
		secondBall.speedUp();
	}

	public void clickedOnReturnButton ()
	{
		SceneManager.LoadScene( 0 );
	}

	public void clickedOnResetButton ()
	{
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
	}

	private void readDataFromFile ()
	{
		StreamReader streamReader = new StreamReader( path );

		firstBall.GetComponent<MeshRenderer>().material = colors[ int.Parse( streamReader.ReadLine() ) ];
		secondBall.GetComponent<MeshRenderer>().material = colors[ int.Parse( streamReader.ReadLine() ) ];

		streamReader.Close();
	}
}
