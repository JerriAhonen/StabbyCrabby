using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl: MonoBehaviour {

    public Slider staminaSlider;
    public InputReader inputReader;

    public int maxStamina;
    int staminaFallRate;
    public int staminaFallMult;
    int staminaRegainRate;
    public int staminaRegainMult;

    public bool canAttack;

	// Use this for initialization
	void Start () {

        inputReader = InputReader.Instance;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;

        staminaFallRate = 1;
        staminaRegainRate = 1;

        canAttack = true;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetButtonDown("Fire1"))
        {
            staminaSlider.value -= 1;
        }

        else
        {
            staminaSlider.value += Time.deltaTime * staminaRegainMult;
        }

        if(staminaSlider.value >= maxStamina)
        {
            staminaSlider.value = maxStamina;
        }

        else if (staminaSlider.value <= 0)
        {
            staminaSlider.value = 0;
            canAttack = false;
        }

        else if (staminaSlider.value >= 1)
        {
            canAttack = true;
        }
	}
}
