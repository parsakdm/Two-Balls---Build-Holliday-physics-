using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject mainPanel;
	public GameObject modePanel;
	public GameObject settingsPanel;
	public GameObject infoPanel;

	public InputField firstBallVelocity;
	public InputField firstBallMass;
	public InputField secondBallVelocity;
	public InputField secondBallMass;

	private string path = "data.txt";

	public void clickedOnStartButton ()
	{
		writeDataToFile();

		mainPanel.SetActive( false );
		modePanel.SetActive( true );
	}

	public void clickedOnSettingsButton ()
	{
		settingsPanel.SetActive( true );
	}

	public void clickedOnInfoButton ()
	{
		infoPanel.SetActive( true );
	}

	public void clickedOnElasticButton ()
	{
		SceneManager.LoadScene( 1 );
	}

	public void clickedOnInelasticButton ()
	{
		SceneManager.LoadScene( 2 );
	}

	public void clickedOnReturnButton ()
	{
		settingsPanel.SetActive( false );
		infoPanel.SetActive( false );
	}

	private void writeDataToFile ()
	{
		StreamWriter streamWriter = new StreamWriter( path , false );

		streamWriter.WriteLine( firstBallVelocity.text == "" ? "0" : firstBallVelocity.text );
		streamWriter.WriteLine( firstBallMass.text == "" ? "0" : firstBallMass.text );

		streamWriter.WriteLine( secondBallVelocity.text == "" ? "0" : secondBallVelocity.text );
		streamWriter.WriteLine( secondBallMass.text == "" ? "0" : secondBallMass.text );

		streamWriter.Close();

		TextAsset asset = (TextAsset) Resources.Load( "data" );
	}
}
