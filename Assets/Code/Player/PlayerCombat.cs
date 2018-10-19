using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public static PlayerCombat Instance { get; set; }

    private InputReader _inputReader;
    
    public GameObject meleeHitColliderObject;

    public float meleeHitRadius = 1.0f;
    public float knifeDamageAmount = 100.0f;

    // Controlled thru UIControl.
    public bool canAttack = true;
    public bool specialMoveIsActive;

    void Awake()
    {
        Instance = this;    
    }

    void Start () {
        _inputReader = InputReader.Instance;
        meleeHitColliderObject = GameObject.FindGameObjectWithTag("PlayerHitCollider");
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (_inputReader.Stab)
        {
            Stab();
        }
	}

    void Stab()
    {
        
    }

}
