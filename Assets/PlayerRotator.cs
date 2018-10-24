using UnityEngine;

public class PlayerRotator : MonoBehaviour {

    private InputReader _inputReader;

    private GameObject _playerMover;
    private Animator _anim;
    
    private float _rotationSpeed;
    
    private void Start()
    {
        _inputReader = InputReader.Instance;

        _playerMover = GameObject.FindGameObjectWithTag("PlayerMover");
        _anim = GetComponentInChildren<Animator>();
        _rotationSpeed = 10;
    }

    private void Update()
    {
        Move();
        Animate();
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

        transform.localRotation = Quaternion.Lerp(transform.rotation, rot, _rotationSpeed * Time.deltaTime);
    }

    void Animate()
    {
        _anim.SetFloat("VelX", _inputReader.MovementInputX);
        _anim.SetFloat("VelY", _inputReader.MovementInputZ);
    }
}
