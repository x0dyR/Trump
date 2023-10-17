using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody enemyRb;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        target = GameObject.Find("pers");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newEnemyPos = new(Random.Range(2f, 4f), target.transform.position.y, Random.Range(2f, 4f));
        Vector3 vectorToTarget = new((target.transform.position.x-Random.Range(2.0f, 4.0f)), target.transform.position.y, (target.transform.position.z-Random.Range(2.0f, 4.0f)));
        enemyRb.AddForce(vectorToTarget);
    }
}
