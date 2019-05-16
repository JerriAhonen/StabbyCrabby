using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour {

    public static UIManager Instance { get; set; }

    public TMP_Text timerText;
    public TMP_Text comboText;
    public TMP_Text pointsText;
    public TMP_Text tempPointsText;
    public TMP_Text bulletsText;

    public TMP_Text killedbyText;
    public TMP_Text yourTimeText;
    public TMP_Text yourPointsText;
    public TMP_Text bestComboText;
    //public TMP_Text powerStabsText;
    public TMP_Text bulletsUsedText;
    public TMP_Text enemiesKilledText;

    public TMP_Text lastEnemyText;
    public TMP_Text winTimeText;
    public TMP_Text winPointsText;
    public TMP_Text winBestComboText;
    //public TMP_Text powerStabsText;
    public TMP_Text winBulletsUsedText;
    public TMP_Text winEnemiesKilledText;


    public GameObject gameUI;
    public GameObject endScreenUI;
    public GameObject pauseMenuUI;
    public GameObject winScreenUI;
    
    public GameObject[] bullets = new GameObject[6];
    public int bulletCount;
    private int bulletsUsed = 0;

    private Animator _pauseMenuAnim;
    public Animator gameUIAnim;
    public Animator pauseMenuContinueButtonAnim;
    
    private float timer;
    public int combo;
    private int bestCombo = 0;
    private int enemiesKilled = 0;
    private int points;
    private int tempPoints;
    private float comboResetTime;
    
    private bool gameFinished = false;

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
        winScreenUI.SetActive(false);

        UpdateBullets();
    }
	
	void Update () {
        if (!gameFinished) {
            Timer();
        }
        
        ComboResetTimer();
    }

    // Update current combo amount.
    public void ComboMeter(bool kill)
    {
        // Change this to "If you kill something"
        if (kill)
        {
            combo++;
            gameUIAnim.SetTrigger("Refresh Combo");
            comboResetTime = 4.0f;

            if (combo > bestCombo) {
                bestCombo = combo;
            }

            enemiesKilled++;
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
            comboText.fontSize = 35.0f + (35.0f * (comboResetTime/2));

            comboResetTime -= Time.deltaTime;
            if (comboResetTime <= 0)
            {
                int newPoints = tempPoints * combo;
                Points(newPoints);

                tempPoints = 0;
                combo = 0;
                comboText.text = combo.ToString();
                UpdateTempPoints();
            }
        }
    }

    public void TempPoints(int tempPoints)
    {
        this.tempPoints += tempPoints;
        UpdateTempPoints();
    }

    private void UpdateTempPoints()
    {
        if (tempPoints > 0)
            tempPointsText.text = tempPoints.ToString();
        else
            tempPointsText.text = "";
    }

    public void SetBulletCount(int amount)
    {
        bulletCount = amount;
        UpdateBullets();
    }

    public void AddBullet()
    {
        bulletCount++;
        UpdateBullets();
    }

    public void RemoveBullet()
    {
        bulletCount--;
        bulletsUsed++;
        UpdateBullets();
    }

    public void UpdateBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
        }

        for (int i = 0; i < bulletCount; i++)
        {
            bullets[i].SetActive(true);
        }

        bulletsText.text = bulletCount.ToString() + "/6";
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

    //TEMP QUICK GAMEOVER
    public void GameOver(GameObject killer) {
        gameFinished = true;

        killedbyText.text = "Killed by:  " + killer.tag;
        yourTimeText.text = timerText.text;
        yourPointsText.text = pointsText.text;
        bestComboText.text = bestCombo.ToString();
        //powerStabsText.text = "";
        bulletsUsedText.text = bulletsUsed.ToString();
        enemiesKilledText.text = enemiesKilled.ToString();

        endScreenUI.SetActive(true);
    }

    //TEMP QUICK WIN
    public void Win(GameObject enemyKilled) {
        gameFinished = true;

        lastEnemyText.text = "Last enemy killed:  " + enemyKilled.tag;
        winTimeText.text = timerText.text;
        winTimeText.text = timerText.text;
        winPointsText.text = pointsText.text;
        winBestComboText.text = bestCombo.ToString();
        //winPowerStabsText.text = "";
        winBulletsUsedText.text = bulletsUsed.ToString();
        winEnemiesKilledText.text = enemiesKilled.ToString();

        winScreenUI.SetActive(true);
    }
}
