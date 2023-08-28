using System.Collections;
using UnityEngine;

public class PlantButton : MonoBehaviour
{
    public GameObject Plant;
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
        Instantiate(Plant, SpawnPoint, Quaternion.identity);
    }
}