using System;
using System.Collections;
using EntityComponents.Control;
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
        private bool _isAttacking;

        public bool IsAttacking
        {
            get
            {
                if (_isAttacking == false)
                    return false;

                _isAttacking = false;
                return true;
            }
        }

        public void HandleInput(InputData inputData)
        {
            if (inputData.IsMainAttack)
                TryAttack();

            if (inputData.IsSecondaryAttack)
                TrySecondaryAttack();
        }

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

            StartCoroutine(StartCooldown(_secondaryAttackCooldown, onEnded: () => _isSecondaryAttackLocked = false));
            ThrowWeapon throwWeapon = Instantiate(_secondaryWeaponPrefab, _secondaryWeaponPivot.position,
                _secondaryWeaponPivot.rotation);

            throwWeapon.Init(_whatAttack);

            if (transform.localScale.x < 0)
                throwWeapon.transform.Rotate(new Vector3(0, 0, 1), 180);

            yield return new WaitForSeconds(_secondaryAttackCooldown);
        }

        private IEnumerator StartCooldown(float delay, Action onEnded)
        {
            yield return new WaitForSeconds(delay);
            onEnded?.Invoke();
        }

        private IEnumerator Attack()
        {
            _isAttackLocked = true;
            _isAttacking = true;

            StartCoroutine(StartCooldown(_attackCooldown, onEnded: () => _isAttackLocked = false));

            yield return new WaitForSeconds(_attackHitDelay);

            int count = GetHitsByOverlap(in _hits);

            for (int i = 0; i < count; i++)
            {
                if (_hits[i].TryGetComponent(out IDamageable damageable)) 
                    damageable.ApplyDamage(5f);
            }

            _isAttacking = false;
        }

        private int GetHitsByOverlap(in Collider2D[] colliders) =>
            Physics2D.OverlapBoxNonAlloc(_mainWeaponHitbox.position, _mainAttackHitSize, 0, colliders, _whatAttack);
    }
}