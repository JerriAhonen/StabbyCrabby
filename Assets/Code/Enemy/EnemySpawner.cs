using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private bool _spawn;

    private int enemyType;

    [SerializeField] private int _poolSize;

    [SerializeField] private int[] _enemiesInWave;

    [SerializeField] private GameObject[] _enemyTypes;

    [SerializeField] private float[] _spawnIntervals;

    [SerializeField] private Transform[] _spawnLocations;

    private List<GameObject> _enemyPool;

    private int _waveCount = -1;
    private int _waveStartIndex = 0;

    private List<Transform> _toasters = new List<Transform>();

    private float _spawnTimer;
    private bool _waveKilled = false;
    private float _timeToNextWave;

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

        _timeToNextWave = 10f;
    }

    private GameObject AddEnemy(GameObject enemyToAdd, int wave) {
        GameObject enemy = Instantiate(enemyToAdd, _spawnLocations[wave].position, Quaternion.identity);

        var enemyType = enemy.GetComponent<Enemy>().enemyType;

        enemyType = (Enemy.EnemyType) wave;

        if (enemyType == Enemy.EnemyType.Toaster) {
            _toasters.Add(enemy.transform);

            List<Transform> spawnLocations = new List<Transform>();

            // Fill the list of toaster spawn locations.
            foreach (Transform child in enemy.transform) {
                if (child.CompareTag("SpawnLocation")) {
                    spawnLocations.Add(child);
                }
            }

            // Give the new toaster its initial spawn location.
            enemy.transform.position = spawnLocations[0].position;

            // If any of the other toasters has the same spawn location, try for a new one.
            foreach (Transform toaster in _toasters) {
                
                //while (toaster.position == enemy.transform.position) {
                //    enemy.transform.position = spawnLocations[Random.Range(0, 4)].position;
                //}
            }
        }

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

        _timeToNextWave = _spawnIntervals[wave];

        for (int i = _waveStartIndex; i < (_waveStartIndex + _enemiesInWave[wave]); i++) {
                _enemyPool[i].SetActive(true);
        }
    }

    private void Update() {
        _spawnTimer += Time.deltaTime;

        if ((_spawnTimer > _timeToNextWave || _waveKilled) && (_waveCount < _enemiesInWave.Length - 1)) {
            _spawn = true;

            _spawnTimer = 0f;
        }

        if (_spawn) {
            _spawn = false;
            
            ActivateWave(++_waveCount);
        }
    }
}
