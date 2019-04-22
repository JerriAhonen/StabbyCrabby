using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private GameObject _player;
    private Enemy _enemy;
    private ToastSpawner _toastSpawner;
    
    private bool _isThrown = false;

    public bool ThrownBack {
        get {
            return _isThrown;
        }
        set {
            _isThrown = value;
        }
    }

    //private RaycastHit _hitInfo;

    //private float _height = 0.2f;

    private float _groundHeight = 0f;
    private float _heightPadding = -0.05f;

    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _enemies;

    private bool _isGrounded = false;
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

    private bool _isDropped;
    private float _speedModifier = 3f;

    public Vector3 _playerPosition;
    public Vector3 _targetLocation;
    public float _maxDistance;
    public float _maxAttackDistance;
    public bool _attack;

    Vector3 _currentVelocity;

    private void Awake() {
        _player = GameObject.Find("PLAYER");
        _enemy = GetComponent<Enemy>();

        if (_enemy.enemyType == Enemy.EnemyType.Toaster) {
            _toastSpawner = GetComponent<ToastSpawner>();

            _isDropped = true;
        }

        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _enemy.Init();

        _playerPosition = _player.transform.position;

        _targetLocation = new Vector3(_player.transform.position.x, _groundHeight, _player.transform.position.z);

        _spawnWaitTime = 1.6f;
        _maxDistance = 2f;
        _maxAttackDistance = _enemy.AttackDistance;
    }

    public float distance;
    public float attackDistance;

	private void Update() {
        distance = Vector3.Distance(_player.transform.position, _targetLocation);
        attackDistance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance < _maxDistance) {
            _targetLocation = new Vector3(_player.transform.position.x, _groundHeight, _player.transform.position.z);
        }

        if (attackDistance < _maxAttackDistance) {
            _animator.SetTrigger("Attack");
        } else if (_isGrounded && _targetLocation != Vector3.zero) {
            if (_enemy.enemyType == Enemy.EnemyType.Toast) {

                Wander(_targetLocation);

                _animator.SetTrigger("Walk");
            }
        } else {
            if (_enemy.enemyType == Enemy.EnemyType.Toast) {
                _animator.SetTrigger("Idle");
            }
        }

        Rotate(_targetLocation);

        if (_enemy.enemyType == Enemy.EnemyType.Toaster) {
            if (_isGrounded && _birthingTime) {
                _birthingTime = false;

                _animator.SetTrigger("Birth");

                _toastSpawner.Invoke("Spawn", _spawnWaitTime);
            }
        }
    }

    private void FixedUpdate() {
        //if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, _height + 0.01f, _ground)) {
        //    //if (Vector3.Distance(transform.position, _hitInfo.point) < _height)
        //    //{
        //    //    transform.position = Vector3.Lerp(transform.position,
        //    //                                    transform.position + Vector3.up * _height,
        //    //                                    5 * Time.deltaTime);
        //    //}

        //    _grounded = true;
        //    Debug.Log("grounded " + _grounded);

        //    if (_thrownBack) {
        //        _thrownBack = false;
        //    }

        //    if (_dropped) {
        //        _dropped = false;
        //    }
        //} else {
        //    _grounded = false;
        //}

        //if (!_grounded) {
        //    if(_dropped) {
        //        _speedModifier = 3f;
        //    } else {
        //        _speedModifier = 1f;
        //    }

        //    if (transform.position.y > -0.5f) {
        //        transform.position += Physics.gravity * _speedModifier * Time.deltaTime;
        //    }
        //}

        if (transform.position.y > _groundHeight - _heightPadding) // _heightPadding subtracted for a small bounce when landing
        {
            transform.position += Physics.gravity * _speedModifier * Time.deltaTime;

            _isGrounded = false;
        } else if (transform.position.y < _groundHeight + _heightPadding)
        {
            transform.position = Vector3.Lerp(transform.position,
                                            transform.position + Vector3.up,
                                            5 * Time.deltaTime);

            _isGrounded = false;

            if (_isThrown)
            {
                _isThrown = false;
            }
        } else {
            _isGrounded = true;

            if (_isDropped)
            {
                _isDropped = false;
            }
            
            if (_isThrown)
            {
                _isThrown = false;
            }
        }

        if (_isThrown) {
            ThrowEnemy();
        }
    }

    private void LateUpdate() {
        
    }

    private void Rotate(Vector3 target) {
        if ((target - transform.position) != Vector3.zero) {
            Quaternion newRotation = Quaternion.LookRotation(target - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5f);
        }
    }

    private void Wander(Vector3 target) {
        // Get current agent data for the agent.
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Get current controller data for the agent.
        Vector3 separation = Vector3.zero;
        Vector3 alignment = transform.forward;
        Vector3 cohesion = transform.position;

        Collider[] agents = Physics.OverlapSphere(position,
            _enemy.DistanceBetweenAgents, _enemies, QueryTriggerInteraction.Collide);

        foreach(var agent in agents) {
            // If the agent is this agent, it already has the current controller data.
            if (agent.gameObject == this.gameObject) {
                continue;
            }

            // Update the other agents in the flock.
            separation += CalculateSeparationVector(agent.transform);
            alignment += agent.transform.forward;
            cohesion += agent.transform.position;
        }

        float average = 1.0f / agents.Length;

        // Steer to align to the average heading of neighbors
        alignment *= average;
        // Steer towards the average position of neighbors = finds middle point of all neighbors and tries to move there
        cohesion *= average;
        cohesion = (cohesion - position).normalized; // offset from own position
        //cohesion = Vector3.SmoothDamp(cohesion, target, ref _currentVelocity, 0.5f);

        // THIS MAKES IT RAINNNNNNNNNNNNN :D
        //Vector3 newDirection = separation + alignment + cohesion;

        Vector3 flockMove = new Vector3(separation.x * 7 + alignment.x + cohesion.x, 0, separation.z * 7 + alignment.z + cohesion.z);

        // THIS MAKES THEM RUN AWAY AND CIRCLE THE LEVEL :D
        //transform.position = Vector3.MoveTowards(transform.position + newDirection, target, _enemy.Speed * Time.deltaTime);

        //transform.forward = newDirection;

        transform.position += flockMove * 0.5f * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, _enemy.Speed * Time.deltaTime);
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
        _launchVelocity = 65f;

        _horizontalTrajectory = direction * _launchVelocity * Mathf.Cos(_launchAngle * Mathf.Deg2Rad);

        _flyTime = 0;

        _isThrown = true;
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

    // Calculates the separation vector between an agent in the flock and the current agent.
    private Vector3 CalculateSeparationVector(Transform target) {
        // Steer to avoid hitting neighbors
        Vector3 separationVector = transform.position - target.transform.position;

        // The length of the vector.
        float distance = separationVector.magnitude;

        float scaler = Mathf.Clamp01(1.0f - distance / _enemy.DistanceBetweenAgents);

        return separationVector * (scaler / distance);
    }
}
