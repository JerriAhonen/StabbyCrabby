using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

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
        GameObject enemy = Instantiate(_enemy);

        enemy.SetActive(false);
        
        _enemyPool.Add(enemy);

        return enemy;
    }

    private void ActivateEnemy(int number) {

    }

    public void SpawnWave() {
        ActivateEnemy(8);
    }
}
