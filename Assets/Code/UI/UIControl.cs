using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIControl: MonoBehaviour {
    
    public Slider specialMoveSlider;

    private InputReader inputReader;
    private PlayerCombat playerCombat;
    private Health playerHealth;

    public TMP_Text timerText;
    public TMP_Text comboText;
    public TMP_Text pointsText;
    public GameObject endCanvas;
    
    public int maxSpecial;
    
    float timer;
    int combo;
    int points;
    public float comboResetTime;

	// Use this for initialization
	void Start () {

        inputReader = InputReader.Instance;
        playerCombat = PlayerCombat.Instance;

        // THIS SHOULD BE IN A PLAYER SCRIPT SOMEWHERE
        playerHealth = GetComponent<Health>();
        playerHealth.SetHealth(1);

        endCanvas.SetActive(false);
        
        specialMoveSlider.maxValue = maxSpecial;
        specialMoveSlider.value = 0;
        timer = 0f;
        combo = 1;
        points = 0;
        comboResetTime = 4f;
	}
	
	// Update is called once per frame
	void Update () {

        bool stab = inputReader.Stab;
        
        //Stamina(stab);
        Timer();
        ComboMeter(stab);
        Points(stab);
        SpecialMove(stab);
        Death();
	}

    // Displays the game timer.
    void Timer()
    {
        timer += Time.deltaTime;

        timerText.text = timer.ToString("F2");
    }

    // Update points for killing enemies.
    void Points(bool stab)
    {
        if(stab)
        {
            points = points + 100;
        }
        pointsText.text = "p: " + points.ToString();
    }

    // Update current combo amount.
    void ComboMeter(bool stab)
    {
        // Change this to "If you kill something"
        if(stab)
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
    void SpecialMove(bool stab)
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
        if (stab)
        {
            specialMoveSlider.value += 1;
        }
    }

    // Shows the end screen if player dies.
    void Death()
    {
        //SHOULDN'T THIS INFO BE SENT TO UI CONTROL INSTEAD OF UI CONTROL POLLING FOR DEATH EVERY FRAME??

        if (playerHealth.IsDead)
        {
            endCanvas.SetActive(true);

            if (Input.GetButtonDown("Cancel")) {
                SceneManager.LoadScene(1);
            }
        }
    }
}
//public Slider staminaSlider;
//staminaSlider.maxValue = maxStamina;
//staminaSlider.value = maxStamina;
//staminaFallRate = 1;

/*
public int maxStamina;
int staminaFallRate;
public int staminaFallMult;
int staminaRegainRate;
public int staminaRegainMult;
*/

/*
// Displays current stamina and toggles if the player can attack or not.
void Stamina(bool stab)
{
    if (staminaSlider.value == maxStamina)
        staminaSlider.gameObject.SetActive(false);
    else
        staminaSlider.gameObject.SetActive(true);

    if (stab)
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
*/
