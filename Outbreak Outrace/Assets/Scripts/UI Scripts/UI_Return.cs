using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Return : MonoBehaviour {

	public Button MainMenu;
	// Use this for initialization
	void Start () {
	
		MainMenu = MainMenu.GetComponent<Button>();

	}
	
	public void Return()
		
	{
		Application.LoadLevel (0);
	}
}
