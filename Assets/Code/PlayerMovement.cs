using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public InputReader inputReader;

    public float movementSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    
	void Start () {
        inputReader = InputReader.Instance;
	}
	
	void Update () {
        Move();
        Rotate();
	}

    void Move()
    {
        Vector3 movementVector = inputReader.MovementVector;
        if (Mathf.Abs( movementVector.x ) > Mathf.Abs( movementVector.z ))
        {
            movementSpeed = 15f;
        }
        else
        {
            movementSpeed = 5;
        }

        transform.Translate(inputReader.MovementVector * movementSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        if (inputReader.IsMoving)
        {
            // Rotate the player with the camera
            // We could use a Lerp, so that when the player starts to move again the transition is smooth
            Quaternion rot = inputReader.LocalRotation;
            rot.x = 0;
            rot.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Do not rotate player
        }
    }
}
