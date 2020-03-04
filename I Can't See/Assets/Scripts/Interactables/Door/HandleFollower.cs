using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class makes the door handle follow an object in the world
/// </summary>
public class HandleFollower : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(target.transform.position);
    }
}

//Lewis Hammond
