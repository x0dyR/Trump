using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace collegeGame
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController1 : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Walking,
            Moving,
            Running,
            Dashing,
        }

        private State _state;

        [field: SerializeField] private Rigidbody _playerRb;
        [field: SerializeField] private CapsuleCollider _playerCollider;
        [field: SerializeField] private float _moveSpeed;
        [field: SerializeField] private LayerMask _groundLayer;
        private PlayerInputActions _inputActions;
        private float _maxSlopeAngle = 45f;
        private RaycastHit _slopeHit;
        public float turnSmoothVelocity = .1f;
        private float turnSmoothTime = .1f;

        private void Awake()
        {
            _inputActions = new();
            _inputActions.Player.Enable();

        }

        private void Update()
        {
            MovementHandler();
        }

        #region Move
        private void MovementHandler()
        {
            Vector3 inputDir = GetMovement();
            if (inputDir == Vector3.zero)
            {
                return;
            }
            if (OnSlope())
            {
                transform.position += GetSlopeMovementDirection() * Time.deltaTime;
            }
            transform.position += inputDir.x * Time.deltaTime * Camera.main.transform.right + inputDir.z * Time.deltaTime * Camera.main.transform.forward;
        }

        private Vector3 GetMovement()
        {
            return new(GetMovementNormalized().x, 0, GetMovementNormalized().y);
        }
        private Vector2 GetMovementNormalized()
        {
            return _inputActions.Player.Move.ReadValue<Vector2>().normalized;
        }
        private bool OnSlope()
        {

            if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, 2f))
            {
                float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
                return angle < _maxSlopeAngle && angle != 0;
            }
            return false;
        }
        private Vector3 GetSlopeMovementDirection()
        {
            return Vector3.ProjectOnPlane(GetMovement(), _slopeHit.normal).normalized;
        }
        #endregion
    }
}
