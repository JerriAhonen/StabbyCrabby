using UnityEngine;

public class PlayerBladeCollision : MonoBehaviour {

    private PlayerCombat pc;
    private bool isStabbing;
    private Animator anim;
    private ParticleSystem slashParticle;
    private InputReader _inputReader;

    // Use this for initialization
    void Start () {
        pc = GetComponentInParent<PlayerCombat>();
        anim = GetComponentInParent<Animator>();
        slashParticle = GetComponentInChildren<ParticleSystem>();
        _inputReader = InputReader.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") 
            || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2"))
        {
            isStabbing = true;
        }
        else
        {
            isStabbing = false;
        }
        // Particle code for slash effect. Not sure if right place to put this.
        if (pc.canAttack && _inputReader.Stab)
        {
            Invoke("ShootParticles",0.07f);
        }
        if (pc.canAttack && _inputReader.Stab && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 1"))
        {
            ShootParticles();
        }
    }

    // Shoot Particles.
    private void ShootParticles()
    {
        slashParticle.Emit(30);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isStabbing && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // TODO: ENEMY HEALTH
            // collision.gameObject.GetComponent<EnemyHealth>().GetHit();
            Debug.Log(other.gameObject.name + " got Hit");
        }
    }
}
