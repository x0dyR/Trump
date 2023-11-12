using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator doorAnimator;
    private bool doorClose;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        
    }

    public void Update()
    {
        doorClose = GameObject.FindWithTag("Player").GetComponent<PlayerController>().doorClose;
    }

    // Update is called once per frame
    public void ToggleDoor()
    {
        if(!doorClose)
        {
            doorAnimator.CrossFadeInFixedTime("close", 0.4f);
        }
        if(doorClose)
        {
            doorAnimator.CrossFadeInFixedTime("open", 0.2f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        this.tag = "door";
    }
    private void OnCollisionExit(Collision collision)
    {
        this.tag = "asd";
    }
}
