﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private bool _spawn;

    private int enemyType;

    [SerializeField] private int _poolSize;

    [SerializeField] private int[] _enemiesInWave;

    [SerializeField] private GameObject[] _enemyTypes;

    [SerializeField] private Transform[] _spawnLocations;

    private List<GameObject> _enemyPool;

    private int _waveCount = -1;
    private int _waveStartIndex = 0;

    private float _spawnTimer;
    private bool _waveKilled = false;

    private void Awake() {
        _enemyPool = new List<GameObject>(_poolSize);

        for (int wave = 0; wave < _enemyTypes.Length; wave++) {
            for (int numberOfEnemies = 0; numberOfEnemies < _enemiesInWave[wave]; numberOfEnemies++) {
                AddEnemy(_enemyTypes[wave], wave);
            }
        }        
    }

    private void Start() {
        _spawnTimer = 0;
    }

    private GameObject AddEnemy(GameObject enemyToAdd, int wave) {
        GameObject enemy = Instantiate(enemyToAdd, _spawnLocations[wave].position, Quaternion.identity);

        enemy.GetComponent<Enemy>().enemyType = (Enemy.EnemyType) wave;

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
        _spawnTimer += Time.deltaTime;

        // hard coded time probably to be changed to a float variable that changes by wave?
        if (_spawnTimer > 10f || _waveKilled) {
            _spawn = true;

            _spawnTimer = 0f;
        }

        if (_spawn) {
            _spawn = false;
            
            ActivateWave(++_waveCount);
        }
    }
}
