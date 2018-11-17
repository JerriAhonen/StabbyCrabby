using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private GameObject _player;
    private Enemy _enemy;
    private ToastSpawner _toastSpawner;
    
    private bool _thrownBack = false;

    public bool ThrownBack {
        get {
            return _thrownBack;
        }
        set {
            _thrownBack = value;
        }
    }

    private RaycastHit _hitInfo;

    private float _height = 0.3f;

    [SerializeField] private LayerMask _ground;

    private bool _grounded = false;
    private bool _birthingTime = true;

    public bool BirthingTime {
        set {
            _birthingTime = value;
        }
    }

    private bool _disoriented = false;

    public Animator _animator;

    private void Awake() {
        _player = GameObject.Find("PLAYER");
        _enemy = GetComponent<Enemy>();

        if (_enemy.enemyType == Enemy.EnemyType.Toaster) {
            _toastSpawner = GetComponent<ToastSpawner>();
        }

        _animator = GetComponent<Animator>();
    }

    private void Start() {
        
    }
	
	private void Update() {
        //if (_thrownBack) {
        //    return;
        //}

        //float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);

        if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, _height, _ground)) {
            _grounded = true;
        } else {
            _grounded = false;
        }

        if (!_grounded) {
            transform.position += Physics.gravity * 3.5f * Time.deltaTime;
        }
    }

    //private void FixedUpdate() {
    //    if (_thrownBack) {
    //        ThrowEnemy();
    //    }
    //}

    private void LateUpdate() {
        switch(_enemy.enemyType) {
            case Enemy.EnemyType.Toaster: {
                if (_grounded && _birthingTime) {
                    _birthingTime = false;

                    _animator.SetTrigger("Birth");

                    _toastSpawner.Spawn = true;
                }
                break;
            }
        }

        
    }

    //private float _gravity;
    //private float _launchAngle;
    //private float _launchVelocity;
    //private Vector3 _horizontalTrajectory;
    //private Vector3 _verticalTrajectory;
    //private float _flyTime = 0;

    //public void GetThrown() {
    //    _gravity = 45f;
    //    _launchAngle = 35f;
    //    _launchVelocity = 45f;

    //    _horizontalTrajectory = -transform.forward * _launchVelocity * Mathf.Cos(_launchAngle * Mathf.Deg2Rad);

    //    _flyTime = 0;



    //    _thrownBack = true;
    //}

    //private void ThrowEnemy() {
    //    // Set flight's vertical trajectory. Gravity affects the vertical trajectory at every point in fly time.
    //    _verticalTrajectory.y = _launchVelocity * Mathf.Sin(_launchAngle * Mathf.Deg2Rad) - _gravity * _flyTime;
    //    // Time spent in flight always increases.
    //    _flyTime += Time.deltaTime;

    //    transform.position += _verticalTrajectory * Time.deltaTime;
    //    transform.position += _horizontalTrajectory * Time.deltaTime;
    //}

    //private void OnCollisionEnter(Collision collision) {
    //    if (_thrownBack && collision.gameObject.CompareTag("Ground")) {
    //        Debug.Log("Collided with ground");

    //        _thrownBack = false;
    //    }

    //    if (_thrownBack && collision.gameObject.CompareTag("Enemy")) {
    //        //should get bowled over
    //        Debug.Log("Collided with another enemy");
    //    }

    //}
}
