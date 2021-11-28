using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Car;
    public void carSpawner()
    {
        GameObject carSpawn = Instantiate(Car) as GameObject;
        carSpawn.transform.position = new Vector3(-33.24f, 14.93572f, 42.17f);

    }

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
