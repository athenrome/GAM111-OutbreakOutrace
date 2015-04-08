using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

    public PlayerCar Player;
	Rigidbody RigBody;

    GameController Game = new GameController();

	// Use this for initialization
	void Start () {
        
        RigBody = GetComponent<Rigidbody>();
        Player = Game.CreateNewPlayer();
        this.name = Player.PlayerName;
	
	}
	
	// Update is called once per frame
	void Update () {

        DoPlayerMovement();
	
	}

    void DoPlayerMovement()
    {
		if (Input.GetKey(KeyCode.Space)) {
            
			Accelerate ();

		} 
        else 
        {
			Deccelerate ();
		}

		//Player.PlayerPos.x += Player.PlayerSpeed;

		//this.transform.position = Player.PlayerPos;

		///better to use Rigidbody AddForce instead, works better on the ramp (JC);

    }

    void Accelerate()
    {

        print("Accelerating");
        Player.PlayerSpeed = Player.PlayerSpeed += Time.deltaTime; 

        RigBody.AddForce(Vector3.right * 20 * Player.PlayerSpeed, ForceMode.Impulse);
        
    }
    
    void Deccelerate()
    {
        if(Player.PlayerSpeed > 0)
        {
            print("Decelerating");
            Player.PlayerSpeed -= Time.deltaTime;

            RigBody.AddForce(Vector3.left * 20 * Player.PlayerSpeed, ForceMode.Acceleration);
            
        }
        
    }

}
