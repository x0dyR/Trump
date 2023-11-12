using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] private Animator doorAnimator;
    private Vector3 move;

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
        move = ctx.ReadValue<Vector3>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            move += Vector3.up / 2;
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
            playerAnimator.Play("attack");
        }
    }

    void FixedUpdate()
    {

        movePlayer();

    }


    public void movePlayer()
    {
        Vector3 movement = new(move.x, move.y, move.z);
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
