using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupController : MonoBehaviour {

    GameController Game = new GameController();

    public List<Sprite> PowerupList;

    string CurrentPowerup;

	// Use this for initialization
	void Start () {
        ChoosePowerupType();
	
	}

    void ChoosePowerupType()
    {
        

        int PowerupChooser = Random.Range(1,4);

        switch(PowerupChooser)
        {
            case 1: //Scrap Metal
                CurrentPowerup = "Scrap";
                print("Spawning powerup as: Scrap ");
                Instantiate(PowerupList[0], this.transform.position, this.transform.rotation);
                break;

            case 2: //Spiked Plates
                CurrentPowerup = "Spikes";
                print("Spawning powerup as: Spikes ");
                Instantiate(PowerupList[0], this.transform.position, this.transform.rotation);
                break;

            case 3: //Nitrous
                CurrentPowerup = "Nitrous";
                print("Spawning powerup as: Nitrous ");
                Instantiate(PowerupList[0], this.transform.position, this.transform.rotation);
                break;

            case 4: //Supplies
                CurrentPowerup = "Supplies";
                print("Spawning powerup as: Supplies ");
                Instantiate(PowerupList[0], this.transform.position, this.transform.rotation);
                break;
                
        }
    }
    	
	// Update is called once per frame
	void Update () {
	
	}

    
    void OnCollisionEnter (Collision Col)
    {
        PlayerCar ColPlayer;


        if (Col.gameObject.name == Game.Player1.PlayerName)
        {
            ColPlayer = Game.Player1;
        }
        else
        {
            ColPlayer = Game.Player2;
        }
            switch (CurrentPowerup)
            {
                case "Scrap": //Scrap Metal
                    ColPlayer.PlayerArmour += 3;
                    break;

                case "Spikes": //Spiked Plates
                    ColPlayer.SpikesActive = true;
                    break;

                case "Nitrous": //Nitrous
                    ColPlayer.NitroActive = true;
                    break;

                case "Supplies": //Supplies
                    ColPlayer.PlayerScore += 10;
                    break;
            }        
    }

}
