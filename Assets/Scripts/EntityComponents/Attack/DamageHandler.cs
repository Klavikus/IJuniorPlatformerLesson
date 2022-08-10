using System;
using UnityEngine;

namespace EntityComponents.Attack
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageHandler : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth;
        
        private const float DeathDelay = 1f;

        private float _currentHealth;
        private bool _isDead;

        public event Action<float> HealthChanged;
        public event Action Died;

        private void Start() => _currentHealth = _maxHealth;

        public void ApplyDamage(float damageAmount)
        {
            if (_isDead)
                return;

            _currentHealth -= damageAmount;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Died?.Invoke();
                Die();
            }

            HealthChanged?.Invoke(_currentHealth);
        }

        private void Die() => Destroy(gameObject, DeathDelay);
    }
}