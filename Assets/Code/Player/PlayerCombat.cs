using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public static PlayerCombat Instance { get; set; }

    private InputReader _ir;
    private AudioManager _am;

    public int knifeDamageAmount;
    public GameObject meleeHitColliderObject;
    
    public float fireRate;
    public string[] comboParams;
    public int comboIndex = 0;
    Animator _animator;
    public float resetTimer;

    // SHOULD THERE BE A PLAYER CLASS THAT INITIALIZES HEALTH, 
    // GETS SENT INFO ABOUT DEATH, THEN SETS RAGDOLL TO TRUE 
    // AND TELLS UI IT'S "GAMEOVER, MAN, GAMEOVER!"??
    private Health _health;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start () {
        _ir = InputReader.Instance;
        _am = AudioManager.Instance;
        meleeHitColliderObject = GameObject.FindGameObjectWithTag("PlayerHitCollider");
        
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
            comboParams = new string[] { "Attack 1", "Attack 2" };

        _animator = GetComponentInChildren<Animator>();

        // SHOULD THERE BE A PLAYER CLASS THAT INITIALIZES HEALTH, 
        // GETS SENT INFO ABOUT DEATH, THEN SETS RAGDOLL TO TRUE 
        // AND TELLS UI IT'S "GAMEOVER, MAN, GAMEOVER!"??
        _health = GetComponentInChildren<Health>();

        _health.SetHealth(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Stab(_ir.Stab);
	}

    void Stab(bool stab)
    {
        if (stab && comboIndex < comboParams.Length)
        {
            _animator.SetTrigger(comboParams[comboIndex]);
            _am.PlayWithRandomPitch("SwordWoosh1", 0.5f, 2.0f);
            
            comboIndex = (comboIndex + 1) % comboParams.Length;

            resetTimer = 0f;
        }

        // Reset combo if the user has not clicked quickly enough
        if (comboIndex > 0)
        {
            resetTimer += Time.deltaTime;

            if (resetTimer > fireRate)
            {
                _animator.SetTrigger("Reset");
                comboIndex = 0;
            }
        }
    }
}
