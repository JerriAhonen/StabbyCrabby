using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour {

    protected Collider[] ChildrenCollider;
    protected Rigidbody[] ChildrenRigidbody;

    void Awake()
    {
        ChildrenCollider = GetComponentsInChildren<Collider>();
        ChildrenRigidbody = GetComponentsInChildren<Rigidbody>();
    }
	// Use this for initialization
	void Start () {
        
		foreach (var collider in ChildrenCollider)
        {
            collider.enabled = !isActiveAndEnabled;
        }
        foreach (var rigidbody in ChildrenRigidbody)
        {
            rigidbody.detectCollisions = !isActiveAndEnabled;
            rigidbody.isKinematic = isActiveAndEnabled;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
