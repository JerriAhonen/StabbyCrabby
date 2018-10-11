using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public bool spawn;

    public int enemyType;

    [SerializeField] private int _poolSize = 500;

    [SerializeField] private int[] _enemiesInWave;

    [SerializeField] private GameObject[] _enemy;

    private List<GameObject> _enemyPool;

    private int _waveCount = -1;

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
        int lastIndex;

        if (wave == 0) {
            lastIndex = wave;
        } else {
            lastIndex = _enemyPool.LastIndexOf(_enemy[wave]);
        }
        
        Debug.Log("lastIndex was " + lastIndex);

        for (int i = lastIndex; i < (lastIndex + _enemiesInWave[wave]); i++) {
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
