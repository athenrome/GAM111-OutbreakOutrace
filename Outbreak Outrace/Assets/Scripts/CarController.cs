using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

    public PlayerCar Player;
	private Rigidbody r;

    GameController Game = new GameController();

	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody> ();
        Player = Game.CreateNewPlayer();
	
	}
	
	// Update is called once per frame
	void Update () {

        DoPlayerMovement();
	
	}

    void DoPlayerMovement()
    {
		if (Input.GetKey (KeyCode.Space)) {
            
			Accelerate ();

		} else {
			Deccelerate ();
		}

		//Player.PlayerPos.x += Player.PlayerSpeed;

		//this.transform.position = Player.PlayerPos;

		///better to use Rigidbody AddForce instead, works better on the ramp (JC);

    }

    void Accelerate()
    {

        print("Accelerating");

		r.AddForce (Vector3.right * 50 * Time.deltaTime, ForceMode.Impulse);
        //Player.PlayerSpeed = Player.PlayerSpeed += Time.deltaTime; 
    }
    
    void Deccelerate()
    {
        if(Player.PlayerSpeed > 0)
        {
            print("Decelerating");

			r.AddForce (Vector3.left * 50 * Time.deltaTime, ForceMode.Acceleration);
            //Player.PlayerSpeed -= Time.deltaTime;
        }
        
    }

}
