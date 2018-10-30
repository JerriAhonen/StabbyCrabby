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
    private EnemyMovement _enemyMovement;

    private int _startingHealth;
    private int _damage;

    void Start() {
        _enemyHealth = gameObject.AddComponent<Health>();
        _enemyMovement = gameObject.AddComponent<EnemyMovement>();

        switch (enemyType) {
            case EnemyType.Toast: {
                _startingHealth = 50;
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
        } else {
            _enemyMovement.GetThrown();
        }

        return dead;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (collision.gameObject.GetComponent<Health>() != null) {
                Debug.Log("Collided with player health component object");
                collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
            }
        }
    }
}
