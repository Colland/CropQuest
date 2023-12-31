using System;
using System.Collections;
using UnityEngine;

public class Planting : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject plantObj;
    public GameObject growObj;
    public LayerMask collisionLayers;
    public int growDelay;
    AudioManager audioManager;
    public PumpkinPlant pumpkinPlant;
    public BerriesPlant berriesPlant;

    // Update is called once per frame
    void Update()
    {
        Vector3 location = new((int)MathF.Round(playerObj.transform.position.x + 1), (int)MathF.Round(playerObj.transform.position.y), (int)MathF.Round(playerObj.transform.position.z));

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics2D.OverlapCircle(location, 0.2f, collisionLayers) == null)
            {
                pumpkinPlant.SpawnPlant(location);
                audioManager.playSFX(audioManager.plant);
            }

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Physics2D.OverlapCircle(location, 0.2f, collisionLayers) == null)
            {
                berriesPlant.SpawnPlant(location);
                audioManager.playSFX(audioManager.plant);
            }

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Press F");

        }

    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    //instantiate the first phase plant and destroy it after set time
    //coroutine running same time to instantiate second phase
    /*void SpawnPlant(Vector3 loc)
    {
        Destroy(Instantiate(plantObj, loc, Quaternion.identity), growDelay);
        StartCoroutine(Grow(loc));
    }*/

    //this runs when first phase is started, instantiate after first phase destroyed
    /*private IEnumerator Grow(Vector3 loc)
    {
        yield return new WaitForSeconds(growDelay);
        Instantiate(growObj, loc, Quaternion.identity);
    }*/
}