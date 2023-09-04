using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class PlantButton : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject plantObj;
    public GameObject grownObj;
    public GameObject playerObj;
    public Vector3 SpawnPoint;
    public GrowPlant nextScript;
    public LayerMask plantsCollisionLayer;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnPlant();
        }
    }

    void SpawnPlant()
    {
        //spawn location of plant
        SpawnPoint = new Vector3((int)MathF.Round(playerObj.transform.position.x + 1), (int)MathF.Round(playerObj.transform.position.y), (int)MathF.Round(playerObj.transform.position.z));
        //collision layer
        Collider2D collider = Physics2D.OverlapCircle(SpawnPoint, 0.2f, plantsCollisionLayer);

        if (collider == null)
        {
            //creating object and setting parent
            var pObj = Instantiate(plantObj, SpawnPoint, Quaternion.identity);
            pObj.transform.parent = parentObj.transform;

            nextScript = new GrowPlant();
            nextScript.Grow(SpawnPoint, grownObj);
            // destroy once grown spawns
            Destroy(pObj);
        } 
        else 
        {
            Debug.Log("oops");  
        }
    }
}