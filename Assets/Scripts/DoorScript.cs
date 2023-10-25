using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    public Animator animatorDoor;
    private Collider doorCollider;
    private PlayerInput playerInput;

    public bool state = false;
    private void Awake()
    {
        doorCollider = GameObject.FindWithTag("door").GetComponent<Collider>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !state)
        {
            state = true;
            animatorDoor.SetBool("isOpen", true);
            animatorDoor.PlayInFixedTime("open", -1, 3);
        }
        if (Input.GetKeyDown(KeyCode.E) && state)
        {
            state = false;
            animatorDoor.SetBool("isOpen", false);
            animatorDoor.PlayInFixedTime("close", -1, 3);
        }

    }
}
