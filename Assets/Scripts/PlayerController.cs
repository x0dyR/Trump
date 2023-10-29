using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private bool onGround = true;

    private Animator playerAnimator;
    private Animator doorAnimator;
    private Vector2 move;
    private Vector2 jump;
    [SerializeField] private bool doorClose = true;
    [SerializeField] private bool signOpen = false;
    [SerializeField] private bool signClose = false;

    public Collider playerTriger;
    public Rigidbody playerRb;
    public float speed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        if (ctx.performed && doorClose)
        {
            doorClose = false;
            signOpen = true;
            signClose = false;
        }
        else if (ctx.performed && !doorClose)
        {
            doorClose = true;
            signClose = true;
            signOpen = false;
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
        Vector3 movement = new(move.x, jump.y, move.y);
        transform.Translate(speed*Time.deltaTime*movement, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("door"))
        {
            
        }
    }
}
