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

    private int _startingHealth;
    private int _damage;

    void Start() {
        _enemyHealth = gameObject.AddComponent<Health>();

        switch (enemyType) {
            case EnemyType.Toast: {
                _startingHealth = 10;
                _damage = 10;
                break;
            }
            case EnemyType.Pot: {
                _startingHealth = 50;
                _damage = 20;
                break;
            }
        }

        _enemyHealth.SetHealth(_startingHealth);
	}

    public bool TakeDamage(int damage) {
        bool dead = _enemyHealth.TakeDamage(damage);

        if (dead) {
            Destroy(gameObject);
        }

        return dead;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
