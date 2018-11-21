using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float expireTime = 2f;

	// Use this for initialization
	void Start () {

        Destroy(gameObject, expireTime);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
