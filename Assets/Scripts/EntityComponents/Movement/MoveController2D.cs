using System;
using System.Collections;
using EntityComponents.Control;
using UnityEngine;

namespace EntityComponents.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class MoveController2D : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private LayerMask _whatIsWall;
        [SerializeField] private Transform _groundCheckAnchor;
        [SerializeField] private Transform _wallCheckAnchor;
        [SerializeField] private float _groundedRadius;
        [SerializeField] private Vector2 _wallCheckerSize;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashDuration;
        [SerializeField] private float _dashCooldown;
        [SerializeField] private bool _isFacingRight;

        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider;
        private bool _isDoubleJumpAllowed;
        private bool _canDash;
        private bool _isMoveLocked;
        private bool _isFacedWall;

        public bool IsGrounded { private set; get; }
        public bool IsDashing { private set; get; }
        public float CurrentMoveSpeed { private set; get; }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
            _isDoubleJumpAllowed = true;
            _canDash = true;
        }

        private void Update() => UpdateStatus();

        public void HandleInput(InputData inputData)
        {
            if (Math.Abs(inputData.Horizontal) > 0.1f)
                HandleMove(inputData.Horizontal);

            if (inputData.IsJumping)
                HandleJump();

            if (inputData.IsDashing) 
                HandleDash();
        }

        private void HandleMove(float horizontalInput)
        {
            if (_isMoveLocked)
                return;

            if ((horizontalInput > 0 && !_isFacingRight) || (horizontalInput < 0 && _isFacingRight))
                Flip();

            if (_isFacedWall)
                return;

            _rigidbody.velocity = new Vector2(horizontalInput * _horizontalSpeed, _rigidbody.velocity.y);
        }

        private void HandleJump()
        {
            if (IsGrounded || _isDoubleJumpAllowed)
            {
                _isDoubleJumpAllowed = false;
                Vector2 currentVelocity = _rigidbody.velocity;
                currentVelocity.y = 0;
                _rigidbody.velocity = currentVelocity;
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        private void HandleDash()
        {
            if (_canDash == false)
                return;

            StartCoroutine(Dash());
        }

        private IEnumerator Dash()
        {
            _isMoveLocked = true;
            _canDash = false;
            IsDashing = true;

            Vector2 dashDirection = _isFacingRight ? Vector2.right : Vector2.left;
            float currentDashTime = 0f;

            Vector2 initialColliderSize = _collider.size;
            Vector2 initialColliderOffset = _collider.offset;

            _collider.offset = new Vector2(initialColliderOffset.x, initialColliderOffset.y * 1.5f);
            _collider.size = initialColliderSize * 0.5f;

            while (currentDashTime < _dashDuration)
            {
                currentDashTime += Time.deltaTime;
                _rigidbody.velocity = new Vector2(dashDirection.x * _dashSpeed, _rigidbody.velocity.y);
                yield return null;
            }

            _collider.size = initialColliderSize;
            _collider.offset = initialColliderOffset;

            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            _isMoveLocked = false;
            IsDashing = false;

            yield return new WaitForSeconds(_dashCooldown);

            _canDash = true;
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        private void UpdateStatus()
        {
            IsGrounded = Physics2D.OverlapCircle(_groundCheckAnchor.position, _groundedRadius, _whatIsGround);
            _isFacedWall = Physics2D.OverlapBox(_wallCheckAnchor.position, _wallCheckerSize, 0,_whatIsWall);

            if (_isDoubleJumpAllowed == false && IsGrounded)
                _isDoubleJumpAllowed = true;

            CurrentMoveSpeed = Mathf.Abs(_rigidbody.velocity.x) / _horizontalSpeed;
        }
    }
}