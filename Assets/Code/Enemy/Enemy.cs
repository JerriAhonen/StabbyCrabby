using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum EnemyType {
        Toast = 0,
        Pot = 1
    }

    public EnemyType enemyType;

    void Start() {
		switch (enemyType) {
            case EnemyType.Toast: {

                break;
            }
            case EnemyType.Pot: {

                break;
            }
        }
	}
	
	void Update() {
		
	}
}
