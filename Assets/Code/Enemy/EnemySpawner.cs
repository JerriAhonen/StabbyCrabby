using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public bool spawn;

    private int enemyType;

    [SerializeField] private int _poolSize = 500;

    [SerializeField] private int[] _enemiesInWave;

    [SerializeField] private GameObject[] _enemy;

    private List<GameObject> _enemyPool;

    private int _waveCount = -1;
    private int _waveStartIndex = 0;

    private void Awake() {
        _enemyPool = new List<GameObject>(_poolSize);

        for (int wave = 0; wave < _enemy.Length; wave++) {
            for(int numberOfEnemies = 0; numberOfEnemies < _enemiesInWave[wave]; numberOfEnemies++) {
                AddEnemy(_enemy[wave], wave);
            }
        }        
    }

    private GameObject AddEnemy(GameObject enemyToAdd, int waveNumber) {
        GameObject enemy = Instantiate(enemyToAdd, transform.position, Quaternion.identity);

        enemy.GetComponent<Enemy>().enemyType = (Enemy.EnemyType) waveNumber;

        enemy.SetActive(false);
        
        _enemyPool.Add(enemy);

        return enemy;
    }

    private void ActivateWave(int wave) {
        if (wave == 0) {
            _waveStartIndex = 0;
        } else {
            _waveStartIndex = _waveStartIndex + _enemiesInWave[wave - 1];
        }
        
        for (int i = _waveStartIndex; i < (_waveStartIndex + _enemiesInWave[wave]); i++) {
                _enemyPool[i].SetActive(true);
        }
    }

    private void Update() {
        if (spawn) {
            spawn = false;

            ActivateWave(++_waveCount);
        }
    }
}
