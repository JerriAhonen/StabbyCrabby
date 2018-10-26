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

    // Returns whether died or not.
    public bool TakeDamage(int damage) {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, CurrentHealth);

        if (CurrentHealth == 0) {
            IsDead = true;
        }

        return IsDead;
    }

    public void SetHealth(int health) {
        if (!IsDead) {
            CurrentHealth = health;
        }
    }
}
