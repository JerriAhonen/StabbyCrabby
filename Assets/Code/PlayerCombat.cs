using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    private InputReader _inputReader;

    public GameObject meleeColliderVisual;  // Debugging

    public float meleeHitRadius = 1.0f;
    public float knifeDamageAmount = 100.0f;

    // Use this for initialization
    void Start () {
        _inputReader = InputReader.Instance;

        meleeColliderVisual.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
        if (_inputReader.Stab)
        {
            meleeColliderVisual.SetActive(true);
            Stab();
        }
        else
        {
            meleeColliderVisual.SetActive(false);
        }

	}

    void Stab()
    {
        Collider[] enemiesHit = Physics.OverlapSphere(meleeColliderVisual.transform.position, meleeHitRadius);

        foreach (Collider col in enemiesHit)
        {
            Debug.Log("Hit enemy: " + col.transform.name);
            //Health enemyHealth = col.gameObject.GetComponent<Health>();
            //enemyHealth -= knifeDamageAmount;
        }
    }
    

}
