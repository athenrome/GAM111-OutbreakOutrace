using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Timer : MonoBehaviour {

	public Text Timertext;
	//public UnityEngine.UI.Text Timertext;
	public float Currenttime = 0;
	public bool FinishedPressed = false;
	public bool PausedPressed = false;

	public Button Finish;

	float multiplier = 1;
	
	public Button Pu;
	public Button Z;
	public Button H;

	public Button Restart;
	public Canvas RestartCanvas;
	public Canvas FinishCanvas;



	// Use this for initialization
	void Start () {

		Finish = Finish.GetComponent<Button>();
		Restart = Restart.GetComponent<Button>();
		RestartCanvas = RestartCanvas.GetComponent<Canvas>();
		FinishCanvas = FinishCanvas.GetComponent<Canvas>();
		
		Pu = Pu.GetComponent<Button>();
		Z = Z.GetComponent<Button>();
		H = H.GetComponent<Button>();

		RestartCanvas.enabled = false;
	

	}

	public void Pausepress()
	{
		PausedPressed = true;
	}

	public void UnPausepress()
	{
		PausedPressed = false;
	}

	
	public void FinishPress()
	{
		
		Pu.enabled = false;
		Z.enabled = false;
		H.enabled = false;
		FinishedPressed = true;
		RestartCanvas.enabled = true;
		FinishCanvas.enabled = false;
		

		
		if(Currenttime <= 10)
		{
			multiplier=4;
		}
		else if(Currenttime <= 20)
		{
			multiplier=3;
		}
		else if(Currenttime <= 30)
		{
			multiplier=2;
		} else {multiplier=1;}



	}

	public bool PressedX ()
	{
		return FinishedPressed;
	}



	public float ScoremultiplierX ()
	{
		return multiplier;
	}


	public void SetValue()
	{
		if(!FinishedPressed)
		{
		Timertext.text = Currenttime.ToString();
		}
	}


	
	// Update is called once per frame


	void Update () {

		if(!PausedPressed)
		{
			Currenttime += Time.deltaTime;
		}

		if(FinishedPressed == true)
		{
			ScoremultiplierX();
		}

		SetValue();
	
	}



public void RestartSimulation()

	{
		Pu.enabled = true;
		Z.enabled = true;
		H.enabled = true;
		FinishedPressed = false;
		RestartCanvas.enabled = false;
		FinishCanvas.enabled = true;

		Currenttime = 0;
	}


















}







