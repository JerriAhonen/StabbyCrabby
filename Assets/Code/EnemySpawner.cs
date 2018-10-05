using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public bool spawn;

    [SerializeField] private int _poolSize;

    [SerializeField] private GameObject _enemy;

    private List<GameObject> _enemyPool;

    private void Awake() {
        _enemyPool = new List<GameObject>(_poolSize);

        for (int i = 0; i < _poolSize; i++) {
            AddEnemy();
        }
    }

    private GameObject AddEnemy() {
        GameObject enemy = Instantiate(_enemy, transform.position, Quaternion.identity);

        enemy.SetActive(false);
        
        _enemyPool.Add(enemy);

        return enemy;
    }

    private void ActivateEnemy(int number) {
        for (int i = 0; i < number; i++) {
            _enemyPool[i].SetActive(true);
        }
    }

    private void Update() {
        if (spawn) {
            spawn = false;

            ActivateEnemy(_poolSize);
        }
    }
}
