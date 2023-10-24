using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BerriesPlant : MonoBehaviour
{
    public GameObject plantStage1;
    public GameObject plantStage2;
    public GameObject plantStage3;
    public GameObject growObj;
    public LayerMask collisionLayers;
    public int growDelay;
    public Vector3 plantLoc;

    // Start is called before the first frame update
    public void SpawnPlant(Vector3 loc)
    {
        Destroy(Instantiate(plantStage1, loc, Quaternion.identity), growDelay);
        StartCoroutine(Grow(loc));
        plantLoc = loc;
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