using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public bool drawDebugLines;
    public PlayerMovement pm;

    RaycastHit camRayHit;
    Ray camRay;
    RaycastHit gunRayHit;
    Ray gunRay;

    public ParticleSystem gunFire;
    private InputReader _inputReader;
    private Animator _animator;

    public GameObject bullet;
    public Transform gun;

    public LayerMask enemyLayer;

    public Vector3 aimPoint;

    public float shootRate = 0f;
    public float shootForce = 0f;
    private float _shootRateTimeStamp = 0f;

    private UIManager _ui;
    private AudioManager _am;

    public const int MAX_BULLETS = 6;
    public const int MIN_BULLETS = 0;
    public int bullets;
    public float bulletDestroyDelay;

    // Use this for initialization
    void Start () {

        _inputReader = InputReader.Instance;
        _ui = UIManager.Instance;
        _am = AudioManager.Instance;
        _animator = GetComponentInChildren<Animator>();

        bullets = 1;
        _ui.SetBulletCount(1);
    }
	
	// Update is called once per frame
	void Update () {

        Aim();
        Shoot();
    }

    void Aim()
    {
        // Get the point where the player is looking in world space
        camRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        // Cast a ray straight out of the camera.
        // If we hit something, set that point as the aim point.
        // If we don't hit anything, get set the point on camRay at 100 distance as the aim point.
        // (We could set the point to the border of the arena)
        if (Physics.Raycast(camRay.origin, camRay.direction, out camRayHit))
        {
            aimPoint = camRayHit.point;
            if (drawDebugLines) Debug.DrawRay(camRay.origin, camRay.direction * camRayHit.distance, Color.black);
        }
        else
        {
            aimPoint = camRay.GetPoint(100.0f);
            if (drawDebugLines) Debug.DrawRay(camRay.origin, camRay.direction * 100.0f, Color.red);
        }

        // Cast a ray from the gun's position to the aimpoint 
        // (GET THE DIRECTION TO AIMPOINT BY SUBSTRACTING GUN.POSITION FROM IT)
        gunRay = new Ray(gun.position, aimPoint - gun.position);

        if (Physics.Raycast(gunRay.origin, gunRay.direction, out gunRayHit, ~enemyLayer))
        {
            if (drawDebugLines) Debug.DrawRay(gunRay.origin, gunRay.direction * gunRayHit.distance, Color.white);
        }
        else
        {
            if (drawDebugLines) Debug.DrawRay(gunRay.origin, gunRay.direction * Vector3.Distance(gunRay.origin, aimPoint), Color.white);
        }
    }

    void Shoot()
    {
        if (_inputReader.Shoot && bullets > MIN_BULLETS)
        {
            if (Time.time > _shootRateTimeStamp)
            {
                GameObject go = (GameObject)Instantiate(bullet, gun.position, gun.rotation);
                gunFire.Play();
                _animator.SetTrigger("ShootTrigger");
                go.GetComponent<Rigidbody>().AddForce(gunRay.direction * shootForce);
                _shootRateTimeStamp = Time.time + shootRate;

                _ui.RemoveBullet();
                bullets--;

                _am.Play("Gunshot1");

                // Tell PlayerMovement to move the crab backwards.
                pm.FlyFromShooting();
            }
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.layer == LayerMask.NameToLayer("Collectable"))
        {
            if (bullets < MAX_BULLETS)
            {
                bullets++;
                StartCoroutine(DestroyBulletPickUp(trigger.gameObject, bulletDestroyDelay));
                _ui.AddBullet();
            }
        }
    }
    
    IEnumerator DestroyBulletPickUp(GameObject bullet, float delay)
    {
        float i = delay;
        while (i >= 0)
        {
            i -= Time.deltaTime;
            yield return null;
        }

        Destroy(bullet);
    }
}