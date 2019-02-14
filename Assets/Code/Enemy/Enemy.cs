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

    public float Speed { private set; get; }

    //private int _points;

    public int Points { private set; get; }

    private bool _isDead;

    public bool IsDead {
        get {
            return _isDead;
        }
        private set {
            _isDead = value;
        }
    }

    private Sliceable _sliceable;

    void Start() {
        _enemyHealth = GetComponent<Health>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _sliceable = GetComponent<Sliceable>();

        // Slice creates new gameObjects that shouldn't go through the rest of Start
        if (_sliceable != null) {
            if (_sliceable.Sliced) {
                return;
            }
        }

        switch (enemyType) {
            case EnemyType.Toaster: {
                _startingHealth = 1;
                _damage = 0;
                Speed = 1f;
                Points = 1000;
                break;
            }
            case EnemyType.Toast: {
                _startingHealth = 1;
                _damage = 10;
                Speed = 5f;
                Points = 100;

                transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 180f), Vector3.up);
                _enemyMovement.GetThrown(transform.forward);
                break;
            }
            case EnemyType.Pot: {
                _startingHealth = 1;
                _damage = 20;
                Speed = 5f;
                Points = 100;
                break;
            }
        }

        _enemyHealth.SetHealth(_startingHealth);
	}

    // For death by blade
    public bool TakeDamage(int damage, Slicer slicer) {
        IsDead = _enemyHealth.TakeDamage(damage);

        if (gameObject.GetComponent<SliceableAsync>() != null) {
            StartCoroutine(gameObject.GetComponent<SliceableAsync>().Slice(slicer));
        }

        return IsDead;
    }

    // For death by other means
    public bool TakeDamage(int damage) {
        IsDead = _enemyHealth.TakeDamage(damage);

        if (IsDead) {
            Destroy(gameObject);
        }

        return IsDead;
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
