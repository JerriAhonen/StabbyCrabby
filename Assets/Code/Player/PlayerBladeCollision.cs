using UnityEngine;

public class PlayerBladeCollision : MonoBehaviour {

    private PlayerCombat pc;
    private bool isStabbing;
    private Animator anim;

	// Use this for initialization
	void Start () {
        pc = GetComponentInParent<PlayerCombat>();
        anim = GetComponentInParent<Animator>();
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
