using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour {

    public static UIManager Instance { get; set; }

    public TMP_Text timerText;
    public TMP_Text comboText;
    public TMP_Text pointsText;

    public GameObject gameUI;
    public GameObject endScreenUI;
    public GameObject pauseMenuUI;
    public GameObject tempPoints;

    private Animator _pauseMenuAnim;
    public Animator pauseMenuContinueButtonAnim;
    
    private float timer;
    private int combo;
    private int points;
    private float comboResetTime;

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
        
        if (Input.GetButtonDown("Fire1"))
            ComboMeter(true);
        if (Input.GetButtonDown("Fire2"))
            ComboMeter(false);
    }

    // Update current combo amount.
    void ComboMeter(bool kill)
    {
        // Change this to "If you kill something"
        if (kill)
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
            comboText.fontSize = 35.0f + (35.0f * comboResetTime);

            comboResetTime -= Time.deltaTime;
            if (comboResetTime <= 0)
            {
                combo = 0;
                comboText.text = combo.ToString();
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
        {
            pauseMenuUI.SetActive(true);
            _pauseMenuAnim = pauseMenuUI.GetComponent<Animator>();
            _pauseMenuAnim.SetTrigger("Open");
            pauseMenuContinueButtonAnim.SetTrigger("Highlighted");
        }

        else
        {
            // TODO: Allow for the exit Animation before disabling.

            _pauseMenuAnim.SetTrigger("Exit");
            StartCoroutine(DeactivatePauseMenu(0.25f));
        }
    }

    IEnumerator DeactivatePauseMenu(float delay)
    {
        while (delay >= 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }

        pauseMenuUI.SetActive(false);
        yield break;
    }
}
