using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum EnemyType {
        Toaster = 0,
        Toast = 1,
        Pot = 2
    }

    public EnemyType enemyType;

    private Health _enemyHealth;
    private EnemyMovement _enemyMovement;

    private int _startingHealth;
    private int _damage;
    //private int _points;

    public int Points { private set; get; }

    void Start() {
        _enemyHealth = gameObject.AddComponent<Health>();
        _enemyMovement = GetComponent<EnemyMovement>();

        switch (enemyType) {
            case EnemyType.Toaster: {
                _startingHealth = 1;
                _damage = 0;
                Points = 1000;
                break;
            }
            case EnemyType.Toast: {
                _startingHealth = 1;
                _damage = 10;
                Points = 100;

                transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 180f), Vector3.up);
                _enemyMovement.GetThrown(transform.forward);
                break;
            }
            case EnemyType.Pot: {
                _startingHealth = 1;
                _damage = 20;
                Points = 100;
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
            if (collision.gameObject.GetComponent<Health>() != null) {
                Debug.Log("Collided with player health component object");
                collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
            }
        }
    }
}
