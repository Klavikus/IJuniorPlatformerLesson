using EntityComponents.Attack;
using EntityComponents.Movement;
using UnityEngine;

namespace EntityComponents.Input
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private MoveController2D _moveController;
        [SerializeField] private AttackController _attackController;
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

            if (_currentDistance.x < -_attackDistance)
                _moveController.HandleMove(MaxHorizontalInput);

            if (_currentDistance.x > _attackDistance)
                _moveController.HandleMove(-MaxHorizontalInput);

            if (_allowJump && _target.position.y > transform.position.y + _targetOffset.y)
                _moveController.HandleJump();

            if (_currentDistance.x <= _attackDistance && _currentDistance.x >= -_attackDistance)
                _attackController.TryAttack();

            _attackController.TrySecondaryAttack();
        }
    }
}