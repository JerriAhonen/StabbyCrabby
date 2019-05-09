using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public float DistanceBetweenAgents { private set; get; }

    //private int _points;

    public int Points { private set; get; }

    [SerializeField, HideInInspector]
    private bool _isDead;
    [SerializeField, HideInInspector]
    private GarbageCollector _garbageCollector;
    [SerializeField, HideInInspector]
    private Material[] _materials;

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

    private void Start() {
        _garbageCollector = GarbageCollector.Instance;
    }

    // Init instead of Start since changing the script execution order did nothing and this info is needed AFTER EnemyMovement Awake but before Start
    public void Init() {
        switch (enemyType) {
            case EnemyType.Toaster: {
                _startingHealth = 10000000;
                Damage = 0;
                Speed = 1f;
                AttackDistance = 0f;
                DistanceBetweenAgents = 5f;
                Points = 1000;
                break;
            }
            case EnemyType.Toast: {
                _startingHealth = 1;
                Damage = 10;
                Speed = 1f;
                AttackDistance = 1.5f;
                DistanceBetweenAgents = 2f;
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
                DistanceBetweenAgents = 4f;
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
            Animator animator = GetComponent<Animator>();
            EnemyMovement enemyMovement = GetComponent<EnemyMovement>();

            UnityEngine.Object.Destroy(animator);
            UnityEngine.Object.Destroy(enemyMovement);

            PrepareToDie();
        }

        return IsDead;
    }

    private void ConvertToRagDoll() {
        Animator animator = GetComponent<Animator>();
        Collider triggerCollider = GetComponent<Collider>();
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();

        UnityEngine.Object.Destroy(animator);
        UnityEngine.Object.Destroy(triggerCollider);
        UnityEngine.Object.Destroy(enemyMovement);

        var collidersArr = GetComponentsInChildren<Collider>();
        for(int i = 0; i < collidersArr.Length; i++) {
            var collider = collidersArr[i];
            if(collider == triggerCollider)
                continue;

            collider.isTrigger = false;
        }

        // set rigid bodies as non kinematic
        var rigidsArr = GetComponentsInChildren<Rigidbody>();
        for(int i = 0; i < rigidsArr.Length; i++) {
            var rigid = rigidsArr[i];
            rigid.isKinematic = false;
        }
    }

    private void PrepareToDie() {
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach(SkinnedMeshRenderer rend in renderers) {
            Material[] temp = rend.materials;

            _materials = _materials.Concat(temp).ToArray();
        }

        StartCoroutine(_garbageCollector.FadeOut(gameObject, _materials, 5));
    }
}
