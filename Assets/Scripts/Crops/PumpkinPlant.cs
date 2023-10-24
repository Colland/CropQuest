using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PumpkinPlant : MonoBehaviour
{
    public GameObject plantObj;
    public GameObject growObj;
    public LayerMask collisionLayers;
    public int growDelay;

    // Start is called before the first frame update
    public void SpawnPlant(Vector3 loc)
    {
        Destroy(Instantiate(plantObj, loc, Quaternion.identity), growDelay);
        StartCoroutine(Grow(loc));
    }
    private IEnumerator Grow(Vector3 loc)
    {
        yield return new WaitForSeconds(growDelay);
        Instantiate(growObj, loc, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}