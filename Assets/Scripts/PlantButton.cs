using System.Collections;
using UnityEngine;

public class PlantButton : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject plantObj;
    public GameObject playerObj;
    public Vector3 SpawnPoint;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            SpawnPlant();
        }
    }

    void SpawnPlant() 
    {
        //spawn same location player is
        SpawnPoint = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z);

        var pObj = Instantiate(plantObj, SpawnPoint, Quaternion.identity);
        pObj.transform.parent = parentObj.transform;
    }
}