using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Menu : MonoBehaviour {

	public Button StartGame;
	public Button OtherOption;
	public Button OtherOption2;
	public Button OtherOption3;
	public Button OtherOption4;

	// Use this for initialization
	void Start () {
	
		StartGame = StartGame.GetComponent<Button>();
		OtherOption = OtherOption.GetComponent<Button>();
		OtherOption2 = OtherOption2.GetComponent<Button>();
		OtherOption3 = OtherOption3.GetComponent<Button>();
		OtherOption4 = OtherOption4.GetComponent<Button>();
	}

	public void startLevel()

	{
		Application.LoadLevel (1);
	}

	public void ComingSoon()
		
	{
		Application.LoadLevel (2);
	}

	public void Quit()

	{
		Application.Quit();
	}


}
