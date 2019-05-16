using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatCollision : MonoBehaviour {

    private GameManager _gameManager;
    private Enemy _enemy;

    // Use this for initialization
    void Awake () {
        _gameManager = GameManager.Instance;
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if(collider.gameObject.GetComponent<Health>() != null) {
                bool dead = collider.gameObject.GetComponent<Health>().TakeDamage(_enemy.Damage);

                // SHOULD THERE BE A PLAYER CLASS THAT INITIALIZES HEALTH, 
                // GETS SENT INFO ABOUT DEATH, THEN SETS RAGDOLL TO TRUE 
                // AND TELLS UI IT'S "GAMEOVER, MAN, GAMEOVER!"??
                if(dead) {
                    _gameManager.End(false, this.gameObject.tag);

                    if (collider.gameObject.GetComponent<Ragdoll>() != null)
                        collider.gameObject.GetComponent<Ragdoll>().Temp();
                }
            }
        }
    }
}
