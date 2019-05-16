using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //private InputReader _inputReader;
    private AudioManager _am;
    private Animator _cameraAnim;
    
    public GameObject mainMenuCanvas;
    public GameObject optionsCanvas;
    
    public enum Tab
    {
        MainMenu = 0,
        Options = 1,
        Controls = 2,
        Credits = 3
    }

    void Start ()
    {
        mainMenuCanvas = GameObject.FindGameObjectWithTag("MainMenuCanvas");
        optionsCanvas = GameObject.FindGameObjectWithTag("OptionsCanvas");
        _cameraAnim = Camera.main.GetComponent<Animator>();
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);

        _am = AudioManager.Instance;
        _am.Play("Song");
    }
	
	void Update ()
    {
		
	}

    public void ChangeMMTab(int tab)
    {
        if (tab == (int)Tab.MainMenu)
        {
            _cameraAnim.SetTrigger("ToMainmenu");

            mainMenuCanvas.SetActive(true);
            optionsCanvas.SetActive(false);
        }
        else if (tab == (int)Tab.Options)
        {
            _cameraAnim.SetTrigger("ToOptions");

            mainMenuCanvas.SetActive(false);
            optionsCanvas.SetActive(true);
        }
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadSceneAsync(sceneNum);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
