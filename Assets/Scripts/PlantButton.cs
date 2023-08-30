using UnityEngine;

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
        SpawnPoint = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z);

        //creating object and setting parent
        var pObj = Instantiate(plantObj, SpawnPoint, Quaternion.identity);
        pObj.transform.parent = parentObj.transform;

        nextScript = new GrowPlant();
        nextScript.Grow(SpawnPoint, grownObj);

        // destroy once grown spawns
        // Destroy(pObj);
    }
}