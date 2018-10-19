using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private InputReader _inputReader;

    [Range(0.1f, 1.0f)]
    public float groundCollisionCheckDistance;

    public float movementSpeed = 5.0f;
    public float movementSpeedMult;
    public float rotationSpeed = 10.0f;

    public bool _isGrounded;// Change to private
    private bool _isMoving;

    private float _jumpForce = 6.0f;
    private float _gravity = 12.0f;
    public float _verticalVelocity; // Change to private
    public float _minimumVerticalVelocity;  // Negative number

    // Animation variables
    
    private Animator _anim;
    private Rigidbody _rb;

	void Start () {
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _inputReader = InputReader.Instance;
	}
	
	void Update () {
        Move();
        Rotate();
        Jump();
    }

    void LateUpdate ()
    {
        Animate();
    }

    void Move()
    {
        Vector3 movementVector;
        movementVector.x = _inputReader.MovementInputX;
        movementVector.y = 0;
        movementVector.z = _inputReader.MovementInputZ;

        // CLAMPING TO 1

        if (movementVector != Vector3.zero)
        {
            movementVector = Vector3.ClampMagnitude(movementVector, 1);
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        
        // SIDEWAYS MOVEMENT MULTIPLIER

        if (Mathf.Abs( movementVector.x ) > Mathf.Abs( movementVector.z ))
        {
            movementSpeedMult = 3f;
        }
        else
        {
            movementSpeedMult = 2f;
        }
        
        transform.Translate(movementVector * movementSpeed * movementSpeedMult * Time.deltaTime);
    }

    void Jump()
    {
        if (_inputReader.Jump)
            _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
    }

    void Rotate()
    {
        if (_isMoving)
        {
            // Rotate the player with the camera
            Quaternion rot = _inputReader.LocalRotation;
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
        _anim.SetFloat("VelX", _inputReader.MovementInputX);
        _anim.SetFloat("VelY", _inputReader.MovementInputZ);
    }
    
    
}

