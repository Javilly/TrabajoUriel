using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlataformas : MonoBehaviour
{

    private PoolManager myPoolManager;

    [SerializeField] string poolNameToUse;

    float posX;
    float posY;
    float posZ;

    int posArrayPool = 0;
    bool primerPlataforma = true;

    private void Start()
    {
        myPoolManager = PoolManager.Instance;


        StartCoroutine(CrearPlataforma());
        //StartCoroutine(DestruirPlataforma());

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

    }

    private void Update()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }
    IEnumerator CrearPlataforma()
    {
        yield return new WaitForSeconds(2);
        myPoolManager.RequestToPool(poolNameToUse, new Vector3(Random.Range(posX + 3, posX - 3), Random.Range(posY + 3, posY - 3), Random.Range(posZ + 3, posZ - 3)), new Vector3(0, 0, 0));
        StartCoroutine(CrearPlataforma());
    }

    
    IEnumerator DestruirPlataforma()
    {
        if (primerPlataforma)
        {
            yield return new WaitForSeconds(20);
            primerPlataforma = false;
        }
        else
        {
            yield return new WaitForSeconds(2);
        }
        posArrayPool++;

        myPoolManager.DeleteObjectFromPool(poolNameToUse, posArrayPool);

        if (posArrayPool == 19)
        {
            posArrayPool = 0;
        }

        StartCoroutine(DestruirPlataforma());
    }
    
}
