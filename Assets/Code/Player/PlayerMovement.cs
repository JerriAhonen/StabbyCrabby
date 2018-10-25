using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private InputReader _inputReader;  // Visible to this class and Classes derived from this class.
    private PlayerRotator _pr;
    public GameObject player;

    // MOVEMENT //

    public float velocity;
    public float movementSpeedMult;
    public float rotationSpeed = 100.0f;
    private Vector3 movementVector;
    private Vector3 forward;
    public Vector3 ForwardVector { get { return forward; } }
    private bool grounded;
    public float inputPadding = 0.1f;
    
    // ROTATION //

    private Quaternion targetRotation;
    private float angle;
    private float groundAngle;
    private Transform cam;

    // GROUND CHECKS //

    public LayerMask ground;
    public float maxGroundAngle = 120f;

    private RaycastHit _hitInfo;
    public RaycastHit HitInfo { get { return _hitInfo; } }

    public float height = 0.5f;
    public float heightPadding = 0.05f;

    // DEBUGGING //

    public bool debugLines;
    
    private void Start()
    {
        _inputReader = InputReader.Instance;
        
        _pr = player.GetComponent<PlayerRotator>();
        cam = Camera.main.transform;
    }

    /// <summary>
    /// Only do Move() and Rotate() if we have some Movement Input.
    /// </summary>
    private void Update()
    {
        if (_inputReader == null)
            _inputReader = InputReader.Instance;

        GetInput();
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();
        DrawDebugLines();

        if (Mathf.Abs(_inputReader.MovementInputX) < inputPadding 
            && Mathf.Abs(_inputReader.MovementInputZ) < inputPadding) return;

        Move();
        Rotate();
        _pr.RotateModel();
    }

    /// <summary>
    /// Gets the input form InputReader, which is a Component of the Player.
    /// </summary>
    void GetInput()
    {
        movementVector.x = _inputReader.MovementInputX;
        movementVector.z = _inputReader.MovementInputZ;
    }

    /// <summary>
    /// 1. Check if ground angle isn't too steep.
    /// </summary>
    void Move()
    {
        if (groundAngle >= maxGroundAngle) return;

        transform.position += forward * velocity * Time.deltaTime;
    }

    /// <summary>
    /// 1. Get the rotation Quaternion from InputReader's CameraInput.
    /// 2. Set the X and Z axis to 0 so we only follow the camera on the Y axis.
    /// 3. Lerp the rotation for smoother movement.
    /// </summary>
    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = targetRotation;
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(movementVector.x, movementVector.z);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    /// <summary>
    /// Calculates the forward Vector3 according to the ground angle.
    /// </summary>
    void CalculateForward()
    {
        if (!grounded)
        {
            forward = transform.forward;
            return;
        }
        
        forward = Vector3.Cross(transform.right, _hitInfo.normal);
    }
    
    /// <summary>
    /// 1. If we are not grounded, set the ground angle to 90. MAY WANT TO CHANGE THIS
    /// 2. GroundAngle is the angle between the the player's forward vector and the floor's normal.
    /// </summary>
    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(_hitInfo.normal, transform.forward);
    }

    /// <summary>
    /// 1. Raycast straight down from the middle of the Character for "height" + "heightPadding" distance.
    /// 1.1. NOTICE that height needs to be half of the character's colliders height!
    /// 2. If we are grounded, correct our position with lerp if we sink in the ground.
    /// </summary>
    void CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, height + heightPadding, ground))
        {
            if (Vector3.Distance(transform.position, _hitInfo.point) < height)
            {
                transform.position = Vector3.Lerp(transform.position,
                                                transform.position + Vector3.up * height,
                                                5 * Time.deltaTime);
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    /// <summary>
    /// Simple Gravity using Unity's built in Physics.
    /// </summary>
    void ApplyGravity()
    {
        if (!grounded)
        {
            transform.position += Physics.gravity * Time.deltaTime;
        }
    }
    
    /// <summary>
    /// Debug lines for visualisation of our vectors.
    /// </summary>
    void DrawDebugLines()
    {
        if (!debugLines) return;

        Debug.DrawLine(transform.position, transform.position + forward * height * 2, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * (height + heightPadding), Color.green);
    }
}
