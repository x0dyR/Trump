using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace collegeGame.Inputs
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool attack;
        public bool walk;
        public bool interact;
        public bool isWalking = false;
        public bool bow;
        public bool catalyst;
        public bool polearm;
        public bool sword;
        public float zoom;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnWalk(InputValue value)
        {
            WalkInput(value.isPressed);
        }
        public void OnAttack(InputValue value)
        {
            AttackInput(value.isPressed);
        }
        public void OnZoom(InputValue value)
        {
            if (cursorInputForLook)
            {
                ZoomInput(value.Get<float>() / 100f);
            }
        }

        public void OnBow(InputValue value)
        {
            BowChangeInput(value.isPressed);
        }
        public void OnCatalyst(InputValue value)
        {
            CatalystChangeInput(value.isPressed);
        }
        public void OnPolearm(InputValue value)
        {
            PolearmChangeInput(value.isPressed);
        }
        public void OnSword(InputValue value)
        {
            SwordChangeInput(value.isPressed);
        }

        public void OnInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }

#endif

        public void BowChangeInput(bool isPressed)
        {
            bow = isPressed;
        }
        public void CatalystChangeInput(bool isPressed)
        {
            catalyst = isPressed;
        }
        public void PolearmChangeInput(bool isPressed)
        {
            polearm = isPressed;
        }
        public void SwordChangeInput(bool isPressed)
        {
            sword = isPressed;
        }

        public void ZoomInput(float zoomValue)
        {
            zoom = zoomValue;
        }

        public void AttackInput(bool isPressed)
        {
            attack = isPressed;
        }

        public void WalkInput(bool isPressed)
        {
            if (isPressed) isWalking = !isWalking;
        }

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void InteractInput(bool isPressed)
        {
            interact = isPressed;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}