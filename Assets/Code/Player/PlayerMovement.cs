using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private InputReader _inputReader;

    [Range(0.1f, 1.0f)]
    public float groundCollisionCheckDistance;
    public Vector3 groundCollisionOffset;

    public float movementSpeed = 5.0f;
    public float movementSpeedMult;
    public float rotationSpeed = 10.0f;
    private Quaternion _groundRotation;

    public bool _isGrounded;// Change to private
    private bool _isMoving;

    private float _jumpForce = 6.0f;
    private float _gravity = 1.0f;
    public float _verticalVelocity; // Change to private
    public float _minimumVerticalVelocity;  // Negative number

    // Animation variables

    private Animator _anim;
    private Rigidbody _rb;

	void Start () {
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _inputReader = InputReader.Instance;

        GetColliderInfo();
    }
	
	void Update () {
        Move();
        Rotate();
        IsGrounded();
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

        RaycastHit hit;
        Vector3 rayDrawPoint = transform.position - groundCollisionOffset;

        if (Physics.Raycast(rayDrawPoint, Vector3.down, out hit, groundCollisionCheckDistance))
        {
            _isGrounded = true;
            _groundRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            movementVector.y = 0.0f;

            Debug.DrawRay(rayDrawPoint, Vector3.down * groundCollisionCheckDistance, Color.cyan);
        }
        else
        {
            _isGrounded = false;
            movementVector.y -= (_gravity * Time.deltaTime);

            Debug.DrawRay(rayDrawPoint, Vector3.down * groundCollisionCheckDistance, Color.red);
        }

        /*
        if (_isGrounded)
        {
            _verticalVelocity = -0.1f;
        }
        else
        {
            _verticalVelocity -= (_gravity * Time.deltaTime);
        }
        */

        //movementVector.y = _verticalVelocity;

        transform.Translate(movementVector * movementSpeed * movementSpeedMult * Time.deltaTime);
    }

    void Rotate()
    {
        if (_isMoving)
        {
            // Rotate the player with the camera
            Quaternion rot = _inputReader.LocalRotation;
            //rot.x = 0;
            //rot.z = 0;

            rot.x = _groundRotation.x;
            rot.z = _groundRotation.z;

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

    void IsGrounded()
    {
        
    }

    Collider m_Collider;
    Vector3 m_Center;
    Vector3 m_Size, m_Min, m_Max;

    void GetColliderInfo()
    {
        //Fetch the Collider from the GameObject
        m_Collider = GetComponent<Collider>();
        //Fetch the center of the Collider volume
        m_Center = m_Collider.bounds.center;
        //Fetch the size of the Collider volume
        m_Size = m_Collider.bounds.size;
        //Fetch the minimum and maximum bounds of the Collider volume
        m_Min = m_Collider.bounds.min;
        m_Max = m_Collider.bounds.max;
        //Output this data into the console
        OutputData();
    }

    void OutputData()
    {
        //Output to the console the center and size of the Collider volume
        Debug.Log("Collider Center : " + m_Center);
        Debug.Log("Collider Size : " + m_Size);
        Debug.Log("Collider bound Minimum : " + m_Min);
        Debug.Log("Collider bound Maximum : " + m_Max);
    }


}

