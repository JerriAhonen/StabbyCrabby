using UnityEngine;

public class PlayerBladeCollision : MonoBehaviour {

    private PlayerCombat _pc;
    private bool _isStabbing;
    private Animator _anim;
    private ParticleSystem _slashParticle;
    private InputReader _inputReader;

    // Use this for initialization
    void Start () {
        _pc = GetComponentInParent<PlayerCombat>();
        _anim = GetComponentInParent<Animator>();
        _slashParticle = GetComponentInChildren<ParticleSystem>();
        _inputReader = InputReader.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        // Check if one of the Attack animations are playing.
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") 
            || _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2"))
        {
            _isStabbing = true;
        }
        else
        {
            _isStabbing = false;
        }

        // Particle code for slash effect. Not sure if right place to put this.
        if (_pc.canAttack && _inputReader.Stab)
        {
            Invoke("ShootParticles",0.07f);

        }
        if (_pc.canAttack && _inputReader.Stab && _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 1"))
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
            // TODO: ENEMY HEALTH
            // EnemyHealth eh = collision.gameObject.GetComponent<EnemyHealth>();
            // eh.GetHit();
            // if (eh.dead)
            //      Täältä jonnekin tieto et vihu kuoli. UIControllin pitäs saada tietää se.

            Debug.Log(trigger.gameObject.name + " got Hit");
        }
    }
}
