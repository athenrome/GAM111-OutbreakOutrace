using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Score : MonoBehaviour {

	public Text CurrentScoreText;
	public Text AddedScoreText;




	float CurrentScore = 0;
	float AddedScore = 0;
	float tempScore = 0;
	float multiScore = 0;
	float multi = 0;
	float multiplier = 1;
	float countdown;
	bool ActiveCountdown = false;
	bool stoploop = false;

	UI_Timer Timemultiplier;



	void setValue()

	{
		CurrentScoreText.text = CurrentScore.ToString();

		AddedScoreText.text = AddedScore.ToString();
	}



	public void pickupTemp()

	{
		tempScore = 500;

	}

	public void ZombieTemp()

	{
		tempScore = 100;
	}

	public void HoardTemp ()

	{
		tempScore = 1000;
	}

	public void Scoremultiplier()
	{


		if(Timemultiplier.PressedX() == false)
		{
			stoploop = false;
		}

		if (Timemultiplier.PressedX() == true & stoploop == false)

		{



	

			//if(AddedScore > 0)
			//{
				AddedScoreText.enabled = false;
				CurrentScore += AddedScore;
				AddedScore = 0;
			//}



		stoploop = true;

		multiplier = Timemultiplier.ScoremultiplierX();

		multiScore += CurrentScore;

		multi += multiplier * multiScore; 

		CurrentScore = multi;


		}}





		
		



	void ScoreUpdate()
	{
		if (tempScore != 0)
		{
		AddedScore += tempScore;
		tempScore = 0;
		countdown = 2;
		ActiveCountdown = true;
		AddedScoreText.enabled = true;
		}

		if (ActiveCountdown == true & countdown <= 0)
		{
			ActiveCountdown = false;
			AddedScoreText.enabled = false;
			CurrentScore += AddedScore;
			AddedScore = 0;

		}
	}


	// Use this for initialization
	void Start () {

		AddedScoreText.enabled = false;

		Timemultiplier = FindObjectOfType<UI_Timer>();

	
	}
	
	// Update is called once per frame
	void Update () {

	
		setValue();
		ScoreUpdate();
		Scoremultiplier();


		if (ActiveCountdown == true)
		{
		countdown -= Time.deltaTime;
		
		}
	
	}

	public void RestartScore()
	{
		CurrentScore = 0;
		AddedScore = 0;
		tempScore = 0;
		multiScore = 0;
		multi = 0;
		multiplier = 1;
	}
}
