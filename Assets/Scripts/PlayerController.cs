using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private bool onGround = true;

    private Animator playerAnimator;
    private Animator doorAnimator;


    public Rigidbody playerRb;



    public float speed;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        doorAnimator = GameObject.FindWithTag("door").GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            playerAnimator.PlayInFixedTime("attack");
        }

        PlayerInputActions playerInputActions = new();
        playerInputActions.Enable();
        playerInputActions.Player.Jump.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerRb.AddForce(Vector3.up, ForceMode.Impulse); Debug.Log(context);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("door"))
        {
            doorAnimator.SetBool("isOpen", true);
            doorAnimator.PlayInFixedTime("open", -1, 3);
        }
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("door"))
        {
            doorAnimator.SetBool("isOpen", false);
            doorAnimator.PlayInFixedTime("close", -1, 3);
        }
    }
}
