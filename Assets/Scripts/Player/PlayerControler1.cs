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

        private float _idleSpeed = 0;
        private float _movingSpeed = 7f;
        private float _dashingSpeed = 15f;
        private float _runningSpeed = 10.5f;
        private float _walkingSpeed = 3.5f;

        private float _dashTimer = 1f;
        private float _maxDashTimer = 1f;
        private float _runTimer = 3f;
        private float _maxRunTimer = 3f;

        private float _jumpForce;
        private float _slopeAngleLimit = 45f;
        private float _slopeStepLimit;
        [field: SerializeField] private bool _isWalking = false;

        public float turnSmoothVelocity;
        public float turnSmoothTime = .1f;
        public PlayerInputActions inputActions;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
            inputActions.Player.Enable();

            inputActions.Player.Movement.started += Movement_performed;

            inputActions.Player.Attack.performed += Attack_performed;

            inputActions.Player.Jump.performed += Jump_performed;

            inputActions.Player.Dash.started += Dash_performed;

            inputActions.Player.Walktoggle.performed += Walktoggle_performed;

            _jumpForce = _playerCollider.height / 2f * 5;
            _slopeStepLimit = _playerCollider.height / 2f + .1f;
        }

        private void Update()
        {
            MovementHandler();
            ChangeState();
        }

        private void Attack_performed(InputAction.CallbackContext obj)
        {

        }

        private void Jump_performed(InputAction.CallbackContext context)
        {
            if (IsGrounded())
            {
                Jump();
            }
        }

        private void Movement_performed(InputAction.CallbackContext obj)
        {
            if (!_isWalking)
            {
                _state = State.Moving;
            }
            else
            {
                _state = State.Walking;
            }

        }

        private void Walktoggle_performed(InputAction.CallbackContext obj)
        {
            if (_state == State.Walking)
            {
                _state = State.Moving;
                _isWalking = false;
            }
            else
            {
                _state = State.Walking;
                _isWalking = true;
            }
        }

        private void Dash_performed(InputAction.CallbackContext obj)
        {
            if (GetMovementNormalized() != Vector2.zero)
            {
                _state = State.Dashing;
            }
        }

        public Vector2 GetMovementNormalized()
        {
            return inputActions.Player.Movement.ReadValue<Vector2>().normalized;
        }

        public void MovementHandler()
        {
            Vector3 inputDir = new(GetMovementNormalized().x, 0, GetMovementNormalized().y);
            if (inputDir == Vector3.zero)
            {
                return;
            }

            float targetAngle = Mathf.Atan2(GetMovementNormalized().x, GetMovementNormalized().y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDir = GetMovementDirection(angle);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            transform.position += _moveSpeed * Time.deltaTime * moveDir;
        }

/*        private void CheckSlope()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, _playerCollider.height + 1f, _groundLayer))
            {
                if (hit.distance < _slopeStepLimit)
                {
                    _playerRb.AddForce(.3f * Vector3.up, ForceMode.VelocityChange);
                }

            }
            Debug.Log(hit.distance);
            Debug.Log(_slopeStepLimit);
        }*/

        private static Vector3 GetMovementDirection(float angle)
        {
            return Quaternion.Euler(0, angle, 0) * Vector3.forward;
        }

        public bool IsGrounded()
        {
            Vector3 playerPos = transform.position + _playerCollider.center;
            float rayDistance = _playerCollider.height / 2f + 0.1f;
            return Physics.Raycast(playerPos, Vector3.down, rayDistance, _groundLayer);
        }
        private void Jump()
        {
            _playerRb.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
        }
        private void ChangeState()
        {
            switch (_state)
            {
                case State.Idle:
                    if (_runTimer <= _maxRunTimer)
                    {
                        _runTimer += Time.deltaTime;
                        if (_dashTimer <= _maxDashTimer)
                        {
                            _dashTimer += Time.deltaTime;
                        }
                    }
                    _moveSpeed = _idleSpeed;
                    break;
                case State.Running:
                    _runTimer -= Time.deltaTime;
                    if (_runTimer <= 0)
                    {
                        if (_isWalking)
                        {
                            _state = State.Walking;
                        }
                        else
                        {
                            _state = State.Moving;
                        }
                    }
                    _moveSpeed = _runningSpeed;
                    break;
                case State.Dashing:
                    _dashTimer -= Time.deltaTime;
                    if (_dashTimer <= 0)
                    {
                        _state = State.Running;
                    }
                    _moveSpeed = _dashingSpeed;
                    break;
                case State.Moving:
                    if (_runTimer <= _maxRunTimer)
                    {
                        _runTimer += Time.deltaTime;
                        if (_dashTimer <= _maxDashTimer)
                        {
                            _dashTimer += Time.deltaTime;
                        }
                    }
                    _moveSpeed = _movingSpeed;
                    break;
                case State.Walking:
                    if (_runTimer <= _maxRunTimer)
                    {
                        _runTimer += Time.deltaTime;
                        if (_dashTimer <= _maxDashTimer)
                        {
                            _dashTimer += Time.deltaTime;
                        }
                    }
                    _moveSpeed = _walkingSpeed;
                    break;
            }
        }
    }
}
