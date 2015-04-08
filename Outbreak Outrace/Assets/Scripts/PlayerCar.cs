using UnityEngine;
using System.Collections;

public class PlayerCar : MonoBehaviour {

    public string PlayerName;
    public int PlayerScore;
    public int PlayerHealth;
    public int PlayerArmour;
    public Vector2 PlayerPos;
    public float PlayerSpeed;
    public bool SpikesActive;
    public bool NitroActive;

	
	public PlayerCar(string Pn, int Ps, int Ph, int Pa, Vector2 Position, float Pspeed, bool Sa, bool Na) 
    {
        PlayerName = Pn;
        PlayerScore = Ps;
        PlayerHealth = Ph;
        PlayerArmour = Pa;
        PlayerPos = Position;
        PlayerSpeed = Pspeed;
	}

}
