using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastSpawner : MonoBehaviour {

    [SerializeField] private int _poolSize;

    [SerializeField] private int[] _enemiesInWave;

    [SerializeField] private GameObject[] _enemyTypes;

    [SerializeField] private float[] _spawnIntervals;

    [SerializeField] private Transform[] _spawnLocations;

    private List<GameObject> _enemyPool;

    private EnemyMovement _enemy;

    private int _waveCount = -1;
    private int _waveStartIndex = 0;

    private bool _spawn;

    private float _spawnTimer;
    private float _timeToNextWave;

    [SerializeField, HideInInspector]
    private EnemyManager _enemyManager;

    private void Awake() {
        _enemyManager = EnemyManager.Instance;

        _enemyPool = new List<GameObject>(_poolSize);

        for (int wave = 0; wave < _enemyTypes.Length; wave++) {
            for (int numberOfEnemies = 0; numberOfEnemies < _enemiesInWave[wave]; numberOfEnemies++) {
                AddEnemy(_enemyTypes[wave], wave);
                _enemyManager.gameEnemyCount++;
            }
        }
    }

    private void Start() {
        _spawnTimer = 0;

        _timeToNextWave = 5f;

        _enemy = GetComponent<EnemyMovement>();
    }

    private GameObject AddEnemy(GameObject enemyToAdd, int wave) {
        GameObject enemy = Instantiate(enemyToAdd, transform.position, Quaternion.identity);

        var enemyType = enemy.GetComponent<Enemy>().enemyType;

        enemyType = (Enemy.EnemyType) wave;

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

        StartCoroutine(Activate(wave));
    }

    private void Update() {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer > _timeToNextWave) {
            _enemy.SpawnWaitTime = 2f;
            _enemy.BirthingTime = true;

            _spawnTimer = 0f;
        }

        if (_spawn && (_waveCount < _enemiesInWave.Length - 1)) {
            _spawn = false;
            
            ActivateWave(++_waveCount);
        }
    }

    public void Spawn() {
        _spawn = true;
    }

    private IEnumerator Activate(int wave) {
        for (int i = _waveStartIndex; i < (_waveStartIndex + _enemiesInWave[wave]); i++) {
            _enemyPool[i].transform.position = 
                new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);

            _enemyPool[i].SetActive(true);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
