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
    public int Damage { private set; get; }

    public float Speed { private set; get; }

    public float AttackDistance { private set; get; }

    //private int _points;

    public int Points { private set; get; }

    [SerializeField, HideInInspector]
    private bool _isDead;

    public bool IsDead {
        get {
            return _isDead;
        }
        private set {
            _isDead = value;
        }
    }

    private UIManager _ui;

    private void Awake() {
        _enemyHealth = GetComponent<Health>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    // Init instead of Start since changing the script execution order did nothing and this info is needed AFTER EnemyMovement Awake but before Start
    public void Init() {
        switch (enemyType) {
            case EnemyType.Toaster: {
                _startingHealth = 1;
                Damage = 0;
                Speed = 1f;
                AttackDistance = 0f;
                Points = 1000;
                break;
            }
            case EnemyType.Toast: {
                _startingHealth = 1;
                Damage = 10;
                Speed = 5f;
                AttackDistance = 1.5f;
                Points = 100;

                transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 180f), Vector3.up);

                if (_enemyMovement != null) {
                    _enemyMovement.GetThrown(transform.forward);
                }
                break;
            }
            case EnemyType.Pot: {
                _startingHealth = 1;
                Damage = 20;
                Speed = 5f;
                AttackDistance = 10f;
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
}
