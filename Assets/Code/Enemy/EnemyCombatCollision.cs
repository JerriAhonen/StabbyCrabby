using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatCollision : MonoBehaviour {

    private UIManager _ui;
    private Enemy _enemy;

    // Use this for initialization
    void Awake () {
        _ui = UIManager.Instance;
        _enemy = GetComponentInParent<Enemy>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if(collider.gameObject.GetComponent<Health>() != null) {
                bool dead = collider.gameObject.GetComponent<Health>().TakeDamage(_enemy.Damage);

                // SHOULD THERE BE A PLAYER CLASS THAT INITIALIZES HEALTH, 
                // GETS SENT INFO ABOUT DEATH, THEN SETS RAGDOLL TO TRUE 
                // AND TELLS UI IT'S "GAMEOVER, MAN, GAMEOVER!"??
                if(dead) {
                    _ui.GameOver();
                    collider.gameObject.GetComponent<Ragdoll>().Temp();
                }
            }
        }
    }
}
