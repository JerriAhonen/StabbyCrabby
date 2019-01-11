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

    private float _spawnWaitTime;

    public float SpawnWaitTime {
        set {
            _spawnWaitTime = value;
        }
    }

    private Animator _animator;

    private bool _dropped;
    private float _speedModifier;

    private void Awake() {
        _player = GameObject.Find("PLAYER");
        _enemy = GetComponent<Enemy>();

        if (_enemy.enemyType == Enemy.EnemyType.Toaster) {
            _toastSpawner = GetComponent<ToastSpawner>();

            _dropped = true;
        }

        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _spawnWaitTime = 1.6f;
    }
	
	private void Update() {
        //if (_thrownBack) {
        //    return;
        //}

        //float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
    }

    private void FixedUpdate() {
        if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, _height, _ground)) {
            _grounded = true;

            if (_thrownBack) {
                _thrownBack = false;
            }

            if (_dropped) {
                _dropped = false;
            }
        } else {
            _grounded = false;
        }

        if (!_grounded) {
            if(_dropped) {
                _speedModifier = 3f;
            } else {
                _speedModifier = 1f;
            }

            transform.position += Physics.gravity * _speedModifier * Time.deltaTime;
        }

        if (_thrownBack) {
            ThrowEnemy();
        }
    }

    private void LateUpdate() {
        switch(_enemy.enemyType) {
            case Enemy.EnemyType.Toaster: {
                if (_grounded && _birthingTime) {
                    _birthingTime = false;

                    _animator.SetTrigger("Birth");

                    _toastSpawner.Invoke("Spawn", _spawnWaitTime);
                }
                break;
            }
        }
    }

    private float _gravity;
    private float _launchAngle;
    private float _launchVelocity;
    private Vector3 _horizontalTrajectory;
    private Vector3 _verticalTrajectory;
    private float _flyTime = 0;

    public void GetThrown(Vector3 direction) {
        _gravity = 55f;
        _launchAngle = 80f;
        _launchVelocity = 50f;

        _horizontalTrajectory = direction * _launchVelocity * Mathf.Cos(_launchAngle * Mathf.Deg2Rad);

        _flyTime = 0;

        _thrownBack = true;
    }

    private void ThrowEnemy() {
        // Set flight's vertical trajectory. Gravity affects the vertical trajectory at every point in fly time.
        _verticalTrajectory.y = _launchVelocity * Mathf.Sin(_launchAngle * Mathf.Deg2Rad) - _gravity * _flyTime;
        // Time spent in flight always increases.
        _flyTime += Time.deltaTime;

        transform.position += _verticalTrajectory * Time.deltaTime;
        transform.position += _horizontalTrajectory * Time.deltaTime;

        // Softens fall but makes it look unnatural
        //if (transform.position.y < 0.2f) {
        //    _thrownBack = false;
        //}

        // Makes the object bounce :D
        //if (transform.position.y < 0f) {
        //    _flyTime = 0f;
        //}
    }
}
