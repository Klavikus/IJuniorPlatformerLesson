using System;
using System.Collections;
using UnityEngine;

namespace EntityComponents.Attack
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatAttack;
        [SerializeField] private Transform _mainWeaponHitbox;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _attackHitDelay;
        [SerializeField] private Vector2 _mainAttackHitSize;

        [SerializeField] private ThrowWeapon _secondaryWeaponPrefab;
        [SerializeField] private Transform _secondaryWeaponPivot;
        [SerializeField] private float _secondaryAttackCooldown;

        private readonly Collider2D[] _hits = new Collider2D[5];

        private bool _isAttackLocked;
        private bool _isSecondaryAttackLocked;

        public event Action AttackInitiated;

        public void TryAttack()
        {
            if (_isAttackLocked)
                return;

            StartCoroutine(Attack());
        }

        public void TrySecondaryAttack()
        {
            if (_isSecondaryAttackLocked)
                return;

            StartCoroutine(SecondaryAttack());
        }

        private IEnumerator SecondaryAttack()
        {
            _isSecondaryAttackLocked = true;

            StartCoroutine(StartCooldown(_secondaryAttackCooldown, OnEnded: () => _isSecondaryAttackLocked = false));
            ThrowWeapon spawnedObject = Instantiate(_secondaryWeaponPrefab, _secondaryWeaponPivot.position,
                _secondaryWeaponPivot.rotation);

            spawnedObject.Init(_whatAttack);

            if (transform.localScale.x < 0)
                spawnedObject.transform.Rotate(new Vector3(0, 0, 1), 180);

            yield return new WaitForSeconds(_secondaryAttackCooldown);
        }

        private IEnumerator StartCooldown(float delay, Action OnEnded)
        {
            yield return new WaitForSeconds(delay);
            OnEnded?.Invoke();
        }

        private IEnumerator Attack()
        {
            _isAttackLocked = true;
            StartCoroutine(StartCooldown(_attackCooldown, OnEnded: () => _isAttackLocked = false));

            AttackInitiated?.Invoke();

            yield return new WaitForSeconds(_attackHitDelay);

            int count = GetHitsByOverlap(in _hits);

            for (int i = 0; i < count; i++)
            {
                if (_hits[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(5f);
                }
            }
        }

        private int GetHitsByOverlap(in Collider2D[] _colliders) =>
            Physics2D.OverlapBoxNonAlloc(_mainWeaponHitbox.position, _mainAttackHitSize, 0, _colliders, _whatAttack);
    }
}