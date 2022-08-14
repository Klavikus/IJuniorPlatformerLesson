using UnityEngine;

namespace EntityComponents.Control
{
    public class EnemyAI : InputHandler
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _agroDistance;
        [SerializeField] private float _attackDistance;
        [SerializeField] private bool _allowJump;
        [SerializeField] private Vector2 _targetOffset;

        private const int MaxHorizontalInput = 1;

        private Vector3 _currentDistance;

        private void Update()
        {
            if (_target == null)
                return;

            _currentDistance = transform.position - _target.position;

            if (Mathf.Abs(_currentDistance.x) > _agroDistance)
                return;
            
            float horizontal = 0;
            bool isJumping = false;
            bool isDashing = false;
            bool isMainAttack = false;
            bool isSecondaryAttack = false;

            if (_currentDistance.x < -_attackDistance)
                horizontal = MaxHorizontalInput;

            if (_currentDistance.x > _attackDistance)
                horizontal = -MaxHorizontalInput;

            if (_allowJump && _target.position.y > transform.position.y + _targetOffset.y)
                isJumping = true;

            if (_currentDistance.x <= _attackDistance && _currentDistance.x >= -_attackDistance)
                isMainAttack = true;

            isSecondaryAttack = true;

            InputUpdated?.Invoke(new InputData(horizontal, isDashing, isJumping, isMainAttack, isSecondaryAttack));
        }
    }
}