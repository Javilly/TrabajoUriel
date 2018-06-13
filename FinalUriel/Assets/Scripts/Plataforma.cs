using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(PararPlataforma());
    }

    void FixedUpdate()
    {
        rb.AddForce(-transform.right * thrust);
    }

    IEnumerator PararPlataforma()
    {
        yield return new WaitForSeconds(10);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }



}