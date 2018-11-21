using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public ParticleSystem gunFire;
    private InputReader _inputReader;
    Animator _animator;

    public GameObject bullet;
    public Transform gun;
    public float shootRate = 0f;
    public float shootForce = 0f;
    private float shootRateTimeStamp = 0f;

    // Use this for initialization
    void Start () {

        _inputReader = InputReader.Instance;
        _animator = GetComponentInChildren<Animator>();


    }
	
	// Update is called once per frame
	void Update () {
        if (_inputReader.Shoot && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Time.time > shootRateTimeStamp)
            {
                GameObject go = (GameObject)Instantiate(bullet, gun.position, gun.rotation);
                gunFire.Play();
                _animator.SetTrigger("ShootTrigger");
                go.GetComponent<Rigidbody>().AddForce(gun.forward * shootForce);
                shootRateTimeStamp = Time.time + shootRate;
            }
        }

    }
}
