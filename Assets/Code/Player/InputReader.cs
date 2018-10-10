using UnityEngine;

public class InputReader : MonoBehaviour {

    public static InputReader Instance { get; set; }

    // CAMERA SETTINGS AND INPUT

    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;

    private float _rightStickInputX;
    private float _rightStickInputZ;

    private float _mouseX;
    private float _mouseY;

    private float _finalMovementInputX;
    private float _finalMovementInputZ;
    private float _rotX;
    private float _rotY;
    
    private Quaternion _localRotation;
    public Quaternion LocalRotation { get { return _localRotation; } }

    // PLAYER MOVEMENT INPUT

    private float _movementInputX;
    private float _movementInputZ;
    private Vector3 _movementVector;
    public Vector3 MovementVector { get { return _movementVector; } }

    private bool _isMoving;
    public bool IsMoving { get { return _isMoving; } }

    // COMBAT INPUT

    private bool _stab, _aim, _shoot;
    public bool Stab { get { return _stab; } }

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        #region CameraInput

        _rightStickInputX = Input.GetAxis("RightStickHorizontal");
        _rightStickInputZ = Input.GetAxis("RightStickVertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _finalMovementInputX = _rightStickInputX + _mouseX;
        _finalMovementInputZ = _rightStickInputZ + _mouseY;

        _rotY += _finalMovementInputX * inputSensitivity * Time.deltaTime;
        _rotX += _finalMovementInputZ * inputSensitivity * Time.deltaTime;

        _rotX = Mathf.Clamp(_rotX, -clampAngle, clampAngle);

        _localRotation = Quaternion.Euler(_rotX, _rotY, 0);

        #endregion

        #region MovementInput

        _movementInputX = Input.GetAxis("Horizontal");
        _movementInputZ = Input.GetAxis("Vertical");

        _movementVector.x = _movementInputX;
        _movementVector.y = 0;
        _movementVector.z = _movementInputZ;

        if (_movementVector != Vector3.zero)
        {
            _movementVector = Vector3.ClampMagnitude(_movementVector, 1);
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        #endregion

        #region CombatInput

        _stab = Input.GetButtonDown("Fire1");

        #endregion
    }
}
