using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace collegeGame
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControler : MonoBehaviour
    {
        [field: SerializeField] CharacterController _characterController;
        [field: SerializeField] PlayerInputActions _inputActions;
        [field: SerializeField] float _moveSpeed = 7f;
        public float turnSmoothVelocity;
        public float turnSmoothTime = .1f;

        private void Awake()
        {
            _inputActions = new();
            _inputActions.Player.Enable();
        }

        private void Update()
        {
            MovementHandler();
        }
        public Vector2 GetMovementNormalized()
        {
            return _inputActions.Player.Movement.ReadValue<Vector2>().normalized;
        }

        public void MovementHandler()
        {
            Vector3 inputDir = new(GetMovementNormalized().x, 0, GetMovementNormalized().y);
            float targetAngle = Mathf.Atan2(GetMovementNormalized().x, GetMovementNormalized().y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            if(inputDir == Vector3.zero)
            {
                return;
            }
            if(!_characterController.isGrounded)
            {
                moveDir += Physics.gravity;
            }
            transform.rotation = Quaternion.Euler(0, angle, 0);
            _characterController.Move(_moveSpeed * Time.deltaTime * moveDir);
        }


    }
}
/*

    public Vector2 GetMovementNormalized()
    {
        return inputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
    public void MovementHandler()
    {
        Vector3 moveDirecrtion = new(GetMovementNormalized().x, 0, GetMovementNormalized().y);
        float targetAngle = Mathf.Atan2(GetMovementNormalized().x, GetMovementNormalized().y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        Vector3 moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;

        if (moveDirecrtion == Vector3.zero)
        {
            return;
        }

        transform.rotation = Quaternion.Euler(0, angle, 0);
        characterController.Move(moveSpeed * Time.deltaTime * moveDir.normalized);
    }*/
