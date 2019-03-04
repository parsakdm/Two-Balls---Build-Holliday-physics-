using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class UIManager : MonoBehaviour
{
	public Animator animOfStartBtns;
	public string keshsanScene, nakeshsanScene;
	public GameObject settingPanel;

	public Dropdown firstBallColor;
	public Dropdown secondBallColor;

	private string path = "data.txt";
	
	public void OnClick_Exit ()
	{
		Application.Quit();
	}

	public void OnClick_Keshsan ()
	{
		writeDataToFile();

		SceneManager.LoadScene( 1 );
	}

	public void OnClick_NaKeshsan ()
	{
		writeDataToFile();

		SceneManager.LoadScene( 2 );
	}
	
	private void writeDataToFile ()
	{
		StreamWriter streamWriter = new StreamWriter( path , false );

		streamWriter.WriteLine( firstBallColor.value );
		streamWriter.WriteLine( secondBallColor.value );

		streamWriter.Close();
	}
}
