using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickupInterraction : MonoBehaviour {
	
	public Text carHealthDis;
	public float carAr, carHealth;
	public bool haveSpike;
	
	private float speed, mouseMovedOff, slowed, score, carToGroundDis;
	//private float deceleration = -50f;
	private int spikeSpateEndu;
	private CarControl cc;
	
	// Use this for initialization
	void Start () {
		cc = GetComponent<CarControl> ();
		//mouseOri = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		score = 0;
		spikeSpateEndu = 0;
		DisplayHealthScore(carHealth, carAr, score);
	}
	
	// Update is called once per frame
	void Update () {

		//disable the spike
		if (spikeSpateEndu <= 0) {
			haveSpike = false;
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		PenaltyRewards (c.GetComponent<Collider2D> ().gameObject.tag);
	}
	
	void PenaltyRewards (string hitObTag) {
		float remainDam = 0; //The remaining damage once absorbed by the armour
		
		switch (hitObTag) {
		case "ZombieS": //A single zombie is hit
			score += 10;
			DisplayHealthScore(carHealth, carAr, score);
			break;
		case "ZombieH": //A horde of zombies is hit
			if (!haveSpike) { //This block won't happens if the player have equipped a spike.
				if (carAr > 0) {
					remainDam = 200 * 0.2f;
					carAr -= 200 * 0.8f;
					carHealth -= remainDam; 
				}
				
				if (carAr <= 0) { //armour not active
					carHealth -= 200;
				}
				
				slowed = 0.5f;
			}
			
			if (haveSpike) {
				spikeSpateEndu--;
			}
			
			score += 50;
			DisplayHealthScore(carHealth, carAr, score);
			
			if (carHealth <= 0) {
				Time.timeScale = 0;
				carHealthDis.text = "Gameover";
			}
			
			break;
		case "Barricade": //hit a barricade
			if (!haveSpike) { ////This block won't happens if the player have equipped a spike.
				slowed = 0.1f;
			}
			
			if (haveSpike) {
				spikeSpateEndu--;
			}
			break;
		case "Backpack":
			score += 200;
			DisplayHealthScore(carHealth, carAr, score);
			break;
		case "Nitros":
			cc.niGas++;
			DisplayHealthScore(carHealth, carAr, score);
			break;
		case "Spike":
			spikeSpateEndu = 10;
			haveSpike = true;
			DisplayHealthScore(carHealth, carAr, score);
			break;
		case "Armor":
			carAr = 500;
			DisplayHealthScore(carHealth, carAr, score);
			break;
		}
	}

	void DisplayHealthScore (float h, float a, float s) {
		carHealthDis.text = "health: " + h.ToString () + "\n Scrap Metal: " + a.ToString () 
			+ "\n Exp: " + s.ToString () + "\n SP: " + spikeSpateEndu.ToString () + "\n current Gas meter: " 
				+ cc.nitrate.ToString ();
	}
}
