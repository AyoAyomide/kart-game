using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float moveSpeed;
    public float turnSpeed;

    void Start()
    {
        transform.parent = null;
    }

    void Update()
    {


        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, turnSpeed * Time.deltaTime);
    }
}
