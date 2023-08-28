using System.Collections;
using UnityEngine;

public class CreatePlantButton : MonoBehaviour
{
    //figure out how to grab the plants free_9 asset (carrot)
    public GameObject Plant;
    public GameObject playerObj = null;
    public Vector3 SpawnPoint;

    void Update()
    {
        Event ev = Event.current;

        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.Log("It actually worked you mad lad");
            SpawnPlant();
        }
    }

    void SpawnPlant() 
    {
        SpawnPoint = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z+4);
        Instantiate(Plant, SpawnPoint, Quaternion.identity);
    }
}