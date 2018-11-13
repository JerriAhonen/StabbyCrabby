using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private GameObject _player;
    
    private bool _thrownBack = false;

    public bool ThrownBack {
        get {
            return _thrownBack;
        }
        set {
            _thrownBack = value;
        }
    }

    private bool _disoriented = false;

    private void Awake() {
        _player = GameObject.Find("PLAYER");
        
    }

    private void Start() {
        
    }
	
	private void Update() {
        //if (_thrownBack) {
        //    return;
        //}

        //float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);

        
    }

    //private void FixedUpdate() {
    //    if (_thrownBack) {
    //        ThrowEnemy();
    //    }
    //}

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
