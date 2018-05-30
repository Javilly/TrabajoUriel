using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlataformas : MonoBehaviour
{

    public Vector3 posInicial;
    public Vector3 posArriba;
    public Vector3 posCheck;
    bool subiendo = true;

    public float smoothFactor = 0.5f;

    void Update()
    {
        if (transform.position == posInicial || subiendo)
        {
            transform.position = Vector3.Lerp(transform.position, posArriba, Time.deltaTime * smoothFactor);

        }
        else if(transform.position == posArriba || !subiendo)
        {
            subiendo = false;
            transform.position = Vector3.Lerp(transform.position, posInicial, Time.deltaTime * smoothFactor);
        }
    }
}
