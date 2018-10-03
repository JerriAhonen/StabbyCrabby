using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public InputReader inputReader;

    public float cameraMoveSpeed = 120.0f;
    
    
    void Start ()
    {
        inputReader = InputReader.Instance;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	void Update ()
    {
        
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        // Rotate the camera according to the InputReader input
        transform.rotation = inputReader.LocalRotation;

        // Set the target GO to follow
        Transform target = player.transform;

        // Move towards the target GO
        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
