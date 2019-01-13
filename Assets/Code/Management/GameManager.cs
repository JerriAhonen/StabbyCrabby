using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; set; }
    
    private InputReader _inputReader;
    
    public GameObject gameUI;
    public GameObject endScreenUI;
    public GameObject pauseMenuUI;

    public bool gamePaused;
    public bool gameOver;

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

        gameUI.SetActive(true);
        endScreenUI.SetActive(false);
        pauseMenuUI.SetActive(false);

        MouseLocked(true);
    }

    private void Update()
    {
        if (_inputReader.Esc) Esc();
    }
    
    public void Esc()
    {
        // Toggle pause menu on/off
        gamePaused = !gamePaused;
        
        if (gamePaused)
        {
            pauseMenuUI.SetActive(true);
            MouseLocked(false);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            MouseLocked(true);
            Time.timeScale = 1;
        }
    }

    private void MouseLocked(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadSceneAsync(sceneNum);
    }
}