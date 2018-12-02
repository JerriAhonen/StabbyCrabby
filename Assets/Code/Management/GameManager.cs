using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; set; }

    // MAIN MENU CAMERA

    /*
    public GameObject mmCameraPos;
    public GameObject optionsCameraPos;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private bool _cameraIsMoving;
    */
    private Animator _cameraAnim;

    // MAIN MENU CANVAS

    public GameObject mainMenuCanvas;
    public GameObject optionsCanvas;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)  // Main Menu
        {
            mainMenuCanvas = GameObject.FindGameObjectWithTag("MainMenuCanvas");
            optionsCanvas = GameObject.FindGameObjectWithTag("OptionsCanvas");
            _cameraAnim = Camera.main.GetComponent<Animator>();
            mainMenuCanvas.SetActive(true);
            optionsCanvas.SetActive(false);
        }
        
    }

    private void Update()
    {

    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadSceneAsync(sceneNum);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeMMTab(int tab)
    {
        if (tab == 0)   // Main Menu
        {
            /*
            _targetPosition = mmCameraPos.transform.position;
            _targetRotation = mmCameraPos.transform.rotation;

            _cameraIsMoving = true;
            */

            _cameraAnim.SetTrigger("ToMainmenu");

            mainMenuCanvas.SetActive(true);
            optionsCanvas.SetActive(false);
        }
        else if (tab == 1)  // Options
        {
            /*
            _targetPosition = optionsCameraPos.transform.position;
            _targetRotation = optionsCameraPos.transform.rotation;

            _cameraIsMoving = true;
            */

            _cameraAnim.SetTrigger("ToOptions");

            mainMenuCanvas.SetActive(false);
            optionsCanvas.SetActive(true);
        }
    }

    public void Esc()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)  // Main Menu
        {
            Quit();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)  // Workshop
        {

        }
    }

    public void GetReferences()
    {

    }
}
/*
    void MoveCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        if (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.1f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                                                        targetPosition,
                                                        5f * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation,
                                                                targetRotation,
                                                                5f * Time.deltaTime);
        }
        else
        {
            _cameraIsMoving = false;
        }
        
    }
    */
