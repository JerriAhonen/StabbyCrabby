using UnityEngine;

public class Ragdoll : MonoBehaviour {

    private InputReader _inputReader;

    private Collider[] _ChildrenCollider;
    private Rigidbody[] _ChildrenRigidbody;
    private Animator _anim;
    private BoxCollider _boxCollider;

    private PlayerMovement _playerMovement;
    private PlayerCombo _playerCombo;
    private PlayerCombat _playerCombat;

    private GameObject _crabModel;
    private GameObject _playerMover;
    private Collider _knifeCollider;
    
	void Start () {
        _inputReader = InputReader.Instance;

        _crabModel = gameObject.transform.GetChild(0).gameObject;
        _playerMover = GameObject.FindGameObjectWithTag("PlayerMover");
        _knifeCollider = GameObject.FindGameObjectWithTag("PlayerHitCollider").GetComponent<Collider>();

        _anim = GetComponentInChildren<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        _playerCombat = GetComponent<PlayerCombat>();
        _playerCombo = GetComponent<PlayerCombo>();

        _playerMovement = _playerMover.GetComponent<PlayerMovement>();
        _ChildrenCollider = _crabModel.GetComponentsInChildren<Collider>();
        _ChildrenRigidbody = _crabModel.GetComponentsInChildren<Rigidbody>();
        
        RagdollActive(false);
        
        _knifeCollider.enabled = true;
        _knifeCollider.isTrigger = true;
    }
	
	void Update () {
		
        // Add here logic for Ragdoll toggle. plz

	}

    void RagdollActive(bool active)
    {
        //children
        foreach (var collider in _ChildrenCollider)
            collider.enabled = active;
        foreach (var rigidbody in _ChildrenRigidbody)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        //root
        _knifeCollider.isTrigger = false;
        _anim.enabled = !active;
        _boxCollider.enabled = !active;
        _playerMovement.enabled = !active;
        _playerCombo.enabled = !active;
        _playerCombat.enabled = !active;
    }
}
