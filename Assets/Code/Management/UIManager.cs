using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    public static UIManager Instance { get; set; }

    public TMP_Text timerText;
    public TMP_Text comboText;
    public TMP_Text pointsText;

    public GameObject gameUI;
    public GameObject endScreenUI;
    public GameObject pauseMenuUI;

    private float timer;
    private int combo;
    private int points;
    private float comboResetTime = 4.0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    
    void Start () {
        gameUI.SetActive(true);
        endScreenUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }
	
	void Update () {
        Timer();
        ComboResetTimer();
	}

    // Update current combo amount.
    void ComboMeter(bool kill)
    {
        // Change this to "If you kill something"
        if (Input.GetButtonDown("Fire1"))
        {
            combo++;
            comboResetTime = 4.0f;
        } 
        else
        {
            comboResetTime = 4.0f;
        }
        
        comboText.text = combo.ToString();
    }

    void ComboResetTimer()
    {
        if (combo > 0)
        {
            comboResetTime -= Time.deltaTime;
            if (comboResetTime <= 0)
            {
                combo = 0;
            }
        }
    }

    // Add points and update UI
    public void Points(int amount)
    {
        points += amount;

        pointsText.text = points.ToString();
    }

    // Displays the game timer.
    void Timer()
    {
        timer += Time.deltaTime;

        timerText.text = timer.ToString("F2");
    }

    // GameManager controls this.
    public void Pause(bool paused)
    {
        if (paused)
            pauseMenuUI.SetActive(true);
        else
            pauseMenuUI.SetActive(false);
    }
}
