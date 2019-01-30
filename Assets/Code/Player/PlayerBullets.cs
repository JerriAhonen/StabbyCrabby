using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour {

    public int Bullets { private set; get; }

	// Use this for initialization
	void Start () {
        // Give the player one bullet at the start of the round.
        Bullets = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider trigger)
    {
        
    }
}
