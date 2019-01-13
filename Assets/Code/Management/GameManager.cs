using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; set; }
    
    private InputReader _inputReader;
    
    public GameObject gameUI;
    public GameObject endScreenUI;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        _inputReader = InputReader.Instance;
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        endScreenUI = GameObject.FindGameObjectWithTag("EndScreenUI");
        pauseMenuUI = GameObject.FindGameObjectWithTag("PauseMenuUI");

        gameUI.SetActive(true);
        endScreenUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && _inputReader.Esc) Esc();
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadSceneAsync(sceneNum);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void Esc()
    {
        // Toggle pause menu on off

        endScreenUI.SetActive(true);
    }

    /// <summary>
    /// Change the speed at which time passes.
    /// </summary>
    /// <param name="scale">1=Normal speed, 0=Paused</param>
    public void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}