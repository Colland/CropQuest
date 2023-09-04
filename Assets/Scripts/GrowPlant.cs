using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    public Vector3 SpawnPoint;
    public GameObject parentObj;

    public void Grow(Vector3 loc, GameObject grownObj)
    {

        SpawnPoint = new(loc.x, loc.y, loc.z);
        var newObj = Instantiate(grownObj, SpawnPoint, Quaternion.identity);

        // setting parent is causing an issue for some reason
        // newObj.transform.SetParent(prevScript.parentObj.transform);
        // Debug.Log("step 5: transform.parent works");
    }
}
