using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public InputReader inputReader;

    public float movementSpeed = 5.0f;
    public float movementSpeedMult;
    public float rotationSpeed = 10.0f;
    Vector3 movementVector;
    private Animator anim;

	void Start () {

        anim = GetComponentInChildren<Animator>();
        inputReader = InputReader.Instance;
	}
	
	void Update () {
        Move();
        Rotate();
	}

    void LateUpdate ()
    {
        Animate();
    }

    void Move()
    {
        movementVector = inputReader.MovementVector;
        if (Mathf.Abs( movementVector.x ) > Mathf.Abs( movementVector.z ))
        {
            movementSpeedMult = 3f;
        }
        else
        {
            movementSpeedMult = 1f;
        }

        transform.Translate(inputReader.MovementVector * movementSpeed * movementSpeedMult * Time.deltaTime);
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

    void Animate()
    {

        anim.SetFloat("VelX", Input.GetAxis("Horizontal"));
        anim.SetFloat("VelY", Input.GetAxis("Vertical"));
        
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }


    }
}
