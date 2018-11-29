using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public ParticleSystem gunFire;
    private InputReader _inputReader;
    Animator _animator;

    public GameObject bullet;
    public Transform gun;

    public LayerMask enemyLayer;

    [Range(0.0f, 1.0f)]
    public float aimPointX;
    [Range(0.0f, 1.0f)]
    public float aimPointY;
    private float aimPointZ = 0.0f;


    public Vector3 aimPoint;

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

        
        
        // Get the point where the player is looking in world space
        RaycastHit hit;
        Ray camRay = Camera.main.ViewportPointToRay(new Vector3(aimPointX, aimPointY, aimPointZ));
        



        if (Physics.Raycast(camRay.origin, camRay.direction, out hit))
        {
            aimPoint = hit.point;
            Debug.DrawRay(camRay.origin, camRay.direction * hit.distance, Color.black);
        }
        else
        {
            aimPoint = camRay.GetPoint(100.0f);
            Debug.DrawRay(camRay.origin, camRay.direction * 100.0f, Color.red);
        }
        



        RaycastHit hit2;
        Ray gunRay = new Ray(gun.position, aimPoint);

        if (Physics.Raycast(gunRay.origin, gunRay.direction, out hit2, ~enemyLayer))
        {
            Debug.DrawRay(gunRay.origin, gunRay.direction * hit2.distance, Color.white);
        }
        else
        {
            Debug.DrawRay(gunRay.origin, gunRay.direction * Vector3.Distance(gunRay.origin, aimPoint), Color.white);
        }
        




        if (_inputReader.Shoot && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Time.time > shootRateTimeStamp)
            {
                GameObject go = (GameObject)Instantiate(bullet, gun.position, gun.rotation);
                gunFire.Play();
                _animator.SetTrigger("ShootTrigger");
                go.GetComponent<Rigidbody>().AddForce(gunRay.direction * shootForce);
                shootRateTimeStamp = Time.time + shootRate;

                hit.transform.gameObject.GetComponent<Health>().TakeDamage(100);
                Debug.Log(hit2.transform.name + " was hit with gun");

                /*
                if(hits.Length > 0)
                {
                    Debug.Log("Gun was Shot and we hit something");
                    foreach (RaycastHit hit2 in hits)
                    {
                        //hit.transform.gameObject.GetComponent<Health>().TakeDamage(100);
                        Debug.Log(hit2.transform.name + " was hit with gun");
                    }
                }*/
            }
        }
    }
}
//go.GetComponent<Rigidbody>().AddForce(gun.forward * shootForce);