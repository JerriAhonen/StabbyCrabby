using UnityEngine;

public class Ragdoll : MonoBehaviour {
    
    private Collider[] _ChildrenCollider;
    private Rigidbody[] _ChildrenRigidbody;
    private Animator _anim;
    private BoxCollider _boxCollider;

    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;

    private GameObject _crabModel;
    private GameObject _playerMover;
    private Collider _knifeCollider;
    private Rigidbody _knifeRigidBody;
    
	void Start () {

        _crabModel = gameObject.transform.GetChild(0).gameObject;
        _playerMover = GameObject.FindGameObjectWithTag("PlayerMover");
        _knifeCollider = GameObject.FindGameObjectWithTag("PlayerHitCollider").GetComponent<Collider>();
        _knifeRigidBody = GameObject.FindGameObjectWithTag("PlayerHitCollider").GetComponent<Rigidbody>();

        _anim = GetComponentInChildren<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        _playerCombat = GetComponent<PlayerCombat>();

        _playerMovement = _playerMover.GetComponent<PlayerMovement>();
        _ChildrenCollider = _crabModel.GetComponentsInChildren<Collider>();
        _ChildrenRigidbody = _crabModel.GetComponentsInChildren<Rigidbody>();
        
        RagdollActive(false);
    }
	
	void Update () {
		
        // Add here logic for Ragdoll toggle. plz

	}

    void RagdollActive(bool active)
    {
        //children
        foreach (var collider in _ChildrenCollider)
            if (collider != _knifeCollider)
                collider.enabled = active;
        foreach (var rigidbody in _ChildrenRigidbody)
        {
            if (rigidbody != _knifeRigidBody)
            {
                rigidbody.detectCollisions = active;
                rigidbody.isKinematic = !active;
            }
        }

        //root
        _knifeRigidBody.isKinematic = !active;
        _knifeCollider.enabled = !active;
        _knifeCollider.isTrigger = !active;
        _anim.enabled = !active;
        _boxCollider.enabled = !active;
        _playerMovement.enabled = !active;
        _playerCombat.enabled = !active;
    }

    // TEMP RAGDOLL AFTER DEATH, LIMBS GO BERSERK...
    public void Temp() {
        RagdollActive(true);
    }
}
