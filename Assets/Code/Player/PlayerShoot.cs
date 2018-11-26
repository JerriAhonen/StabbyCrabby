using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public ParticleSystem gunFire;
    private InputReader _inputReader;
    Animator _animator;

    public GameObject bullet;
    public Transform gun;
    public Vector3 aimAdjust;
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

        //FIX THESE
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized + aimAdjust, 100.0f, 1 << 9);
        Debug.DrawRay(transform.position, -Camera.main.ScreenToWorldPoint(Input.mousePosition) * 100 + aimAdjust);

        if (_inputReader.Shoot && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Time.time > shootRateTimeStamp)
            {
                GameObject go = (GameObject)Instantiate(bullet, gun.position, gun.rotation);
                gunFire.Play();
                _animator.SetTrigger("ShootTrigger");
                go.GetComponent<Rigidbody>().AddForce(gun.forward * shootForce);
                shootRateTimeStamp = Time.time + shootRate;

                
                

                if(hits.Length > 0)
                {
                    Debug.Log("Gun was Shot and we hit something");
                    foreach (RaycastHit hit in hits)
                    {
                        //hit.transform.gameObject.GetComponent<Health>().TakeDamage(100);
                        Debug.Log(hit.transform.name + " was hit with gun");
                    }
                }
            }
        }
    }
}
