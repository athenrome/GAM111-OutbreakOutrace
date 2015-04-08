using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public PlayerCar Player1;
    public PlayerCar Player2;

	// Use this for initialization
	void Start () {
        CreateNewPlayer();	
	}

    public PlayerCar CreateNewPlayer()
    {

        PlayerCar NewPlayer = new PlayerCar(GetPlayerName(), 0, 100, 20, new Vector2(0, 0), 0f, false, false);

        if(Player1 == null)
        {
            Player1 = NewPlayer;
        }
        else
        {
            Player2 = NewPlayer;
        }
        
        return NewPlayer;
    }
	
    string GetPlayerName()
    {
        return "Default";
    }
    
	// Update is called once per frame
	void Update () {
	
	}
}
