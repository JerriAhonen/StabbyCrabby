using UnityEngine;

public class InputReader : MonoBehaviour {

    public static InputReader Instance { get; set; }

    // CAMERA SETTINGS AND INPUT

    public float clampAngleUp = 50.0f;
    public float clampAngleDown = 50.0f;
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
    public float MovementInputX { get { return _movementInputX; } }
    private float _movementInputZ;
    public float MovementInputZ { get { return _movementInputZ; } }

    private bool _jump;
    public bool Jump { get { return _jump; } }

    // COMBAT INPUT

    private bool _stab, _aim, _shoot;
    public bool Stab { get { return _stab; } }
    public bool Shoot { get { return _shoot; } }

    // OTHER

    private bool _esc;
    public bool Esc { get { return _esc; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    
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

        _rotX = Mathf.Clamp(_rotX, -clampAngleDown, clampAngleUp);

        _localRotation = Quaternion.Euler(_rotX, _rotY, 0);

        #endregion

        #region MovementInput

        _movementInputX = Input.GetAxis("Horizontal");
        _movementInputZ = Input.GetAxis("Vertical");
        _jump = Input.GetButtonDown("Jump");
        
        #endregion

        #region CombatInput

        if (Time.timeScale != 0)
        {
            _stab = Input.GetButtonDown("Fire1");
            _shoot = Input.GetButtonDown("Fire2");
        }

        #endregion

        //_esc = Input.GetButtonDown("Cancel");
        _esc = Input.GetKeyDown(KeyCode.P);
    }
}
