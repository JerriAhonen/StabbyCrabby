using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public ParticleSystem gunFire;
    private InputReader _inputReader;
    Animator _animator;

    // Use this for initialization
    void Start () {

        _inputReader = InputReader.Instance;
        _animator = GetComponentInChildren<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (_inputReader.Shoot && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            gunFire.Play();
            _animator.SetTrigger("ShootTrigger");
        }

    }
}
