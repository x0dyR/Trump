using UnityEngine;
using UnityEngine.InputSystem;


namespace collegeGame
{

    [RequireComponent(typeof(CharacterController))]

    public class PlayerControler : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Walking,
            Moving,
            Running,
            Dashing,
            Jump,
        }

        [field: SerializeField] CharacterController _characterController;

        [field: SerializeField] private float _idleSpeed = 0f;
        [field: SerializeField] private float _idleTimer = 5f;

        [field: SerializeField] private float _walkingSpeed = 3.5f;

        [field: SerializeField] private float _movingSpeed = 7f;
        [Header("Running")]
        [field: SerializeField] private float _runningSpeed = 10.5f;
        [field: SerializeField] private float _maxRunningTimer = 5f;
        [field: SerializeField] private float _runningTimer = 0;
        [Header("Dashing")]
        [field: SerializeField] private float _dashingSpeed = 15f;
        [field: SerializeField] private float _maxdashTimer = 1f;
        [field: SerializeField] private float _dashTimer = 0;
        [Header("Gravity")]
        [field: SerializeField] private float _playerAttraction;
        [field: SerializeField] private LayerMask _groundLayer;
        [field: SerializeField] private float _jumpForce;
        private float _gravityForce = 9.81f;

        private Vector3 moveDir;
        private State state;

        [Header("Other")]
        public PlayerInputActions inputActions;
        public float turnSmoothVelocity;
        public float turnSmoothTime = .1f;
        public bool isWalking = false;

        private void Awake()
        {
            state = State.Idle;
            inputActions = new();
            inputActions.Player.Enable();

            inputActions.Player.Dash.performed += Dash_performed;

            inputActions.Player.Walktoggle.performed += Walktoggle_performed;

            inputActions.Player.Movement.started += Movement_performed;

            inputActions.Player.Jump.performed += Jump_performed;

        }
        private void FixedUpdate()
        {

        }
        private void Update()
        {
            ChangeState();
            GravityHandler();
            Rotate();
        }

        private void Dash_performed(InputAction.CallbackContext context)
        {
            state = State.Dashing;
        }

        private void Movement_performed(InputAction.CallbackContext context)
        {
            if (isWalking)
            {
                state = State.Walking;
            }
            else
            {
                state = State.Moving;
            }
        }
        private void Walktoggle_performed(InputAction.CallbackContext context)
        {
            if (!isWalking)
            {
                state = State.Walking;
                isWalking = true;
            }
            else
            {
                state = State.Moving;
                isWalking = false;
            }
        }
        private void Jump_performed(InputAction.CallbackContext obj)
        {

            Jump();
        }



        public Vector2 GetMovementNormalized()
        {
            return inputActions.Player.Movement.ReadValue<Vector2>().normalized;
        }

        public void MovementHandler(float moveSpeed)
        {

            Vector3 inputDir = new(GetMovementNormalized().x, 0, GetMovementNormalized().y);
            if (inputDir == Vector3.zero)
            {
                return;
            }
            _characterController.Move(moveSpeed * Time.deltaTime * moveDir);

        }

        public void Rotate()
        {
            float targetAngle = Mathf.Atan2(GetMovementNormalized().x, GetMovementNormalized().y)
                * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        public void Jump()
        {
            moveDir = Vector3.zero;
            moveDir.y += _jumpForce * Time.deltaTime;
            _characterController.Move(moveDir);
            Debug.Log(moveDir);
        }
        public void GravityHandler()
        {
            if (!_characterController.isGrounded)
            {
                _characterController.Move(_gravityForce * Time.deltaTime * Vector3.down);
            }
        }

        private void ChangeState()
        {
            switch (state)
            {
                case State.Idle:
                    MovementHandler(_idleSpeed);
                    if (_runningTimer >= 0)
                    {
                        _runningTimer -= Time.deltaTime; if (_dashTimer >= 0)
                        {
                            _dashTimer -= Time.deltaTime;
                        }
                    }
                    break;
                case State.Running:
                    MovementHandler(_runningSpeed);
                    _runningTimer += Time.deltaTime;
                    if (_runningTimer >= _maxRunningTimer)
                    {
                        state = State.Moving;
                    }
                    break;
                case State.Moving:
                    MovementHandler(_movingSpeed);
                    if (_runningTimer >= 0)
                    {
                        _runningTimer -= Time.deltaTime; if (_dashTimer >= 0)
                        {
                            _dashTimer -= Time.deltaTime;
                        }
                    }
                    break;
                case State.Dashing:
                    MovementHandler(_dashingSpeed);
                    _dashTimer += Time.deltaTime;
                    if (_dashTimer >= _maxdashTimer)
                    {
                        state = State.Running;
                    }
                    break;
                case State.Walking:
                    MovementHandler(_walkingSpeed);
                    if (_runningTimer >= 0)
                    {
                        _runningTimer -= Time.deltaTime; if (_dashTimer >= 0)
                        {
                            _dashTimer -= Time.deltaTime;
                        }
                    }
                    break;
            }
        }
    }
}