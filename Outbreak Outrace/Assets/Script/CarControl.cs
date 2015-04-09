using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// This script will assume how joystick works, the varible for that will be controlled by mouseOri and movedMouse
/// </summary>


public class CarControl : MonoBehaviour {
	public Rigidbody2D carShell; //To control the movment of the car
	public Text carSta, carHealthDis;
	public Transform[] wheels;
	public float jumpHeight, carHealth, carAr;
	public bool haveSpike;
	public Camera viewPortManipu;

	private float speed, mouseMovedOff, slowed, score, nitrate, carToGroundDis;
	private float displaySpeed;
	private float acceleration = 100f;
	//private float deceleration = -50f;
	private Vector2 mouseOri, movedMouse;
	private RaycastHit2D isFloored, AirToGroundRC;
	private int spikeSpateEndu, niGas;
	private Vector3 carOriRot;

	// Use this for initialization
	void Start () {
		carToGroundDis = 0;
		niGas = 1;
		carOriRot = transform.localEulerAngles;
		mouseOri = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		score = 0;
		slowed = 1; //affect the car's current speed
		nitrate = 1;
		spikeSpateEndu = 0;
		DisplayHealthScore(carHealth, carAr, score);
	}
	
	// Update is called once per frame
	void Update () {

		//read input from controller
		//also works with keyboard keys leftArrow and rightArrow without additional code
		var input = Input.GetAxis ("Horizontal");
		//Debug.Log (input);
		speed = (input * acceleration * slowed * nitrate);

		//Check to see if the car is grounded.
		isFloored = Physics2D.Raycast(transform.position, Vector2.up, -2, 1 << LayerMask.NameToLayer("Road")); 
		if (isFloored.collider != null) 
		{
			//Acceleration
			if (input > 0)
			{
				carShell.AddRelativeForce(Vector2.right * speed, ForceMode2D.Force);
				//carShell.AddRelativeForce (Vector2.right * input * acceleration * slowed * nitrate, ForceMode2D.Force);
			}

			if (carShell.velocity.magnitude > 0.01) //If the car is grounded and moving 
				//Fix required. This code checks if the carShell is moving at all. As a result, it will trigger even if the car is falling or airborne.
			{
				if(Input.GetMouseButtonDown(0)){
					carShell.AddRelativeForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);} //makes the car jump 

				//Deceleration	
				//TODO: Deceleration may not interact with "slowed" properly. Untested. 
				if (input < 0)
				{
					if(speed > 0){
						carShell.AddRelativeForce(Vector2.right * speed, ForceMode2D.Force);}
				}
			
			}
		}

		//If the car is moving
		//Fix required. This code checks if the carShell is moving at all. As a result, it will trigger even if the car is falling or airborne.
		if (carShell.velocity.magnitude > 0.01) 
		{
			//Illusion for the wheels
				wheels[0].Rotate(Vector3.forward * -speed, Space.Self);
				wheels[1].Rotate(Vector3.forward * -speed, Space.Self);

			//using the nitrate
			if (Input.GetMouseButtonDown(1)) {
				nitrate = niGas;
			}

			//creates a graduation of slowing down from using the nitrate
			if (nitrate > 1) {
				nitrate -= Time.smoothDeltaTime;
			}
		}


		
		displaySpeed = speed; //ATTENTION: Displaying speed is currently broken.
		//This currently displays the vehicle's acceleration. 
		carSta.text = displaySpeed.ToString ("F2") + "km/s"; //Displaying the car's speed
		Debug.Log ("This is the nitrate value: " + nitrate);


		//TODO: Pull the code that isn't related to controlling the car (eg interaction with pickups), out of CarControl.cs and into a different Car script
		

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
			niGas++;
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

	void LateUpdate() {
		//For air to ground calculation
		AirToGroundRC = Physics2D.Raycast(transform.position, Vector2.up, -100, 
		                                  1 << LayerMask.NameToLayer("Road"));
		carToGroundDis = Vector2.Distance (transform.position, AirToGroundRC.point);
		Debug.Log (AirToGroundRC.point);
		viewPortManipu.orthographicSize = carToGroundDis + 10; //More zoomed out when the car is air travelling.
		
		//to prevent over zooming
		if (viewPortManipu.orthographicSize > 25) {
			viewPortManipu.orthographicSize = 20;
		}
	}

	void DisplayHealthScore (float h, float a, float s) {
		carHealthDis.text = "health: " + h.ToString () + "\n Scrap Metal: " + a.ToString () 
			+ "\n Exp: " + s.ToString () + "\n SP: " + spikeSpateEndu.ToString () + "\n current Gas meter: " + nitrate.ToString ();
	}
}
