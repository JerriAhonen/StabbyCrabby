using System.Collections.Generic;
using UnityEngine;

public class PlayerBladeCollision : MonoBehaviour {

    private PlayerCombat _pc;
    private UIManager _ui;
    private bool _isStabbing;
    private Animator _anim;
    private ParticleSystem _slashParticle;
    private InputReader _inputReader;

    private Slicer _slicer;
    
    // Use this for initialization
    void Start () {
        _pc = GetComponentInParent<PlayerCombat>();
        _ui = UIManager.Instance;
        _anim = GetComponentInParent<Animator>();
        _slashParticle = GetComponentInChildren<ParticleSystem>();
        _inputReader = InputReader.Instance;

        _slicer = GetComponent<Slicer>();
    }
	
	// Update is called once per frame
	void Update () {
        // Check if one of the Attack animations are playing.
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 1"))
        {
            _isStabbing = true;

            _slicer.OriginVector = Vector3.down;
            _slicer.DirectionVector = Vector3.right;
        }
        else if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2"))
        {
            _isStabbing = true;

            _slicer.OriginVector = Vector3.down;
            _slicer.DirectionVector = Vector3.right;
        }
        else
        {
            _isStabbing = false;
        }

        // Particle code for slash effect. Not sure if right place to put this.
        if (_inputReader.Stab)
        {
            Invoke("ShootParticles",0.07f);
        }
        if (_inputReader.Stab && _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 1"))
        {
            ShootParticles();
        }
    }

    // Shoot Particles.
    private void ShootParticles()
    {
        _slashParticle.Emit(30);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (_isStabbing && trigger.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (trigger.gameObject.GetComponent<SliceableAsync>() != null) {
                StartCoroutine(trigger.gameObject.GetComponent<SliceableAsync>().Slice(_slicer));

                // NEED TO GET DEADENEMY INFO FROM NEW PLACE
                //if (deadEnemy)
                //      Täältä jonnekin tieto et vihu kuoli. UIControllin pitäs saada tietää se.
            }

            Enemy enemy = trigger.gameObject.GetComponentInParent<Enemy>();
            Health enemyHealth = trigger.gameObject.GetComponentInParent<Health>();

            if (enemy && enemyHealth)
            {
                if (enemyHealth.CurrentHealth > 0 && enemyHealth.TakeDamage(_pc.knifeDamageAmount))
                {
                    // Enemy died
                    _ui.ComboMeter(true);
                    _ui.TempPoints(enemy.Points);
                }
                else if (enemyHealth.CurrentHealth > 0)
                {
                    // Still alive
                    _ui.ComboMeter(false);
                }
            }
            else
            {
                Debug.LogWarning("Didn't find Health.cs OR Enemy.cs on Enemy!");
            }

        }
    }
}
