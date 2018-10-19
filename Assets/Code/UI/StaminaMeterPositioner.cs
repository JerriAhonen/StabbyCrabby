using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaMeterPositioner : MonoBehaviour {

    public GameObject staminaMeter;
	
	// Update is called once per frame
	void Update () {
        Vector3 staminaPos = Camera.main.WorldToScreenPoint(transform.position);
        staminaMeter.transform.position = staminaPos;
	}
}
