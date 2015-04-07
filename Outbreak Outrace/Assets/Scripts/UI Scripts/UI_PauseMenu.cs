using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_PauseMenu : MonoBehaviour {

	public Canvas PauseCanvas;
	public Button Pu;
	public Button Z;
	public Button H;
	public Button R;
	public Button F;
	public Button P;
	
	public Button Unpause;
	public Button menu;



	public void Pausepress()

	{
		PauseCanvas.enabled = true;

		Pu.enabled = false;
		Z.enabled = false;
		H.enabled = false;
		P.enabled = false;
		R.enabled = false;
		F.enabled = false;
	}

	public void UnPausepress()

	{
		PauseCanvas.enabled = false;
	
		Pu.enabled = true;
		Z.enabled = true;
		H.enabled = true;
		P.enabled = true;
		R.enabled = true;
		F.enabled = true;

	}

	public void menuPressed()

	{
		Application.LoadLevel (0);
	}

	// Use this for initialization
	void Start () {

		Pu = Pu.GetComponent<Button>();
		Z = Z.GetComponent<Button>();
		H = H.GetComponent<Button>();
		R = R.GetComponent<Button>();
		F = F.GetComponent<Button>();
		P = P.GetComponent<Button>();

		Unpause = Unpause.GetComponent<Button>();
		menu = menu.GetComponent<Button>();



		PauseCanvas = PauseCanvas.GetComponent<Canvas>();
		PauseCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
