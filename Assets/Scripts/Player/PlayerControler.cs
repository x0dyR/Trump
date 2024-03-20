using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;


namespace collegeGame
{
    public class PlayerControler : MonoBehaviour
    {
        #region Variables: Movement

        private Vector2 _input;
        [field: SerializeField]private CharacterController _characterController;
        private Vector3 _direction;

        [SerializeField] private float speed;

        #endregion
        #region Variables: Rotation

        [SerializeField] private float smoothTime = 0.05f;
        private float _currentVelocity;

        #endregion
        #region Variables: Gravity

        private float _gravity = -9.81f;
        [SerializeField] private float gravityMultiplier = 3.0f;
        private float _velocity;

        #endregion
        #region Variables: Jumping

        [SerializeField] private float jumpPower;

        #endregion

        private PlayerInputActions inputActions;


        private void Awake()
        {
            inputActions = new();
            inputActions.Player.Enable();inputActions.Player.Jump.started += Jump_performed;
        }

        private void Update()
        {
            ApplyGravity();
            ApplyRotation();
            ApplyMovement();
            Move();
        }

        private void ApplyGravity()
        {
            if (IsGrounded() && _velocity < 0.0f)
            {
                _velocity = -1.0f;
            }
            else
            {
                _velocity += _gravity * gravityMultiplier * Time.deltaTime;
            }

            _direction.y = _velocity;
        }

        private void ApplyRotation()
        {
            if (_input.sqrMagnitude == 0) return;

            var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }

        private void ApplyMovement()
        {
            _characterController.Move(_direction * speed * Time.deltaTime);
        }

        public void Move()
        {
            _input = inputActions.Player.Move.ReadValue<Vector2>().normalized;
            _direction = new Vector3(_input.x, 0.0f, _input.y);
        }

        public void Jump()
        {
            _velocity += jumpPower;
        }

        private void Jump_performed(InputAction.CallbackContext obj)
        {
            Jump();
        }

        private bool IsGrounded() => _characterController.isGrounded;
        /*
        public enum State
        {
            idle,
            walk,
            move,
            run,
            dash,
        }
        private PlayerInputActions _inputActions;
        [field: SerializeField] private CharacterController _characterContoroller;
        public float smoothVelocity;
        public float turnSmoothTime = .1f;
        public bool isWalking = false;
        public float speed;


        private float _idleSpeed = 0;
        private float _walkSpeed = 3.5f;
        private float _moveSpeed = 7f;
        private float _runSpeed = 10.5f;
        private float _dashSpeed = 15f;

        private float dashTimer = 1;
        private const float MaxDashTimer = 1;
        private float runTimer = 5;
        private const float MaxRunTimer = 5;

        Vector3 moveDir;
        private State _state;

        private void Awake()
        {
            _inputActions = new();
            _inputActions.Player.Enable();
            _inputActions.Player.Walktoggle.performed += Walktoggle_performed;
            _inputActions.Player.Movement.started += Movement_performed;
            _inputActions.Player.Dash.performed += Dash_performed;
            _state = State.idle;
        }

        private void Dash_performed(InputAction.CallbackContext obj)
        {
            _state = State.dash;
        }

        private void Movement_performed(InputAction.CallbackContext obj)
        {
            _state = State.move;
        }

        private void Walktoggle_performed(InputAction.CallbackContext obj)
        {
            _state = State.walk;
        }

        private void Update()
        {
            MovementHandler();
            ChangeState();
        }
        private void FixedUpdate()
        {
            GravityHandler();
        }
        private void MovementHandler()
        {
            Vector2 inputDir = GetMovement();
            moveDir = new(inputDir.x, 0, inputDir.y);
            if (moveDir == Vector3.zero)
            { return; }
            float targetAngle = Mathf.Atan2(GetMovement().x, GetMovement().y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            _characterContoroller.Move(speed * Time.deltaTime * moveDir);
        }
        private Vector2 GetMovement()
        {

            return _inputActions.Player.Movement.ReadValue<Vector2>().normalized;
        }
        private void GravityHandler()
        {
            if (!IsGrounded())
            {
                moveDir.y += Physics.gravity.y;
                _characterContoroller.Move(moveDir * Time.deltaTime);
            }
            else
            {
                moveDir.y = -_characterContoroller.height / 2f + .2f;
            }
        }

        private bool IsGrounded()
        {
            if (!Physics.Raycast(transform.position + _characterContoroller.center, Vector3.down, _characterContoroller.height / 2f + .2f))
            {
                return false;
            }
            return true;
        }
        private void ChangeState()
        {
            switch (_state)
            {
                case State.idle:
                    speed = _idleSpeed;
                    if (dashTimer < MaxDashTimer)
                    {
                        dashTimer += Time.deltaTime;
                    }
                    if (runTimer < MaxRunTimer)
                    {
                        runTimer += Time.deltaTime;
                    }
                    break;
                case State.walk:
                    speed = _walkSpeed;
                    if (dashTimer < MaxDashTimer)
                    {
                        dashTimer += Time.deltaTime;
                    }
                    if (runTimer < MaxRunTimer)
                    {
                        runTimer += Time.deltaTime;
                    }
                    break;
                case State.move:
                    speed = _moveSpeed;
                    if (dashTimer < MaxDashTimer)
                    {
                        dashTimer += Time.deltaTime;
                    }
                    if (runTimer < MaxRunTimer)
                    {
                        runTimer += Time.deltaTime;
                    }
                    break;
                case State.dash:
                    speed = _dashSpeed;
                    dashTimer -= Time.deltaTime;
                    if (dashTimer <= 0f)
                    {
                        _state = State.run;
                    }
                    break;
                case State.run:
                    speed = _runSpeed;
                    runTimer -= Time.deltaTime;
                    if (runTimer <= 0f)
                    {
                        _state = State.move;
                    }
                    break;
            }
        }*/
    }
}