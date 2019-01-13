using UnityEngine;

public class PlayerRotator : MonoBehaviour {

    private InputReader _inputReader;

    private GameObject _playerMover;
    private PlayerMovement _playerMovement;
    private Animator _anim;

    private Vector3 _forward;
    [SerializeField] private Vector3 _groundNormal;
    
    private float _rotationSpeed;

    public bool drawDebugLines;
    
    private void Start()
    {
        _inputReader = InputReader.Instance;

        _playerMover = GameObject.FindGameObjectWithTag("PlayerMover");
        _playerMovement = _playerMover.GetComponent<PlayerMovement>();
        _anim = GetComponentInChildren<Animator>();
        _rotationSpeed = 10;
    }

    private void Update()
    {
        Move();
        RotateToGround();
        Animate();

        DrawDebugLines();
    }

    void Move()
    {
        transform.position = _playerMover.transform.position;
    }

    public void RotateModel()
    {
        Quaternion rot = _inputReader.LocalRotation;
        
        rot.x = 0;
        rot.z = 0;
        
        Quaternion groundRot = Quaternion.LookRotation(rot * Vector3.forward, _playerMovement.HitInfo.normal);
        
        transform.localRotation = Quaternion.Lerp(transform.rotation, groundRot, _rotationSpeed * Time.deltaTime);
    }

    void RotateToGround() {}

    void Animate()
    {
        _anim.SetFloat("VelX", _inputReader.MovementInputX);
        _anim.SetFloat("VelY", _inputReader.MovementInputZ);
    }

    void DrawDebugLines()
    {
        _groundNormal = _playerMovement.HitInfo.normal;
        Debug.DrawLine(transform.position, transform.position + _playerMovement.HitInfo.normal, Color.red);
    }
}
