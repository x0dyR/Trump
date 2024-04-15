using UnityEngine;
using System;
using TMPro;
using Zenject;



#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using Cinemachine;
#endif

namespace collegeGame.Inputs
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour, ITarget
    {
        #region Variables
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 7f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 10.5f;

        [Tooltip("Walk speed of the characher in m/s")]
        public float WalkSpeed = 3.5f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.0f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.3f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;
        private CinemachineVirtualCamera cm;
        public float minCameraDistance = 1f;
        public float maxCameraDistance = 8f;
        public float cameraDistance = 6f;
        [Header("Interaction")]
        [field: SerializeField] private float interactionDistance = 2f;
        [Header("Weapon")]
        [field: SerializeField] private Weapon weapon;
        [HideInInspector] public bool isAttacking;
        private float _cooldownToChangeWeapon = 3f;

        [Header("Layers")]
        [field: SerializeField] LayerMask interactableLayer;
        [field: SerializeField] LayerMask damageableLayer;

        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        private float _speed;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        private float _maxSprintTime = 5f;
        public float currentSprintTime = 0;

        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

#if ENABLE_INPUT_SYSTEM
        private PlayerInput _playerInput;
#endif
        public Animator animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;
        #endregion
        public Transform cameraPoint;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }

        private void Awake()
        {
            cm = FindObjectOfType<CinemachineVirtualCamera>();
            cm.m_Follow = cameraPoint;
            cm.m_LookAt = cameraPoint;

            if (_mainCamera == null)
            {
                _mainCamera = Camera.main.gameObject;
            }
        }

        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            animator = GetComponent<Animator>();
            weapon = GetComponentInChildren<Weapon>();
#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
            currentSprintTime = _input.sprint ? currentSprintTime + Time.deltaTime : currentSprintTime - Time.deltaTime;
            GroundedCheck();
            Move();
            Attack();
            Interact();
            JumpAndGravity();
        }
        private void LateUpdate()
        {
            CameraRotation();
            CameraZoom();
            ChangeToAny();
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);
        }

        private void CameraZoom()
        {
            float scrollValue = _input.zoom;
            var framingTranspoer = cm.GetCinemachineComponent<CinemachineFramingTransposer>();
            cameraDistance += scrollValue;
            cameraDistance = Mathf.Clamp(cameraDistance, minCameraDistance, maxCameraDistance);
            framingTranspoer.m_CameraDistance = Mathf.Lerp(framingTranspoer.m_CameraDistance, cameraDistance, Time.deltaTime * 10);
        }

        private void CameraRotation()
        {
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            if (isAttacking) { return; }
            float targetSpeed = _input.sprint ? SprintSpeed : (_input.isWalking ? WalkSpeed : MoveSpeed);
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;
            animator.SetFloat("Speed", targetSpeed);
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                _speed = targetSpeed * inputDirection.magnitude;
            }
            else
            {
                _speed = 0.0f;
            }
            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void Interact()
        {
            if (_input.interact)
            {
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactionDistance, interactableLayer);
                foreach (Collider collider in colliderArray)
                {
                    collider.TryGetComponent(out Interactable interactable);
                    interactable.Interact(_controller);
                }
            }
            _input.interact = false;
        }

        public void EnableCollider(int enable)
        {
            if (enable == 1)
            {
                weapon.boxCollider.enabled = true;
                weapon.Attack(damageableLayer);
            }
            weapon.boxCollider.enabled = false;
        }


        private void Attack()
        {
            if (_input.attack && Grounded)
            {
                animator.SetTrigger("Attack");
            }
            _input.attack = false;
        }

        private void ChangeToAny()
        {
            /* Bow weaponBow;

             weaponBow = GetComponentInChildren<Bow>();
             if (_input.bow)
             {
                 weapon.gameObject.SetActive(false);
                 weapon = weaponBow;
                 weapon.gameObject.SetActive(true);
             }*/
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                _fallTimeoutDelta = FallTimeout;
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                    animator.SetTrigger("Jump");
                }
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }

            }
            else
            {
                _jumpTimeoutDelta = JumpTimeout;

                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                _input.jump = false;
            }
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        Transform ITarget.GetTransform()
        {
            return transform;
        }
    }
}