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

    // Returns whether died or not.
    public bool TakeDamage(int damage) {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, CurrentHealth);

        if (CurrentHealth == 0) {
            _isDead = true;
        }

        return _isDead;
    }

    public void SetHealth(int health) {
        if (!_isDead) {
            CurrentHealth = health;
        }
    }
}
