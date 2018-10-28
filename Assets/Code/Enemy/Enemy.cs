using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum EnemyType {
        Toast = 0,
        Pot = 1
    }

    public EnemyType enemyType;

    private Health _enemyHealth;

    void Start() {
        _enemyHealth = gameObject.AddComponent<Health>();

        int startingHealth = 0;

        switch (enemyType) {
            case EnemyType.Toast: {
                startingHealth = 10;
                break;
            }
            case EnemyType.Pot: {
                startingHealth = 50;
                break;
            }
        }

        _enemyHealth.SetHealth(startingHealth);
	}

    public void TakeDamage(int damage) {
        _enemyHealth.TakeDamage(damage);
    }
}
