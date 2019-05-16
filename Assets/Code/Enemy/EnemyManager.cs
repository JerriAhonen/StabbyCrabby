using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager Instance { get; private set; }

    private UIManager _ui;

    public int gameEnemyCount = 0;
    public int gameEnemiesKilled = 0;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start() {
        _ui = UIManager.Instance;
    }

    public void IncreaseKillCount(GameObject killedEnemy) {
        gameEnemiesKilled++;

        if (gameEnemiesKilled >= gameEnemyCount) {
            _ui.Win(killedEnemy);
        }
    }
}
