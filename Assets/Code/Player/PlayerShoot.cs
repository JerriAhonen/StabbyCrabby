using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public static PlayerShoot Instance { get; set; }

    public bool drawDebugLines;
    public PlayerMovement pm;
    public ControlTime controlTime;

    RaycastHit camRayHit;
    Ray camRay;
    RaycastHit gunRayHit;
    Ray gunRay;

    public ParticleSystem gunFire;
    private InputReader _inputReader;
    private Animator _animator;

    public GameObject bullet;
    public Transform gun;
    public int bulletDamage;

    public LayerMask enemyLayer;
    public LayerMask playerLayer;

    public Vector3 aimPoint;

    public float stopTime;
    public float slowSpeed;

    public float shootRate = 0f;
    public float shootForce = 0f;
    private float _shootRateTimeStamp = 0f;

    private UIManager _ui;
    private AudioManager _am;
    private EnemyManager _enemyManager;

    public const int MAX_BULLETS = 6;
    public const int MIN_BULLETS = 0;
    public int bullets;
    public float bulletDestroyDelay;

    // Use this for initialization
    void Start () {

        _inputReader = InputReader.Instance;
        _ui = UIManager.Instance;
        _enemyManager = EnemyManager.Instance;
        _am = AudioManager.Instance;
        _animator = GetComponentInChildren<Animator>();

        _ui.SetBulletCount(bullets);
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
        if (Physics.Raycast(camRay.origin, camRay.direction, out camRayHit, 100.0f, ~playerLayer))
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

        Vector3 gunRayDirection = aimPoint - gun.position;
        gunRayDirection = gunRayDirection.normalized;

        // Flatten the gun vector so that it runs parallel to the ground.
        Vector3 flattenedGunrayDir = Vector3.ProjectOnPlane(gunRayDirection, Vector3.up);

        gunRay = new Ray(gun.position, flattenedGunrayDir);

        if (Physics.Raycast(gunRay.origin, gunRay.direction, out gunRayHit, 100.0f, ~playerLayer))
        {
            if (drawDebugLines) Debug.DrawRay(gunRay.origin, gunRay.direction * gunRayHit.distance, Color.white);
        }
        else
        {
            if (drawDebugLines) Debug.DrawRay(gunRay.origin, gunRay.direction * 100.0f, Color.white);
        }
    }

    void Shoot()
    {
        if (_inputReader.Shoot && bullets > MIN_BULLETS)
        {
            Debug.Log(bullets);

            if (Time.time > _shootRateTimeStamp)
            {
                // STOP TIME BEFORE SHOOTING //
                float flytime = pm.flyTime;
                
                float slowTime = (flytime / slowSpeed) - stopTime;

                StartCoroutine(controlTime.StopAndSlowTime(stopTime, slowTime, slowSpeed));
                
                // SHOOT //

                ApplyDamage();

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

    private void ApplyDamage()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(gunRay.origin, gunRay.direction);

        if (hits.Length != 0)
        {
            Debug.Log("Hit something with bullet");
            Debug.Log("Amount of hits: " + hits.Length);
        }

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.Log("Object hit by bullet: " + hit.transform.name);

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
            Enemy enemy = hit.transform.gameObject.GetComponentInParent<Enemy>();

            bool wasAlive = !enemy.IsDead;

            if (enemy)
            {
                Debug.Log("Hit enemy");

                bool isDead = enemy.TakeDamage(bulletDamage);

                if (wasAlive && isDead)
                {
                    // Enemy died
                    _ui.ComboMeter(true);
                    _ui.Points(enemy.Points);
                    _enemyManager.IncreaseKillCount(hit.transform.gameObject);
                }
                else if (!isDead)
                {
                    // Still alive
                    _ui.ComboMeter(false);
                }
            }
            else
            {
                Debug.LogWarning("Didn't find Health.cs OR Enemy.cs on Enemy!");
            }
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

        bullet.GetComponent<BulletPickup>().bs.DelayedBulletSpawn();
        Destroy(bullet);
    }
}