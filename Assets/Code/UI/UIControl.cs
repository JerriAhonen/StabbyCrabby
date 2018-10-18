using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl: MonoBehaviour {

    public Slider staminaSlider;
    public Slider specialMoveSlider;

    private InputReader inputReader;
    private PlayerCombat playerCombat;

    public TMP_Text timerText;
    public TMP_Text comboText;
    public TMP_Text pointsText;

    public int maxStamina;
    int staminaFallRate;
    public int staminaFallMult;
    int staminaRegainRate;
    public int staminaRegainMult;

    public int maxSpecial;
    
    float timer;
    int combo;
    int points;
    public float comboResetTime;

	// Use this for initialization
	void Start () {

        inputReader = InputReader.Instance;
        playerCombat = PlayerCombat.Instance;
        
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        specialMoveSlider.maxValue = maxSpecial;
        specialMoveSlider.value = 0;
        staminaFallRate = 1;
        timer = 0f;
        combo = 1;
        points = 0;
        comboResetTime = 4f;
	}
	
	// Update is called once per frame
	void Update () {

        Stamina();
        Timer();
        ComboMeter();
        Points();
        SpecialMove();
	}

    // Displays current stamina and toggles if the player can attack or not.
    void Stamina()
    {
        if (staminaSlider.value == maxStamina)
            staminaSlider.gameObject.SetActive(false);
        else
            staminaSlider.gameObject.SetActive(true);
        
        if (Input.GetButtonDown("Fire1"))
        {
            staminaSlider.value -= staminaFallRate;
        }

        else
        {
            staminaSlider.value += Time.deltaTime * staminaRegainMult;
        }

        if (staminaSlider.value >= maxStamina)
        {
            staminaSlider.value = maxStamina;
        }

        else if (staminaSlider.value <= 0)
        {
            staminaSlider.value = 0;
            playerCombat.canAttack = false;
        }

        else if (staminaSlider.value >= 1)
        {
            playerCombat.canAttack = true;
        }

    }

    // Displays the game timer.
    void Timer()
    {
        timer += Time.deltaTime;

        timerText.text = timer.ToString("F2");
       
    }

    // Update points for killing enemies.
    void Points()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            points = points + 100;
        }
        pointsText.text = "p: " + points.ToString();
    }

    // Update current combo amount.
    void ComboMeter()
    {
        // Change this to "If you kill something"
        if(Input.GetButtonDown("Fire1"))
        {
            combo = combo + 1;
            comboResetTime = 4f;
           
        }
        if(combo > 0)
        {
            comboResetTime -= Time.deltaTime;
            if(comboResetTime <= 0)
            {
                combo = 0;
            }
        }
        comboText.text = combo.ToString();
    }

    // Updates the current state of the SpecialMove bar.
    void SpecialMove()
    {
        if (specialMoveSlider.value == maxSpecial)
        {
            playerCombat.specialMoveIsActive = true;
        }
        else
        {
            playerCombat.specialMoveIsActive = false;
        }

        // Change this to if getting points.
        if (Input.GetButtonDown("Fire1"))
        {
            specialMoveSlider.value += 1;
        }
        
    }
}
