using UnityEngine;
using UnityEngine.InputSystem;

namespace collegeGame
{
    public class PlayerController : MonoBehaviour
    {
        private Animator playerAnimator;
        [field: SerializeField] private Animator doorAnimator;
        private Vector2 move;
        private Vector2 jump;

        public bool doorClose = true;
        public Rigidbody playerRb;
        public float speed;

        private void Awake()
        {
            playerAnimator = GetComponent<Animator>();
            playerRb = GetComponent<Rigidbody>();

        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            move = ctx.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                jump += Vector2.up / 2;
            }
        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && doorClose)
            {
                doorClose = false;

            }
            else if (ctx.performed && !doorClose)
            {
                doorClose = true;
            }
        }

        public void OnAttack(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                /*            playerAnimator.Play("attack");
                */
            }
        }

        void FixedUpdate()
        {

            movePlayer();

        }


        public void movePlayer()
        {
            Vector3 movement = new(move.x, jump.y, move.y);
            transform.Translate(speed * Time.deltaTime * movement, Space.World);
        }
        private void OnTriggerStay(Collider other)
        {

        }
        private void OnTriggerExit(Collider other)
        {
            doorClose = true;
        }
    }
}