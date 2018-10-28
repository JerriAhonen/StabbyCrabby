using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private GameObject _player;

    private NavMeshAgent _enemy;
    private NavMeshObstacle _enemyObstacle;

    private void Awake() {
        _player = GameObject.Find("PLAYER");

        _enemy = GetComponent<NavMeshAgent>();
        _enemyObstacle = GetComponent<NavMeshObstacle>();
    }

    private void Start() {
        _enemy.baseOffset = 0f;

        _enemy.destination = _player.transform.position;
    }
	
	private void Update() {
        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);

        if (distanceToPlayer < _enemy.stoppingDistance) {
            _enemy.enabled = false;
            _enemyObstacle.enabled = true;
        } else {
            _enemy.enabled = true;
            _enemyObstacle.enabled = false;

            _enemy.destination = _player.transform.position;    // KEKSI JOKU TAPA ETTEI PÄIVITÄ JOKA FRAMELLA
        }
    }
}
