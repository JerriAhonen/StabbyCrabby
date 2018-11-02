﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public static PlayerCombat Instance { get; set; }

    private InputReader _inputReader;
    
    public GameObject meleeHitColliderObject;

    public float meleeHitRadius = 1.0f;
    public int knifeDamageAmount = 100;

    // Controlled thru UIControl.
    public bool canAttack = true;
    public bool specialMoveIsActive;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
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
