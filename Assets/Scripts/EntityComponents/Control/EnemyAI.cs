using EntityComponents.Attack;
using UnityEngine;

namespace EntityComponents.Control
{
    public class EnemyAI : InputHandler
    {
        [SerializeField] private DamageHandler _mainTarget;
        [SerializeField] private PatrolMove _patrolMove;
        [SerializeField] private float _agroDistance;
        [SerializeField] private float _attackDistance;
        [SerializeField] private bool _allowJump;
        [SerializeField] private Vector2 _targetOffset;

        private const int MaxHorizontalInput = 1;

        private Vector3 _distanceToMainTarget;
        private Vector3 _currentTargetPosition;
        private bool _movementLock;

        private void OnEnable() => _mainTarget.Died += OnMainTargetDied;

        private void OnDisable() => _mainTarget.Died -= OnMainTargetDied;

        private void Update()
        {
            if (_movementLock)
                return;

            bool canAttack = true;
            float maxPositionError = _attackDistance;

            _distanceToMainTarget = transform.position - _mainTarget.transform.position;
            _currentTargetPosition = _mainTarget.transform.position;

            if (_distanceToMainTarget.magnitude > _agroDistance)
            {
                canAttack = false;
                maxPositionError = 0;
                _currentTargetPosition = _patrolMove.CurrentTarget.position;
            }

            UpdateInputData(_currentTargetPosition, maxPositionError, canAttack);
        }

        private void UpdateInputData(Vector3 targetPosition, float maxPositionError, bool canAttack)
        {
            float horizontal = 0;
            bool isJumping = false;
            bool isDashing = false;
            bool isMainAttack = false;
            bool isSecondaryAttack = false;

            Vector3 currentDistance = transform.position - targetPosition;

            if (currentDistance.x < -maxPositionError)
                horizontal = MaxHorizontalInput;

            if (currentDistance.x > maxPositionError)
                horizontal = -MaxHorizontalInput;

            if (_allowJump && targetPosition.y > transform.position.y + _targetOffset.y)
                isJumping = true;

            if (canAttack)
            {
                if (currentDistance.x <= _attackDistance && currentDistance.x >= -_attackDistance)
                    isMainAttack = true;

                isSecondaryAttack = true;
            }


            InputUpdated?.Invoke(new InputData(horizontal, isDashing, isJumping, isMainAttack, isSecondaryAttack));
        }

        private void OnMainTargetDied() => _movementLock = true;
    }
}