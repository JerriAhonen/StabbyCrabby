using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager Instance { get; private set; }

    private GameManager gameManager;

    public int gameEnemyCount = 0;
    public int gameEnemiesKilled = 0;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start() {
        gameManager = GameManager.Instance;
    }

    public void IncreaseKillCount(string killedEnemy) {
        gameEnemiesKilled++;

        if (gameEnemiesKilled >= gameEnemyCount) {
            gameManager.End(true, killedEnemy);
        }
    }
}
