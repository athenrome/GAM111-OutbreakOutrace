using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
///
/// </summary>


public class CarControl : MonoBehaviour {
	public Rigidbody2D carShell; //To control the movment of the car
	public Text carSta;
	public Transform[] wheels;
	public float jumpHeight, nitrate;
	public bool haveSpike;
	public Camera viewPortManipu;
	public int niGas;

	private float speed, mouseMovedOff, slowed, score, carToGroundDis;
	private float displaySpeed;
	private float acceleration = 100f;
	//private float deceleration = -50f;
	//private Vector2 mouseOri, movedMouse;
	private RaycastHit2D isFloored, AirToGroundRC;
	private Vector3 carOriRot;

	// Use this for initialization
	void Start () {
		carToGroundDis = 0;
		niGas = 1;
		carOriRot = transform.localEulerAngles;
		//mouseOri = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		score = 0;
		slowed = 1; //affect the car's current speed
		nitrate = 1;
	}
	
	// Update is called once per frame
	void Update () {

		//read input from controller
		//also works with keyboard keys leftArrow and rightArrow without additional code
		var input = Input.GetAxis ("Horizontal");
		//Debug.Log (input);
		speed = (input * acceleration * slowed * nitrate);

		//Check to see if the car is grounded.
		isFloored = Physics2D.Raycast(transform.position, transform.up, -4, 1 << LayerMask.NameToLayer("Road")); 
		//Debug.DrawRay (transform.position, transform.up * -4, Color.red);
		//Debug.Log (isFloored.collider.gameObject);
		if (isFloored.collider != null)
		{
			//Acceleration
			if (input != 0)
			{
				carShell.AddRelativeForce(Vector2.right * speed, ForceMode2D.Force);
				//carShell.AddRelativeForce (Vector2.right * input * acceleration * slowed * nitrate, ForceMode2D.Force);
			}  

			if (carShell.velocity.magnitude > 0.01) //If the car is grounded and moving 
				//Fix required. This code checks if the carShell is moving at all. As a result, it will trigger even if the car is falling or airborne.
			{
				if(Input.GetMouseButtonDown(0)){
					carShell.AddRelativeForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);//makes the car jump 
					carShell.AddRelativeForce(Vector2.right * speed, ForceMode2D.Impulse); //create an arh jump.
				} 

				//Deceleration	
				//TODO: Deceleration may not interact with "slowed" properly. Untested. 
				//SOF: current deceleration is the same as reversing.
				/*if (input < 0)
				{
					if(speed > 0)
					{
						carShell.AddRelativeForce(Vector2.right * speed, ForceMode2D.Force);
					}
				}*/			
			}

			//using the nitrate
			if (Input.GetMouseButtonDown(1)) {
				nitrate = niGas;
			}
			
			//creates a graduation of slowing down from using the nitrate
			if (nitrate > 1) {
				nitrate -= Time.smoothDeltaTime;
			}
		}

		//If the car is moving
		//Fix required. This code checks if the carShell is moving at all. As a result, it will trigger even if the car is falling or airborne.
		if (carShell.velocity.magnitude > 0.01) 
		{
			//Illusion for the wheels
				wheels[0].Rotate(Vector3.forward * -speed, Space.Self);
				wheels[1].Rotate(Vector3.forward * -speed, Space.Self);
		}


		
		displaySpeed = speed; //ATTENTION: Displaying speed is currently broken.
		//This currently displays the vehicle's acceleration. 
		carSta.text = displaySpeed.ToString ("F2") + "km/s"; //Displaying the car's speed
		//Debug.Log ("This is the nitrate value: " + nitrate);


		//TODO: Pull the code that isn't related to controlling the car (eg interaction with pickups), out of CarControl.cs and into a different Car script
	}

	void LateUpdate() {
		//For air to ground calculation
		AirToGroundRC = Physics2D.Raycast(transform.position, Vector2.up, -100, 
		                                  1 << LayerMask.NameToLayer("Road"));
		carToGroundDis = Vector2.Distance (transform.position, AirToGroundRC.point);
		viewPortManipu.orthographicSize = carToGroundDis + 20; //More zoomed out when the car is air travelling.
		
		//to prevent over zooming
		if (viewPortManipu.orthographicSize > 25) {
			viewPortManipu.orthographicSize = 40;
		}
	}
}
