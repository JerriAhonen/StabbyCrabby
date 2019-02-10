using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; set; }
    
    private InputReader _inputReader;
    private UIManager _uiManager;
    private GarbageCollector _garbageCollector;
    private AudioManager _am;
    
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
        _uiManager = UIManager.Instance;
        _garbageCollector = GarbageCollector.Instance;
        _am = AudioManager.Instance;
        _am.Play("Song");

        Time.timeScale = 1;
        MouseLocked(true);
    }

    private void Update()
    {
        if (_inputReader.Esc) Pause();
    }
    
    public void Pause()
    {
        // Toggle pause menu on/off
        gamePaused = !gamePaused;
        
        if (gamePaused)
        {
            _uiManager.Pause(true);
            _am.ChangePitch("Song", 0.8f);
            MouseLocked(false);
            Time.timeScale = 0;
        }
        else
        {
            _uiManager.Pause(false);
            _am.ChangePitch("Song", 1.0f);
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