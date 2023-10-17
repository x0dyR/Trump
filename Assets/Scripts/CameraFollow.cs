using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objectToFollow;
    public float speed = 0.125f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - objectToFollow.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = objectToFollow.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredPosition, speed);
        transform.position = smoothedposition;
    }
}
