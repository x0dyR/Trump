using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private bool onGround = true;

    private Animator playerAnimator;
    private Animator doorAnimator;
    private Vector2 move;
    private Vector2 jump;
    public Collider doorCollider;

    public Rigidbody playerRb;
    public float speed;
    public bool state = true;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        doorAnimator = GameObject.FindWithTag("door").GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();

    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        jump = ctx.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        /*        if (ctx.performed && doorCollider.CompareTag("door"))
                {
                    doorAnimator.SetBool("isOpen", true);
                    doorAnimator.PlayInFixedTime("open", -1, 3);
                }
                if (ctx.performed && doorCollider.CompareTag("door"))
                {
                    doorAnimator.SetBool("isOpen", false);
                    doorAnimator.PlayInFixedTime("close", -1, 3);
                }*/
    }

    void FixedUpdate()
    {

        movePlayer();

    }

    public void movePlayer()
    {
        Vector3 movement = new(move.x, jump.y, move.y);
        transform.Translate(speed*Time.deltaTime*movement, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.CompareTag("door"))
        {
            doorAnimator.SetBool("isOpen", true);
            doorAnimator.PlayInFixedTime("open");
        }
        if (Input.GetKey(KeyCode.E) && other.CompareTag("door"))
        {
            doorAnimator.SetBool("isOpen", false);
            doorAnimator.PlayInFixedTime("close");
        }
    }
}
