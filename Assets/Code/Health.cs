using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    private int _currentHealth;

    public int CurrentHealth {
        get {
            return _currentHealth;
        }
        private set {
            _currentHealth = value;
        }
    }

    private bool _isDead;

    public bool IsDead {
        get {
            return _isDead;
        }
        private set {
            _isDead = value;
        }
    }
    
    public Health(int startingHealth) {
        CurrentHealth = startingHealth;
    }

    public void TakeDamage(int damage) {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, CurrentHealth);

        if (CurrentHealth == 0) {
            IsDead = true;
        }
    }

    public void SetHealth(int health) {
        if (!IsDead) {
            CurrentHealth = health;
        }
    }
}
