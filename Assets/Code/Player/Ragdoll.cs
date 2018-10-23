using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour {

    private InputReader _inputReader;
    protected Collider[] ChildrenCollider;
    protected Rigidbody[] ChildrenRigidbody;
    protected NewMovement playerMovement;
    protected PlayerCombo playerCombo;
    protected PlayerCombat playerCombat;
    protected Animator anim;
    //protected Rigidbody rb;
    protected BoxCollider boxCollider;

    GameObject crabModel;
    Collider knifeCollider;

	// Use this for initialization
	void Start () {

        crabModel = gameObject.transform.GetChild(0).gameObject;
        knifeCollider = GameObject.FindGameObjectWithTag("PlayerHitCollider").GetComponent<Collider>();
        anim = GetComponentInChildren<Animator>();
        //rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        playerMovement = GetComponent<NewMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerCombo = GetComponent<PlayerCombo>();
        ChildrenCollider = crabModel.GetComponentsInChildren<Collider>();
        ChildrenRigidbody = crabModel.GetComponentsInChildren<Rigidbody>();
        _inputReader = InputReader.Instance;
        RagdollActive(false);


        foreach (var collider in ChildrenCollider)
            collider.enabled = !isActiveAndEnabled;
        foreach (var rigidbody in ChildrenRigidbody)
        {
            rigidbody.detectCollisions = !isActiveAndEnabled;
            rigidbody.isKinematic = isActiveAndEnabled;
        }
        
        knifeCollider.enabled = true;
        knifeCollider.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
		
        // Add here logic for Ragdoll toggle. plz

	}

    void RagdollActive(bool active)
    {
        //children
        foreach (var collider in ChildrenCollider)
            collider.enabled = active;
        foreach (var rigidbody in ChildrenRigidbody)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        //root
        knifeCollider.isTrigger = false;
        anim.enabled = !active;
        //rb.detectCollisions = !active;
        //rb.isKinematic = active;
        boxCollider.enabled = !active;
        playerMovement.enabled = !active;
        playerCombo.enabled = !active;
        playerCombat.enabled = !active;
    }
}
