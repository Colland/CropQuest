using UnityEngine;
using System;

public class PlantButton : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject plantObj;
    public GameObject grownObj;
    public GameObject playerObj;
    public Vector3 SpawnPoint;
    public GrowPlant nextScript;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnPlant();
        }
    }

    void SpawnPlant()
    {
        //spawn same location player is
        SpawnPoint = new Vector3((int)MathF.Round(playerObj.transform.position.x), (int)MathF.Round(playerObj.transform.position.y), (int)MathF.Round(playerObj.transform.position.z));

        //creating object and setting parent
        var pObj = Instantiate(plantObj, SpawnPoint, Quaternion.identity);
        pObj.transform.parent = parentObj.transform;

        nextScript = new GrowPlant();
        nextScript.Grow(SpawnPoint, grownObj);

        // destroy once grown spawns
        Destroy(pObj);
    }
}